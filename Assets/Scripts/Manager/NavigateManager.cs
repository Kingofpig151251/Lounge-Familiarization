using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NavigateManager : Singleton<NavigateManager>
{
    private int m_correctRate = 0;
    private int m_totalQuestionGenerate = 0;
    private int m_questionIndex;

    private TaskSO m_currentTaskSO;

    private void Start()
    {
        SetUpListener();
    }

    private void SetUpListener()
    {
        GameEventReference.Instance.OnEnterNavigatePhase.AddListener(OnEnterNavigatePhase);
        GameEventReference.Instance.OnExitNavigatePhase.AddListener(OnExitNavigatePhase);
        GameEventReference.Instance.OnConfirmNavigate.AddListener(OnConfirmNavigate);
        GameEventReference.Instance.OnLanguageChanged.AddListener(OnLanguageChanged);
    }

    private void OnEnterNavigatePhase(params object[] param)
    {
        UIElementReference.Instance.m_navigatePanel.SetActive(true);
        UIElementReference.Instance.m_exitNavigateButton.gameObject.SetActive(true);

        GenerateTask();
        ChangeCorrectRateText();
    }
    private void OnExitNavigatePhase(params object[] param)
    {
        UIElementReference.Instance.m_navigatePanel.SetActive(false);
        UIElementReference.Instance.m_exitNavigateButton.gameObject.SetActive(false);
        GameEventReference.Instance.OnEnter360Mode.Trigger();
    }

    private void OnConfirmNavigate(params object[] param)
    {
        ++m_totalQuestionGenerate;
        if (ViewPointManager.Instance.m_currentViewPoint.m_index == m_currentTaskSO.m_navigateIndex)
        {
            ++m_correctRate;
        }
        else
        {

        }
        GenerateTask();
        ChangeCorrectRateText();
    }

    private void OnLanguageChanged(params object[] param)
    {
        int language = (int)param[0];

        if (m_currentTaskSO != null)
        {
            UpdateTaskText(language);
        }
        ChangeCorrectRateText();
    }

    private void ChangeCorrectRateText()
    {
        //int rate = (m_correctRate / m_totalQuestionGenerate);
        string text;
        switch (GameManager.Instance.GetCurrentLanguage())
        {
            case Class_Language.English:
                text = string.Format($"Correct Rate : {m_correctRate} /  {m_totalQuestionGenerate}");
                break;
            case Class_Language.SimplifiedChinese:
                text = string.Format($"正确率 : {m_correctRate} /  {m_totalQuestionGenerate}");
                break;
            case Class_Language.TraditionalChinese:
                text = string.Format($"正確率 : {m_correctRate} /  {m_totalQuestionGenerate}");
                break;
            default:
                text = "NA";
                break;
        }
        UIElementReference.Instance.m_taskCorrectRate.text = text;
    }

    private void GenerateTask()
    {
        System.DateTime now = System.DateTime.Now;

        //do
        //{

        int seed = (int)((now.Day) * now.Millisecond * Time.realtimeSinceStartup / now.Minute);
        Random.InitState(seed);
        m_questionIndex = Mathf.Clamp(Random.Range(0, TaskReference.Instance.m_taskConfigSO.Count), 0, TaskReference.Instance.m_taskConfigSO.Count - 1);


        //} while (isQuestionIndexPremit());

        m_currentTaskSO = TaskReference.Instance.m_taskConfigSO[m_questionIndex];
        UpdateTaskText(GameManager.Instance.GetCurrentLanguage());
    }

    private void UpdateTaskText(int language)
    {
        switch (language)
        {
            case Class_Language.English:
                UIElementReference.Instance.m_taskText.text = m_currentTaskSO.m_question_ENG;
                break;
            case Class_Language.SimplifiedChinese:
                UIElementReference.Instance.m_taskText.text = m_currentTaskSO.m_question_SC;
                break;
            case Class_Language.TraditionalChinese:
                UIElementReference.Instance.m_taskText.text = m_currentTaskSO.m_question_TC;
                break;
        }
    }

    private bool isQuestionIndexPremit()
    {
        switch (m_questionIndex)
        {
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
            default:
                return true;
        }
    }

    private void OnGameReset(params object[] param)
    {
        m_correctRate = 0;
        m_totalQuestionGenerate = 0;
    }
}

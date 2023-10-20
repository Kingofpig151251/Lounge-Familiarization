using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NavigateManager : Singleton<NavigateManager>
{
    public bool m_isEnterNavigatePhase = false;
    public int m_correctRate = 0;
    public int m_totalQuestionGenerate = 0;
    public int m_questionIndex;

    private void Start()
    {
        SetUpListener();
    }

    private void SetUpListener()
    {
        GameEventReference.Instance.OnEnterNavigatePhase.AddListener(OnEnterNavigatePhase);
        GameEventReference.Instance.OnExitNavigatePhase.AddListener(OnExitNavigatePhase);
        GameEventReference.Instance.OnConfirmNavigate.AddListener(OnConfirmNavigate);
    }

    private void OnEnterNavigatePhase(params object[] param)
    {
        m_isEnterNavigatePhase = true;
        UIElementReference.Instance.m_navigatePanel.SetActive(true);
        UIElementReference.Instance.m_confirmNavigateButton.gameObject.SetActive(true);
        UIElementReference.Instance.m_exitNavigateButton.gameObject.SetActive(true);

        GenerateTask();
    }
    private void OnExitNavigatePhase(params object[] param)
    {
        m_isEnterNavigatePhase = false;
        UIElementReference.Instance.m_navigatePanel.SetActive(false);
        UIElementReference.Instance.m_confirmNavigateButton.gameObject.SetActive(false);
        UIElementReference.Instance.m_exitNavigateButton.gameObject.SetActive(false);
        GameEventReference.Instance.OnEnter360Mode.Trigger();
    }

    private void OnConfirmNavigate(params object[] param)
    {
        Debug.Log($"currentViewPoint {m_questionIndex} ; Navigate Index {UIElementReference.Instance.m_questionList[m_questionIndex].m_navigateIndex}");
        if (ViewPointManager.Instance.m_currentViewPoint.m_index == UIElementReference.Instance.m_questionList[m_questionIndex].m_navigateIndex)
        {
            m_correctRate++;
        }
        else
        {
            
        }
        ChangeCorrectRateText();
        GenerateTask();
    }

    private void ChangeCorrectRateText()
    {
        //int rate = (m_correctRate / m_totalQuestionGenerate);
        string text = string.Format($"Correct Rate : {m_correctRate} /  {m_totalQuestionGenerate}");
        Debug.Log(text);
        UIElementReference.Instance.m_taskCorrectRate.text = text;
    }

    private void GenerateTask()
    {
        m_totalQuestionGenerate++;
        System.DateTime now = System.DateTime.Now;

        //do
        //{

        int seed = (int)((now.Day) * now.Millisecond * Time.realtimeSinceStartup / now.Minute);
        Random.InitState(seed);
        m_questionIndex = Mathf.Clamp(Random.Range(0, UIElementReference.Instance.m_questionList.Count), 0, UIElementReference.Instance.m_questionList.Count - 1);


        //} while (isQuestionIndexPremit());

        UIElementReference.Instance.m_taskText.text = UIElementReference.Instance.m_questionList[m_questionIndex].m_question;
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
}

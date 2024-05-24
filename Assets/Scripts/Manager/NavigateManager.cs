using System;
using System.Collections.Generic;
using System.Linq;
using Reference;
using UnityEngine;
using Random = UnityEngine.Random;

public class NavigateManager : Singleton<NavigateManager>
{
    private int m_correctRate = 0;
    private int m_totalQuestionGenerate = 0;
    private int m_questionIndex;
    private int m_previousQuestionIndex;

    private List<TaskSO> m_taskQueue;
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
        GameEventReference.Instance.OnGameReset.AddListener(OnGameReset);
    }

    private void OnEnterNavigatePhase(params object[] param)
    {
        UIElementReference.Instance.m_navigatePanel.SetActive(true);
        UIElementReference.Instance.m_exitNavigateButton.gameObject.SetActive(true);

        m_taskQueue = TaskReference.Instance.m_taskConfigSO
            .Where(t => t.m_lounge == ViewPointManager.Instance.m_currentLounge)
            .ToList();

        GenerateTaskFromQueue();
        ChangeCorrectRateText(GameManager.Instance.GetCurrentLanguage());
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
        if (m_currentTaskSO.m_navigateIndex.Contains(ViewPointManager.Instance.m_currentViewPoint.m_index))
        {
            ++m_correctRate;
            UIElementReference.Instance.m_correctPanel.SetActive(true);
            m_currentTaskSO.CompleteTask();
        }
        else
        {
            UIElementReference.Instance.m_wrongPanel.SetActive(true);
        }
        ChangeCorrectRateText(GameManager.Instance.GetCurrentLanguage());
    }

    private void OnLanguageChanged(params object[] param)
    {
        int language = (int)param[0];

        if (m_currentTaskSO != null)
        {
            UpdateTaskText(language);
        }

        ChangeCorrectRateText(language);
    }

    private void ChangeCorrectRateText(int language)
    {
        //int rate = (m_correctRate / m_totalQuestionGenerate);
        string text;
        switch (language)
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

    public void GenerateTaskFromQueue()
    {
        // Prevents from infinite loop
        // If all tasks in the lounge are completed, set the current task to the placeholder task
        if (m_taskQueue.TrueForAll(t => t.IsCompleted()))
        {
            m_currentTaskSO = TaskReference.Instance.m_completedTaskPlaceHolderSO;
            UpdateTaskText(GameManager.Instance.GetCurrentLanguage());
            UIElementReference.Instance.m_confirmButton.SetActive(false);
            UIElementReference.Instance.m_selectLocationLabel.SetActive(false);
            return;
        }

        var inCompleteQueue = m_taskQueue.Where(t => !t.IsCompleted()).ToList();
        var index = Random.Range(0, inCompleteQueue.Count);
        m_questionIndex = TaskReference.Instance.m_taskConfigSO.FindIndex(m => m == inCompleteQueue[index]);
        m_currentTaskSO = TaskReference.Instance.m_taskConfigSO[m_questionIndex];
        UpdateTaskText(GameManager.Instance.GetCurrentLanguage());
        UIElementReference.Instance.m_confirmButton.SetActive(true);
    }

    // Unused method, can be safely removed or comment out
    [Obsolete]
    public void GenerateTask()
    {
        // Prevents from infinite loop
        // If all tasks in the lounge are completed, set the current task to the placeholder task
        if (m_taskQueue.TrueForAll(t => t.IsCompleted()))
        {
            m_currentTaskSO = TaskReference.Instance.m_completedTaskPlaceHolderSO;
            UpdateTaskText(GameManager.Instance.GetCurrentLanguage());
            UIElementReference.Instance.m_confirmButton.SetActive(false);
            UIElementReference.Instance.m_selectLocationLabel.SetActive(false);
            return;
        }

        // Look for another task if the current task is completed
        do
        {
            GenerateNewSeed();
            m_questionIndex = Mathf.Clamp(Random.Range(0, TaskReference.Instance.m_taskConfigSO.Count), 0,
                TaskReference.Instance.m_taskConfigSO.Count - 1);
        } while (!isQuestionIndexPremit() || TaskReference.Instance.m_taskConfigSO[m_questionIndex].IsCompleted());

        m_previousQuestionIndex = m_questionIndex;
        m_currentTaskSO = TaskReference.Instance.m_taskConfigSO[m_questionIndex];
        UpdateTaskText(GameManager.Instance.GetCurrentLanguage());
        UIElementReference.Instance.m_confirmButton.SetActive(true);
    }

    private static void GenerateNewSeed()
    {
        var now = DateTime.Now;
        var seed = (int)(now.Day * now.Millisecond * Time.realtimeSinceStartup / now.Minute);
        Random.InitState(seed);
    }

    private void UpdateTaskText(int language)
    {
        UIElementReference.Instance.m_taskText.text = language switch
        {
            Class_Language.English => m_currentTaskSO.m_question_ENG,
            Class_Language.SimplifiedChinese => m_currentTaskSO.m_question_SC,
            Class_Language.TraditionalChinese => m_currentTaskSO.m_question_TC,
            _ => UIElementReference.Instance.m_taskText.text
        };
    }

    private bool isQuestionIndexPremit()
    {
        return TaskReference.Instance.m_taskConfigSO[m_questionIndex].m_lounge == ViewPointManager.Instance.m_currentLounge && m_previousQuestionIndex != m_questionIndex;
    }

    private void OnGameReset(params object[] param)
    {
        UIElementReference.Instance.m_navigatePanel.SetActive(false);
    }

    public TaskSO GetCurrentTaskSO() => m_currentTaskSO;

    public void ResetTaskHistory()
    {
        PlayerPrefs.SetInt("task-mask", 0);
    }
}
using Reference;
using UnityEngine;

public class NavigateManager : Singleton<NavigateManager>
{
    private int m_correctRate = 0;
    private int m_totalQuestionGenerate = 0;
    private int m_questionIndex;
    private int m_previousQuestionIndex;

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

        GenerateTask();
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

    public void GenerateTask()
    {
        System.DateTime now = System.DateTime.Now;

        do
        {
            int seed = (int)((now.Day) * now.Millisecond * Time.realtimeSinceStartup / now.Minute);
            Random.InitState(seed);
            m_questionIndex = Mathf.Clamp(Random.Range(0, TaskReference.Instance.m_taskConfigSO.Count), 0,
                TaskReference.Instance.m_taskConfigSO.Count - 1);
        } while (!isQuestionIndexPremit());

        m_previousQuestionIndex = m_questionIndex;
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
        if (TaskReference.Instance.m_taskConfigSO[m_questionIndex].m_lounge == ViewPointManager.Instance.m_currentLounge && m_previousQuestionIndex != m_questionIndex)
        {
            return true;
        }

        return false;
    }

    private void OnGameReset(params object[] param)
    {
        UIElementReference.Instance.m_navigatePanel.SetActive(false);
    }

    public TaskSO GetCurrentTaskSO() => m_currentTaskSO;
}
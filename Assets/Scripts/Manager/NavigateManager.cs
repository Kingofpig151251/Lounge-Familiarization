using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigateManager : Singleton<NavigateManager>
{
    public bool m_isEnterNavigatePhase = false;

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
        UIElementReference.Instance.m_navigatePanel.SetActive(true);
        UIElementReference.Instance.m_confirmNavigateButton.gameObject.SetActive(true);
        UIElementReference.Instance.m_exitNavigateButton.gameObject.SetActive(true);
        m_isEnterNavigatePhase = true;
    }
    private void OnExitNavigatePhase(params object[] param)
    {
        m_isEnterNavigatePhase = false;
        UIElementReference.Instance.m_navigatePanel.SetActive(false);
        UIElementReference.Instance.m_confirmNavigateButton.gameObject.SetActive(false);
        UIElementReference.Instance.m_exitNavigateButton.gameObject.SetActive(false);
    }

    private void OnConfirmNavigate(params object[] param)
    {
        if (ViewPointManager.Instance.m_currentViewPoint.m_index == UIElementReference.Instance.m_questionList[TestPanelManager.Instance.m_questionIndex].m_navigateIndex)
        {
            Debug.Log("Correct");
        }
        else
        {
            Debug.Log("Wrong");
        }
        GameEventReference.Instance.OnExitNavigatePhase.Trigger();
    }
}

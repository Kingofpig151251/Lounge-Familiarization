using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    private int m_CurrentMode = Class_PlayMode.ViewMode;
    private int m_currentLanguage = Class_Language.TraditionalChinese;
    private bool m_isCityMapPanelActive = true;

    private void Start()
    {
        GameEventReference.Instance.OnEnter360Mode.AddListener(Show360ModePanel);
        GameEventReference.Instance.OnEnterTaskMode.AddListener(OnEnterTaskMode);
        GameEventReference.Instance.OnChangeRegion.AddListener(OnChangeRegion);
        GameEventReference.Instance.OnLanguageChanged.AddListener(OnLanguageChanged);
        GameEventReference.Instance.OnGameReset.AddListener(OnGameReset);
    }

    private void SwitchMode(int mode)
    {
        m_CurrentMode = mode;

        bool isViewMode = m_CurrentMode == Class_PlayMode.ViewMode;
        bool isTaskMode = m_CurrentMode == Class_PlayMode.TaskMode;

        UIElementReference.Instance.m_TopBar.SetActive(isViewMode);
        UIElementReference.Instance.m_InfoPanel.SetActive(isViewMode);
        UIElementReference.Instance.m_floorPlanButton.SetActive(isViewMode);

        UIElementReference.Instance.m_exitNavigateButton.gameObject.SetActive(isTaskMode);
        UIElementReference.Instance.m_navigatePanel.gameObject.SetActive(isTaskMode);

        UIElementReference.Instance.m_taskList.SetActive(isTaskMode);
    }

    private void Show360ModePanel(params object[] param)
    {
        m_CurrentMode = Class_PlayMode.ViewMode;

        SwitchMode(m_CurrentMode);
    }

    private void OnEnterTaskMode(params object[] param)
    {
        m_CurrentMode = Class_PlayMode.TaskMode;

        SwitchMode(m_CurrentMode);

        GameEventReference.Instance.OnEnterNavigatePhase.Trigger();
    }

    private void OnChangeRegion(params object[] param)
    {
        m_isCityMapPanelActive = false;
    }

    private void OnLanguageChanged(params object[] param)
    {
        int language = (int)param[0];
        m_currentLanguage = language;
    }

    private void OnGameReset(params object[] param)
    {
        GameEventReference.Instance.OnEnter360Mode.Trigger();
        GameEventReference.Instance.OnEnterViewPoint.Trigger(0);

        UIElementReference.Instance.m_CityMapPanel.SetActive(true);

        UIElementReference.Instance.m_TopBar.SetActive(false);
        UIElementReference.Instance.m_InfoPanel.SetActive(false);
        //Application.Quit();

        //var process = System.Diagnostics.Process.GetCurrentProcess();
        //System.Diagnostics.Process.Start(process.ProcessName);
    }

    public bool IsCityMapPanelActive() => m_isCityMapPanelActive;
    public int GetCurrentMode() => m_CurrentMode;
    public int GetCurrentLanguage() => m_currentLanguage;
}

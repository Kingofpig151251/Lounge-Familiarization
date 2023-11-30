using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    private int m_CurrentMode = Class_PlayMode.StartMode;
    private int m_currentLanguage = Class_Language.TraditionalChinese;
    private bool m_isCityMapPanelActive = true;

    private void Start()
    {
        GameEventReference.Instance.OnEnter360Mode.AddListener(OnEnter360Mode);
        GameEventReference.Instance.OnEnterTaskMode.AddListener(OnEnterTaskMode);
        GameEventReference.Instance.OnChangeRegion.AddListener(OnChangeRegion);
        GameEventReference.Instance.OnLanguageChanged.AddListener(OnLanguageChanged);
        GameEventReference.Instance.OnGameReset.AddListener(OnGameReset);
        HiedLanguageButton();
    }

    private void SwitchMode(int mode)
    {
        m_CurrentMode = mode;

        bool isViewMode = m_CurrentMode == Class_PlayMode.ViewMode;
        bool isTaskMode = m_CurrentMode == Class_PlayMode.TaskMode;
        bool isStartMode = m_CurrentMode == Class_PlayMode.StartMode;

        UIElementReference.Instance.m_TopBar.SetActive(isViewMode);
        UIElementReference.Instance.m_InfoPanel.SetActive(isViewMode);
        UIElementReference.Instance.m_FloorPlanButton.SetActive(isViewMode);
        UIElementReference.Instance.m_GameModeSwitcher.SetActive(isViewMode);

        UIElementReference.Instance.m_exitNavigateButton.gameObject.SetActive(isTaskMode);
        UIElementReference.Instance.m_navigatePanel.gameObject.SetActive(isTaskMode);
        UIElementReference.Instance.m_taskList.SetActive(isTaskMode);
    }

    private void OnEnter360Mode(params object[] param)
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
        HiedLanguageButton();
    }

    private void OnGameReset(params object[] param)
    {
        GameEventReference.Instance.OnEnter360Mode.Trigger();
        GameEventReference.Instance.OnEnterViewPoint.Trigger(0);

        UIElementReference.Instance.m_IntroducePanel.SetActive(true);

        UIElementReference.Instance.m_InfoPanel.SetActive(true);

        UIElementReference.Instance.m_TopBar.SetActive(true);
        UIElementReference.Instance.m_FloorPlanButton.SetActive(false);
        UIElementReference.Instance.m_GameModeSwitcher.SetActive(false);
        UIElementReference.Instance.m_InfoPanel.SetActive(false);

        //Application.Quit();

        //var process = System.Diagnostics.Process.GetCurrentProcess();
        //System.Diagnostics.Process.Start(process.ProcessName);
    }

    private void HiedLanguageButton()
    {
        UIElementReference.Instance.m_TopBarENG.GetComponent<TMP_Text>().color = new Color(0.184f, 0.310f, 0.306f, 1f);
        UIElementReference.Instance.m_TopBarSC.GetComponent<TMP_Text>().color = new Color(0.184f, 0.310f, 0.306f, 1f);
        UIElementReference.Instance.m_TopBarTC.GetComponent<TMP_Text>().color = new Color(0.184f, 0.310f, 0.306f, 1f);
        switch (m_currentLanguage)
        {
            case Class_Language.English:
                UIElementReference.Instance.m_TopBarENG.GetComponent<TMP_Text>().color = new Color(0.329f, 0.502f, 0.498f, 1f);
                break;
            case Class_Language.SimplifiedChinese:
                UIElementReference.Instance.m_TopBarSC.GetComponent<TMP_Text>().color = new Color(0.329f, 0.502f, 0.498f, 1f);
                break;
            case Class_Language.TraditionalChinese:
                UIElementReference.Instance.m_TopBarTC.GetComponent<TMP_Text>().color = new Color(0.329f, 0.502f, 0.498f, 1f);
                break;
        }
    }

    public bool IsCityMapPanelActive() => m_isCityMapPanelActive;
    public int GetCurrentMode() => m_CurrentMode;
    public int GetCurrentLanguage() => m_currentLanguage;
}
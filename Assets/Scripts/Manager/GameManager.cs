using System;
using Reference;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private InfoSO m_TheDeckInfo;

    [SerializeField]
    private InfoSO m_ThePierInfo;

    [SerializeField]
    private InfoSO m_TheWingInfo;
    private int m_CurrentMode = Class_PlayMode.StartMode;
    private int m_currentLanguage = Class_Language.English;
    private bool m_isCityMapPanelActive = true;
    private bool m_is360FristTime = true;
    private bool m_isTaskFristTime = true;

    private void Start()
    {
        RegisterEvents();
        HiedLanguageButton();
    }

    private void RegisterEvents()
    {
        GameEventReference.Instance.OnEnter360Mode.AddListener(OnEnter360Mode);
        GameEventReference.Instance.OnEnterTaskMode.AddListener(OnEnterTaskMode);
        GameEventReference.Instance.OnChangeRegion.AddListener(OnChangeRegion);
        GameEventReference.Instance.OnLanguageChanged.AddListener(OnLanguageChanged);
        GameEventReference.Instance.OnGameReset.AddListener(OnGameReset);
    }

    private void OnEnter360Mode(params object[] param)
    {
        m_CurrentMode = Class_PlayMode.ViewMode;
        EnterViewMode();
    }

    private void OnEnterTaskMode(params object[] param)
    {
        m_CurrentMode = Class_PlayMode.TaskMode;
        EnterTaskMode();
    }

    private void EnterViewMode()
    {
        UIElementReference.Instance.m_resetTaskButton.gameObject.SetActive(false);
        UIElementReference.Instance.m_homeButton.SetActive(true);
        UIElementReference.Instance.m_InfoPanel.SetActive(true);
        UIElementReference.Instance.m_FloorPlanButton.SetActive(true);
        UIElementReference.Instance.m_GameModeSwitcher.SetActive(true);
    }

    private void EnterTaskMode()
    {
        if (m_isTaskFristTime)
        {
            UIElementReference.Instance.m_TaskteachingPanel.SetActive(true);
            m_isTaskFristTime = false;
        }

        UIElementReference.Instance.m_exitNavigateButton.gameObject.SetActive(true);
        UIElementReference.Instance.m_navigatePanel.gameObject.SetActive(true);
        UIElementReference.Instance.m_taskList.SetActive(true);
        UIElementReference.Instance.m_taskModeButton.SetActive(false);
        UIElementReference.Instance.m_FloorPlanButton.SetActive(false);
        UIElementReference.Instance.m_InfoPanel.SetActive(false);
        GameEventReference.Instance.OnEnterNavigatePhase.Trigger();
    }

    private void OnChangeRegion(params object[] param)
    {
        string regionIndex = (string)param[0];

        InfoSO info = UIElementReference.Instance.m_LoungeInfoSo;

        m_isCityMapPanelActive = false;
        if (m_is360FristTime)
        {
            UIElementReference.Instance.m_360teachingPanel.SetActive(true);
            UIElementReference
                .Instance.m_OkButton.GetComponent<Button>()
                .onClick.AddListener(
                    delegate
                    {
                        switch (regionIndex)
                        {
                            case "10":
                                //GameEventReference.Instance.OnInteractInfo.Trigger(m_TheDeckInfo);
                                break;
                            case "11":
                                GameEventReference.Instance.OnInteractInfo.Trigger(m_TheWingInfo);
                                break;
                            case "12":
                                GameEventReference.Instance.OnInteractInfo.Trigger(m_TheWingInfo);
                                break;
                            case "13":
                                GameEventReference.Instance.OnInteractInfo.Trigger(m_ThePierInfo);
                                break;
                            case "14":
                                GameEventReference.Instance.OnInteractInfo.Trigger(m_ThePierInfo);
                                break;
                        }
                    }
                );
            m_is360FristTime = false;
        }
        else
        {
            switch (regionIndex)
            {
                case "10":
                    //GameEventReference.Instance.OnInteractInfo.Trigger(m_TheDeckInfo);
                    break;
                case "11":
                case "12":
                    GameEventReference.Instance.OnInteractInfo.Trigger(m_TheWingInfo);
                    break;
                case "13":
                case "14":
                    GameEventReference.Instance.OnInteractInfo.Trigger(m_ThePierInfo);
                    break;
            }
        }
    }

    private void OnLanguageChanged(params object[] param)
    {
        int language = (int)param[0];
        m_currentLanguage = language;
        HiedLanguageButton();
        
        UIElementReference.Instance.m_resetTaskButtonText.text = language switch
        {
            Class_Language.English => "Reset Task",
            Class_Language.SimplifiedChinese => "重置任务",
            Class_Language.TraditionalChinese => "重置任務",
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private void HiedLanguageButton()
    {
        for (int i = 0; i < UIElementReference.Instance.m_ButtonSC.Count; i++)
        {
            UIElementReference.Instance.m_ButtonENG[i].GetComponent<TMP_Text>().color = new Color(
                1,
                1,
                1,
                1f
            );
            UIElementReference.Instance.m_ButtonSC[i].GetComponent<TMP_Text>().color = new Color(
                1,
                1,
                1,
                1f
            );
            UIElementReference.Instance.m_ButtonTC[i].GetComponent<TMP_Text>().color = new Color(
                1,
                1,
                1,
                1f
            );
            switch (m_currentLanguage)
            {
                case Class_Language.English:
                    UIElementReference.Instance.m_ButtonENG[i].GetComponent<TMP_Text>().color =
                        new Color(0, 0, 0, 0.5f);
                    break;
                case Class_Language.SimplifiedChinese:
                    UIElementReference.Instance.m_ButtonSC[i].GetComponent<TMP_Text>().color =
                        new Color(0, 0, 0, 0.5f);
                    break;
                case Class_Language.TraditionalChinese:
                    UIElementReference.Instance.m_ButtonTC[i].GetComponent<TMP_Text>().color =
                        new Color(0, 0, 0, 0.5f);
                    break;
            }
        }
    }

    private void OnGameReset(params object[] param)
    {
        GameEventReference.Instance.OnEnter360Mode.Trigger();

        UIElementReference.Instance.m_homeButton.SetActive(false);
        UIElementReference.Instance.m_CityMapPanel.SetActive(true);

        UIElementReference.Instance.m_FloorPlanButton.SetActive(false);
        UIElementReference.Instance.m_GameModeSwitcher.SetActive(false);
        UIElementReference.Instance.m_InfoPanel.SetActive(false);
        UIElementReference.Instance.m_exitNavigateButton.SetActive(false);
        
        UIElementReference.Instance.m_resetTaskButton.gameObject.SetActive(true);
        UIElementReference.Instance.m_resetTaskButtonText.text = GetCurrentLanguage() switch
        {
            Class_Language.English => "Reset Task",
            Class_Language.SimplifiedChinese => "重置任务",
            Class_Language.TraditionalChinese => "重置任務",
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public bool IsCityMapPanelActive() => m_isCityMapPanelActive;

    public int GetCurrentMode() => m_CurrentMode;

    public int GetCurrentLanguage() => m_currentLanguage;

    public bool GetIsFirstEnter() => m_is360FristTime;
}

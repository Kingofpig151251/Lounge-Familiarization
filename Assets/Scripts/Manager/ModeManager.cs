using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ModeManager : Singleton<ModeManager>
{
    public int m_CurrentMode { get; private set; }
    private Color m_inactiveColor = Color.gray;
    private Color m_activeColor = new Color(0, 0.2941177f, 0.3137255f, 255);
    public bool isFloorPlanPanel = false;

    override protected void Init()
    {
        m_CurrentMode = PlayMode.ViewMode;
    }

    private void Start()
    {
        GameEventReference.Instance.OnEnter360Mode.AddListener(Show360ModePanel);
        GameEventReference.Instance.OnEnterTaskMode.AddListener(OnEnterTaskMode);
        GameEventReference.Instance.OnClickInformationButton.AddListener(OnClickInformationButton);
    }

    private void SwitchMode(int mode)
    {
        m_CurrentMode = mode;

        bool isViewMode = m_CurrentMode == PlayMode.ViewMode;
        bool isTaskMode = m_CurrentMode == PlayMode.TaskMode;

        UIElementReference.Instance.m_InfoPanel.SetActive(isViewMode);
        UIElementReference.Instance.m_InformationButton.SetActive(isViewMode);

        UIElementReference.Instance.m_360ButtonText.GetComponentInChildren<TMP_Text>().color = isViewMode?m_inactiveColor:m_activeColor;
        UIElementReference.Instance.m_TaskButtonText.GetComponentInChildren<TMP_Text>().color = isTaskMode ? m_inactiveColor : m_activeColor;

        NavigateManager.Instance.m_isEnterNavigatePhase = isTaskMode;
        UIElementReference.Instance.m_confirmNavigateButton.gameObject.SetActive(isTaskMode);
        UIElementReference.Instance.m_exitNavigateButton.gameObject.SetActive(isTaskMode);
        UIElementReference.Instance.m_navigatePanel.gameObject.SetActive(isTaskMode);

        UIElementReference.Instance.m_taskList.SetActive(isTaskMode);
    }

    private void Show360ModePanel(params object[] param)
    {
        SwitchMode(PlayMode.ViewMode);
    }

    private void OnEnterTaskMode(params object[] param)
    {
        SwitchMode(PlayMode.TaskMode);
    }

    private void OnClickInformationButton(params object[] param)
    {
        isFloorPlanPanel = !isFloorPlanPanel;
        if (isFloorPlanPanel)
        {
            UIElementReference.Instance.m_FloorPlanPanel.SetActive(true);
            UIElementReference.Instance.m_FloorPlanButtonText.GetComponent<TMP_Text>().text = "Back";
            UIElementReference.Instance.m_InfoPanel.SetActive(false);
        }
        else
        {
            UIElementReference.Instance.m_FloorPlanPanel.SetActive(false);
            UIElementReference.Instance.m_FloorPlanButtonText.GetComponent<TMP_Text>().text = "Map";
            UIElementReference.Instance.m_InfoPanel.SetActive(true);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ModeManager : Singleton<ModeManager>
{

    public string m_CurrentMode = "360";
    private Color m_color1;
    public bool isFloorPlanPanel = false;


    // Start is called before the first frame update
    void Start()
    {
        m_color1 = new Color(0, 0.2941177f, 0.3137255f, 255);
        GameEventReference.Instance.OnEnter360Mode.AddListener(Show360ModePanel);
        GameEventReference.Instance.OnEnterTaskMode.AddListener(ShowTaskModePanel);
        GameEventReference.Instance.OnClickInformationButton.AddListener(ShowFloorPlanPanel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Show360ModePanel(params object[] param)
    {
        m_CurrentMode = "360";
        UIElementReference.Instance.m_TopBar.SetActive(true);
        UIElementReference.Instance.m_InfoPanel.SetActive(true);
        UIElementReference.Instance.m_InformationButton.SetActive(true);


        UIElementReference.Instance.m_360ButtonText.GetComponentInChildren<TMP_Text>().color = Color.gray;
        UIElementReference.Instance.m_TaskButtonText.GetComponentInChildren<TMP_Text>().color = m_color1;

        NavigateManager.Instance.m_isEnterNavigatePhase = false;
        UIElementReference.Instance.m_confirmNavigateButton.gameObject.SetActive(false);
        UIElementReference.Instance.m_exitNavigateButton.gameObject.SetActive(false);
    }
    private void ShowTaskModePanel(params object[] param)
    {
        m_CurrentMode = "Task";
        UIElementReference.Instance.m_TopBar.SetActive(true);
        UIElementReference.Instance.m_InfoPanel.SetActive(false);
        UIElementReference.Instance.m_InformationButton.SetActive(false);


        GameEventReference.Instance.OnEnterNavigatePhase.Trigger();

        UIElementReference.Instance.m_360ButtonText.GetComponentInChildren<TMP_Text>().color = m_color1;
        UIElementReference.Instance.m_TaskButtonText.GetComponentInChildren<TMP_Text>().color = Color.gray;
    }

    private void ShowFloorPlanPanel(params object[] param)
    {
        if (isFloorPlanPanel == false)
        {
            UIElementReference.Instance.m_FloorPlanPanel.SetActive(true);
            isFloorPlanPanel = true;
        }
        else
        {
            UIElementReference.Instance.m_FloorPlanPanel.SetActive(false);
            isFloorPlanPanel = false;
        }
    }

}

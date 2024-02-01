using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorPlanManager : Singleton<FloorPlanManager>
{
    private bool m_isFloorPlanPanelActive = false;

    private int m_currentViewPointIndex = 0;

    private void Start()
    {
        SetUpListeners();
    }

    private void SetUpListeners()
    {
        GameEventReference.Instance.OnEnterViewPoint.AddListener(OnEnterViewPoint);
        GameEventReference.Instance.OnEnterTaskMode.AddListener(OnEnterTaskMode);
        GameEventReference.Instance.OnClickFloorPlanButton.AddListener(OnClickFloorPlanButton);
        GameEventReference.Instance.OnGameReset.AddListener(OnGameReset);
    }

    private void OnEnterViewPoint(params object[] param)
    {
        int viewPointIndex = (int)param[0];
        UIElementReference.Instance.m_floorPlan_LocationButton[m_currentViewPointIndex].GetComponent<Image>().sprite = UIElementReference.Instance.m_locationButton;
        UIElementReference.Instance.m_floorPlan_LocationButton[viewPointIndex].GetComponent<Image>().sprite = UIElementReference.Instance.m_activeLocationButton;
        m_currentViewPointIndex = viewPointIndex;
        m_isFloorPlanPanelActive = false;
    }

    private void OnClickFloorPlanButton(params object[] param)
    {
        m_isFloorPlanPanelActive = !m_isFloorPlanPanelActive;
        if (m_isFloorPlanPanelActive)
        {
            UIElementReference.Instance.m_FloorPlanPanel.SetActive(true);
            UIElementReference.Instance.m_InfoPanel.SetActive(false);
        }
        else
        {
            UIElementReference.Instance.m_FloorPlanPanel.SetActive(false);
            UIElementReference.Instance.m_InfoPanel.SetActive(true);
        }


    }

    private void OnEnterTaskMode(params object[] param)
    {
        m_isFloorPlanPanelActive = false;
        UIElementReference.Instance.m_FloorPlanPanel.SetActive(false);
    }

    public void OnGameReset(params object[] param)
    {
        m_isFloorPlanPanelActive = false;
        UIElementReference.Instance.m_FloorPlanPanel.SetActive(false);
    }

    public bool IsFloorPlanPanelActive() => m_isFloorPlanPanelActive;
}

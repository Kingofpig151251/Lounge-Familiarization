using System.Collections.Generic;
using Reference;
using UnityEngine;
using UnityEngine.UI;

public class FloorPlanManager : Singleton<FloorPlanManager>
{
    private bool m_isFloorPlanPanelActive = false;
    private int m_currentViewPointIndex = 0;

    private void Start()
    {
        RegisterEvents();
    }

    private void RegisterEvents()
    {
        GameEventReference.Instance.OnEnterViewPoint.AddListener(OnEnterViewPoint);
        GameEventReference.Instance.OnEnterTaskMode.AddListener(OnEnterTaskMode);
        GameEventReference.Instance.OnClickFloorPlanButton.AddListener(OnClickFloorPlanButton);
        GameEventReference.Instance.OnGameReset.AddListener(OnGameReset);
    }

    private void OnEnterViewPoint(params object[] param)
    {
        int viewPointIndex = (int)param[0] + ViewPointReference.Instance.m_loungeStartIndex[(int)ViewPointManager.Instance.m_currentLounge];
        UIElementReference.Instance.m_floorPlan_LocationButton[m_currentViewPointIndex].GetComponent<Image>().sprite =
            UIElementReference.Instance.m_locationButton;
        UIElementReference.Instance.m_floorPlan_LocationButton[viewPointIndex].GetComponent<Image>().sprite =
            UIElementReference.Instance.m_activeLocationButton;
        m_currentViewPointIndex = viewPointIndex;
        m_isFloorPlanPanelActive = false;
    } 

    private void OnClickFloorPlanButton(params object[] param)
    {
        ToggleFloorPlanPanel();
        UpdateFloorPlanImage();
    }

    private void ToggleFloorPlanPanel()
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

    private void UpdateFloorPlanImage()
    {
        SetListActiveFalse(UIElementReference.Instance.m_FloorPlanImage);

        switch (ViewPointManager.Instance.m_currentLounge)
        {
            case Lounge.DeckBusinessLounge:
                UIElementReference.Instance.m_FloorPlanImage[0].SetActive(true);
                break;
            case Lounge.WingFristClassLounge:
                UIElementReference.Instance.m_FloorPlanImage[1].SetActive(true);
                break;
            case Lounge.WingBusinessLounge:
                UIElementReference.Instance.m_FloorPlanImage[2].SetActive(true);
                break;
            case Lounge.PierFirstClassLounge:
                UIElementReference.Instance.m_FloorPlanImage[3].SetActive(true);
                break;
            case Lounge.PierBusinessLounge:
                UIElementReference.Instance.m_FloorPlanImage[4].SetActive(true);
                break;
        }
    }

    private void SetListActiveFalse(List<GameObject> gameObjectList) // set all obj' active in a list to false.
    {
        foreach (GameObject gameObject in gameObjectList)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnterTaskMode(params object[] param)
    {
        m_isFloorPlanPanelActive = false;
        UIElementReference.Instance.m_FloorPlanPanel.SetActive(false);
    }

    private void OnGameReset(params object[] param)
    {
        m_isFloorPlanPanelActive = false;
        UIElementReference.Instance.m_FloorPlanPanel.SetActive(false);
    }

    public bool IsFloorPlanPanelActive() => m_isFloorPlanPanelActive;
}
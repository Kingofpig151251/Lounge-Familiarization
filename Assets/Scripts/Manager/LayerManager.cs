using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManager : Singleton<LayerManager>
{
    public bool m_isLayerActive = false;

    private void Start()
    {
        SetUpListeners();
    }

    private void SetUpListeners()
    {
        GameEventReference.Instance.OnChangeRegion.AddListener(OnClickRegion);
        GameEventReference.Instance.OnEnterViewPoint.AddListener(OnEnterViewPoint);
    }

    private void OnClickRegion(params object[] param)
    {
        string regionID = (string)param[0];


        if (regionID.Length == 1)
        {
            UIElementReference.Instance.m_WorldMapPanel.SetActive(false);
            UIElementReference.Instance.m_CityMapPanel.SetActive(true);
            //if (regionID == "0")
            //{
            //    UIElementReference.Instance.m_Layer2hongKongBlock.SetActive(false);
            //    UIElementReference.Instance.m_Layer2sheKouBlock.SetActive(true);
            //}
            //else if (regionID == "1")
            //{
            //    UIElementReference.Instance.m_Layer2hongKongBlock.SetActive(true);
            //    UIElementReference.Instance.m_Layer2sheKouBlock.SetActive(false);
            //}
        }
        if (regionID.Length == 2)
        {
            UIElementReference.Instance.m_TopBar.SetActive(true);
            UIElementReference.Instance.m_CityMapPanel.SetActive(false);
            GameEventReference.Instance.OnEnter360Mode.Trigger();
        }
    }

    public void OnEnterViewPoint(params object[] param)
    {
        GameEventReference.Instance.OnClickInformationButton.Trigger();
        UIElementReference.Instance.m_InfoPanel.SetActive(false);
        if (ModeManager.Instance.m_CurrentMode != PlayMode.TaskMode)
        {
            GameEventReference.Instance.OnEnter360Mode.Trigger();
        }
    }

}

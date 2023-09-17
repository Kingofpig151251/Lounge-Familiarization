using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElementSystem : MonoBehaviour
{
    public void BackToPreviousLayer(int layer)
    {
        string regionIndex = GridManager.Instance.m_regionIndex;
        if (regionIndex.Length == 1)
        {
            GameEventReference.Instance.OnChangeRegion.Trigger("");
        }
        else
        {
            GameEventReference.Instance.OnChangeRegion.Trigger(GridManager.Instance.m_regionIndex.Substring(0, regionIndex.Length - 1));
        }

        switch (layer)
        {
            case 1:
                UIElementReference.Instance.m_layerPanel2.SetActive(false);
                UIElementReference.Instance.m_layerPanel1.SetActive(true);
                break;
            case 2:
                UIElementReference.Instance.m_layerPanel3.SetActive(false);
                UIElementReference.Instance.m_layerPanel2.SetActive(true);
                break;
        }
    }

    public void ShowMapLayer(int id)
    {    
        if (NavigateManager.Instance.m_isEnterNavigatePhase || TestPanelManager.Instance.m_isQuestionPanelActive)
            return;
        LayerManager.Instance.m_isLayerActive = true;
        switch (id)
        {
            case 1:
                UIElementReference.Instance.m_layerPanel1.SetActive(true);
                break;
            case 2:
                UIElementReference.Instance.m_layerPanel2.SetActive(true);
                break;
            case 3:
                UIElementReference.Instance.m_layerPanel3.SetActive(true);
                break;
        }
        UIElementReference.Instance.m_infoPanel.SetActive(true);
    }

    public void OnClickNextQuestion()
    {
        TestPanelManager.Instance.m_isQuestionPanelActive = false;
        GameEventReference.Instance.OpenTestPanel.Trigger();
    }
    public void EnterViewPoint(int index) => GameEventReference.Instance.OnEnterViewPoint.Trigger(index);
    public void OnClickInfoButton(string message) => GameEventReference.Instance.OnInteractUIMessage.Trigger(message);
    public void OnClickInfoExpandButton() => GameEventReference.Instance.OnClickInfoExpandButton.Trigger();
    public void OnClickRegion(string regionID) => GameEventReference.Instance.OnChangeRegion.Trigger(regionID);
    public void OnCloseMessagePanel() => UIElementReference.Instance.m_messagePanel.SetActive(false);
    public void OnClickLoungeHeader(int index) => GameEventReference.Instance.OnClickLoungeHeader.Trigger(index);
    public void OnClickClassHeader(int index) => GameEventReference.Instance.OnClickClassHeader.Trigger(index);
    public void OpenTestPanel() => GameEventReference.Instance.OpenTestPanel.Trigger();
    public void OnClickTestOption(int index) => GameEventReference.Instance.OnClickTestOption.Trigger(index);
    public void OnEnterNavigatePhase() => GameEventReference.Instance.OnEnterNavigatePhase.Trigger();
    public void OnExitNavigatePhase() => GameEventReference.Instance.OnExitNavigatePhase.Trigger();
    public void OnConfirmNavigate() => GameEventReference.Instance.OnConfirmNavigate.Trigger();
}

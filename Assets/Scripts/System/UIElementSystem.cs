using Manager;
using Reference;
using UnityEngine;

public class UIElementSystem : MonoBehaviour
{
    #region Navigation

    public void EnterViewPoint(int index) =>
        GameEventReference.Instance.OnEnterViewPoint.Trigger(index);

    public void OnEnterNavigatePhase() =>
        GameEventReference.Instance.OnEnterNavigatePhase.Trigger();

    public void OnExitNavigatePhase() => GameEventReference.Instance.OnExitNavigatePhase.Trigger();

    public void OnConfirmNavigate() => GameEventReference.Instance.OnConfirmNavigate.Trigger();

    #endregion

    #region Language

    public void OnLanguageChanged(int language)
    {
        if (
            IntroducePanelManager.Instance.GetIsCoroutineRunning()
            || GameManager.Instance.GetCurrentLanguage() == language
        )
            return;
        GameEventReference.Instance.OnLanguageChanged.Trigger(language);
    }

    #endregion

    #region Panel

    public void ShowMapLayer(int id)
    {
        //if (TestPanelManager.Instance.m_isQuestionPanelActive)
        //    return;
        LayerManager.Instance.m_isLayerActive = true;
        switch (id)
        {
            case 1:
                UIElementReference.Instance.m_CityMapPanel.SetActive(true);
                break;
            case 2:
                UIElementReference.Instance.m_FloorPlanPanel.SetActive(true);
                break;
        }

        UIElementReference.Instance.m_InfoPanel.SetActive(true);
        UIElementReference.Instance.m_InfoPanel.SetActive(false);
    }

    public void OnClickNextQuestion()
    {
        //TestPanelManager.Instance.m_isQuestionPanelActive = false;
        GameEventReference.Instance.OpenTestPanel.Trigger();
    }

    public void OnGameReset() => GameEventReference.Instance.OnGameReset.Trigger();

    public void OnClickTaskModeButton(string message) =>
        GameEventReference.Instance.OnEnterTaskMode.Trigger(message);

    public void OnClickInfoExpandButton() =>
        GameEventReference.Instance.OnClickInfoExpandButton.Trigger();

    public void OnClickRegion(string regionID) =>
        GameEventReference.Instance.OnChangeRegion.Trigger(regionID);

    public void OnCloseMessagePanel() =>
        UIElementReference.Instance.m_MessagePanel.SetActive(false);

    public void OnClickLoungeHeader(int index) =>
        GameEventReference.Instance.OnClickLoungeHeader.Trigger(index);

    public void OnClickClassHeader(int index) =>
        GameEventReference.Instance.OnClickClassHeader.Trigger(index);

    public void OpenTestPanel() => GameEventReference.Instance.OpenTestPanel.Trigger();

    public void OnClickTestOption(int index) =>
        GameEventReference.Instance.OnClickTestOption.Trigger(index);

    public void OnClickFloorPlanButton() =>
        GameEventReference.Instance.OnClickFloorPlanButton.Trigger();

    public void OnClickIntroducePanelNextButton() =>
        GameEventReference.Instance.OnClickIntroducePanelNextButton.Trigger();

    public void OnClickFeatureListExpand(ViewPoint vp) =>
        GameEventReference.Instance.OnFeaturePointListExpandButtonClicked.Trigger(vp);

    public void Deactive(GameObject gameObject) => gameObject.SetActive(false);

    #endregion

    public void OnClickNeedAnswerButton()
    {
        GameEventReference.Instance.OnEnterViewPoint.Trigger(
            NavigateManager.Instance.GetCurrentTaskSO().m_navigateIndex[0]
        );
        UIElementReference.Instance.m_nextTaskButton.SetActive(true);
        UIElementReference.Instance.m_confirmButton.SetActive(false);
    }

    public void SkipIntroduce()
    {
        UIElementReference.Instance.m_IntroducePanel.SetActive(false);
        UIElementReference.Instance.m_CityMapPanel.SetActive(true);
    }

    public void ClickNextTaskButton()
    {
        NavigateManager.Instance.GenerateTask();
        UIElementReference.Instance.m_nextTaskButton.SetActive(false);
        UIElementReference.Instance.m_confirmButton.SetActive(true);
    }

    public void DelectPage(GameObject page)
    {
        Destroy(page);
    }

    public void ShowLoungeInformation(LoungeInfoSO loungeInfoSO)
    {
        UIElementReference.Instance.m_loungeInformationPanel.SetActive(true);
        GameEventReference.Instance.OnShowLoungeInfo.Trigger(loungeInfoSO);
    }

    public void CloseLoungeInformation()
    {
        GameEventReference.Instance.OnCloseLoungeInfo.Trigger();
    }
}

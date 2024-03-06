using Reference;
using UnityEngine.UI;
using TMPro;

namespace Manager
{
    public class FeaturePointWindowManager : Singleton<IntroducePanelManager>
    {
        // Active status of the window
        private bool m_actived = false;
        private ViewPoint m_currentViewPoint;
        private int m_currentLanguage = Class_Language.English;

        private void Start()
        {
            RegisterEvents();
        }

        // Register event listeners
        private void RegisterEvents()
        {
            GameEventReference.Instance.OnFeaturePointListExpandButtonClicked.AddListener(
                OnFeaturePointListExpandButtonClicked);
            GameEventReference.Instance.OnLanguageChanged.AddListener(OnLanguageChanged);
            GameEventReference.Instance.OnGameReset.AddListener(OnGameReset);
        }

        // Handle feature point list expand button clicked event
        private void OnFeaturePointListExpandButtonClicked(params object[] param)
        {
            m_currentViewPoint = (ViewPoint)param[0];
            UIElementReference.Instance.m_popupWindow.SetActive(m_actived = true);
            UpdatePopupWindowText();
            SetupPopupWindowButtons();
        }

        // Update the text of the popup window
        private void UpdatePopupWindowText()
        {
            switch (m_currentLanguage)
            {
                case Class_Language.English:
                    UIElementReference.Instance.m_popupWindowTitle.GetComponent<TMP_Text>().text =
                        m_currentViewPoint.m_infoSO[0].m_title_ENG;
                    UIElementReference.Instance.m_popupWindowMessage.GetComponent<TMP_Text>().text =
                        m_currentViewPoint.m_infoSO[0].m_content_ENG;
                    break;
                case Class_Language.SimplifiedChinese:
                    UIElementReference.Instance.m_popupWindowTitle.GetComponent<TMP_Text>().text =
                        m_currentViewPoint.m_infoSO[0].m_title_SC;
                    UIElementReference.Instance.m_popupWindowMessage.GetComponent<TMP_Text>().text =
                        m_currentViewPoint.m_infoSO[0].m_content_SC;
                    break;
                case Class_Language.TraditionalChinese:
                    UIElementReference.Instance.m_popupWindowTitle.GetComponent<TMP_Text>().text =
                        m_currentViewPoint.m_infoSO[0].m_title_TC;
                    UIElementReference.Instance.m_popupWindowMessage.GetComponent<TMP_Text>().text =
                        m_currentViewPoint.m_infoSO[0].m_content_TC;
                    break;
            }
        }

        // Setup the buttons of the popup window
        private void SetupPopupWindowButtons()
        {
            UIElementReference.Instance.m_popupWindowEnterButton.GetComponent<Button>().onClick.RemoveAllListeners();
            UIElementReference.Instance.m_popupWindowExitButton.GetComponent<Button>().onClick.RemoveAllListeners();

            UIElementReference.Instance.m_popupWindowEnterButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                GameEventReference.Instance.OnClickInfoExpandButton.Trigger();
                ViewPointManager.Instance.m_currentLounge = m_currentViewPoint.m_loungeName;
                GameEventReference.Instance.OnEnterViewPoint.Trigger(m_currentViewPoint.m_index);
                UIElementReference.Instance.m_popupWindow.SetActive(m_actived = false);

                CameraController.Instance.SetRotation(m_currentViewPoint.m_infoSO[0].m_position);
            });
            UIElementReference.Instance.m_popupWindowExitButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                UIElementReference.Instance.m_popupWindow.SetActive(m_actived = false);
            });
        }

        // Handle language changed event
        private void OnLanguageChanged(params object[] param)
        {
            int language = (int)param[0];
            m_currentLanguage = language;

            if (m_actived)
            {
                RefreshDisplay();
            }
        }

        // Refresh the display of the popup window
        private void RefreshDisplay()
        {
            UpdatePopupWindowText();
        }

        // Handle game reset event
        private void OnGameReset(params object[] param)
        {
            UIElementReference.Instance.m_popupWindow.SetActive(m_actived = false);
        }
    }
}
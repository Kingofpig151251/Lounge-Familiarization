using UnityEngine.UI;
using TMPro;

namespace Manager
{
    public class FeaturePointWindowManager : Singleton<IntroducePanelManager>
    {
        private bool m_actived = false;

        private ViewPoint m_currentViewPoint;
        private int m_currentLanguage = Class_Language.English;

        private void Start()
        {
            GameEventReference.Instance.OnFeaturePointListExpandButtonClicked.AddListener(OnFeaturePointListExpandButtonClicked);
            GameEventReference.Instance.OnLanguageChanged.AddListener(OnLanguageChanged);
            GameEventReference.Instance.OnGameReset.AddListener(OnGameReset);
        }

        private void OnGameReset(params object[] param)
        {
            UIElementReference.Instance.m_popupWindow.SetActive(m_actived = false);
        }

        private void OnFeaturePointListExpandButtonClicked(params object[] param)
        {
            m_currentViewPoint = (ViewPoint)param[0];

            UIElementReference.Instance.m_popupWindow.SetActive(m_actived = true);

            switch (m_currentLanguage)
            {
                case Class_Language.English:
                    UIElementReference.Instance.m_popupWindowTitle.GetComponent<TMP_Text>().text = m_currentViewPoint.m_infoSO[0].m_title_ENG;
                    UIElementReference.Instance.m_popupWindowMessage.GetComponent<TMP_Text>().text = m_currentViewPoint.m_infoSO[0].m_content_ENG;
                    break;
                case Class_Language.SimplifiedChinese:
                    UIElementReference.Instance.m_popupWindowTitle.GetComponent<TMP_Text>().text = m_currentViewPoint.m_infoSO[0].m_title_SC;
                    UIElementReference.Instance.m_popupWindowMessage.GetComponent<TMP_Text>().text = m_currentViewPoint.m_infoSO[0].m_content_SC;
                    break;
                case Class_Language.TraditionalChinese:
                    UIElementReference.Instance.m_popupWindowTitle.GetComponent<TMP_Text>().text = m_currentViewPoint.m_infoSO[0].m_title_TC;
                    UIElementReference.Instance.m_popupWindowMessage.GetComponent<TMP_Text>().text = m_currentViewPoint.m_infoSO[0].m_content_TC;
                    break;
            }

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

        private void OnLanguageChanged(params object[] param)
        {
            int language = (int)param[0];
            m_currentLanguage = language;

            if (m_actived)
            {
                RefreshDisplay();
            }
        }

        private void RefreshDisplay()
        {
            switch (m_currentLanguage)
            {
                case Class_Language.English:
                    UIElementReference.Instance.m_popupWindowTitle.GetComponent<TMP_Text>().text = m_currentViewPoint.m_infoSO[0].m_title_ENG;
                    UIElementReference.Instance.m_popupWindowMessage.GetComponent<TMP_Text>().text = m_currentViewPoint.m_infoSO[0].m_content_ENG;
                    break;
                case Class_Language.SimplifiedChinese:
                    UIElementReference.Instance.m_popupWindowTitle.GetComponent<TMP_Text>().text = m_currentViewPoint.m_infoSO[0].m_title_SC;
                    UIElementReference.Instance.m_popupWindowMessage.GetComponent<TMP_Text>().text = m_currentViewPoint.m_infoSO[0].m_content_SC;
                    break;
                case Class_Language.TraditionalChinese:
                    UIElementReference.Instance.m_popupWindowTitle.GetComponent<TMP_Text>().text = m_currentViewPoint.m_infoSO[0].m_title_TC;
                    UIElementReference.Instance.m_popupWindowMessage.GetComponent<TMP_Text>().text = m_currentViewPoint.m_infoSO[0].m_content_TC;
                    break;
            }
        }
    }
}

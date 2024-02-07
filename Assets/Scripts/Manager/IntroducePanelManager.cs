using TMPro;

namespace Manager
{
    public class IntroducePanelManager : Singleton<IntroducePanelManager>
    {
        public TMP_Text m_DialougueDisplay;

        private DialogueSO m_currentDialogue;
        private int m_currentDialogueIndex;
        private int m_currentLanguage = Class_Language.English;

        private void Start()
        {
            GameEventReference.Instance.OnClickNextButton.AddListener(OnClickNextButton);
            GameEventReference.Instance.OnLanguageChanged.AddListener(OnLanguageChanged);

            m_currentDialogue = UIElementReference.Instance.m_dialogueList[0];
            RefreshDisplay();
        }

        private void OnClickNextButton(params object[] param)
        {
            var list = UIElementReference.Instance.m_dialogueList;
            m_currentDialogueIndex++;
            if (m_currentDialogueIndex == list.Count)
            {
                UIElementReference.Instance.m_IntroducePanel.SetActive(false);
                UIElementReference.Instance.m_CityMapPanel.SetActive(true);
                m_currentDialogueIndex = 0;
            }
            else
            {
                m_currentDialogue = UIElementReference.Instance.m_dialogueList[m_currentDialogueIndex];
                RefreshDisplay();
            }
        }

        private void OnLanguageChanged(params object[] param)
        {
            int language = (int)param[0];
            m_currentLanguage = language;
            RefreshDisplay();
        }

        private void RefreshDisplay()
        {
            switch (m_currentLanguage)
            {
                case Class_Language.English:
                    m_DialougueDisplay.text = m_currentDialogue.m_messageEng;
                    break;
                case Class_Language.SimplifiedChinese:
                    m_DialougueDisplay.text = m_currentDialogue.m_messageSC;
                    break;
                case Class_Language.TraditionalChinese:
                    m_DialougueDisplay.text = m_currentDialogue.m_messageTC;
                    break;
            }
        }
    }
}

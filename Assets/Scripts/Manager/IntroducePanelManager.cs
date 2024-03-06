using System.Collections;
using Reference;
using TMPro;
using UnityEngine;

namespace Manager
{
    public class IntroducePanelManager : Singleton<IntroducePanelManager>
    {
        public TMP_Text m_DialougueDisplay;

        private DialogueSO m_currentDialogue;
        private int m_currentDialogueIndex;
        private int m_currentLanguage = Class_Language.English;
        private bool m_isCoroutineRunning = false;
        [SerializeField] private float delay = 0.01f; // adjust the delay for the text to appear speed

        private void Start()
        {
            GameEventReference.Instance.OnClickIntroducePanelNextButton.AddListener(OnClickNextButton);
            GameEventReference.Instance.OnLanguageChanged.AddListener(OnLanguageChanged);

            SetCurrentDialogue(UIElementReference.Instance.m_dialogueList[0]);
            StartCoroutine(RefreshDisplay());
        }

        // Coroutine for displaying the dialogue letter by letter
        private IEnumerator RefreshDisplay()
        {
            m_isCoroutineRunning = true;

            string dialogue = GetCurrentDialogueText();

            m_DialougueDisplay.text = string.Empty;
            foreach (char letter in dialogue.ToCharArray())
            {
                m_DialougueDisplay.text += letter;
                yield return new WaitForSeconds(delay);
            }

            m_isCoroutineRunning = false;
        }

        // Update the dialogue and start a new coroutine when the next button is clicked
        private void OnClickNextButton(params object[] param)
        {
            if (m_isCoroutineRunning)
                return;

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
                SetCurrentDialogue(UIElementReference.Instance.m_dialogueList[m_currentDialogueIndex]);
                StartCoroutine(RefreshDisplay());
            }
        }

        // Update the dialogue and start a new coroutine when the language changes
        private void OnLanguageChanged(params object[] param)
        {
            if (m_isCoroutineRunning)
                return;
            SetCurrentLanguage((int)param[0]);
            StartCoroutine(RefreshDisplay());
        }


        // Set the current dialogue
        private void SetCurrentDialogue(DialogueSO dialogue)
        {
            m_currentDialogue = dialogue;
        }

        // Set the current language
        private void SetCurrentLanguage(int language)
        {
            m_currentLanguage = language;
        }

        // Get the current dialogue text based on the current language
        private string GetCurrentDialogueText()
        {
            switch (m_currentLanguage)
            {
                case Class_Language.English:
                    return m_currentDialogue.m_messageEng;
                case Class_Language.SimplifiedChinese:
                    return m_currentDialogue.m_messageSC;
                case Class_Language.TraditionalChinese:
                    return m_currentDialogue.m_messageTC;
                default:
                    return string.Empty;
            }
        }
    }
}
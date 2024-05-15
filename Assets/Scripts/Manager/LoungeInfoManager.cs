using Reference;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LoungeInfoManager : MonoBehaviour
{
    private LoungeInfoSO m_currentLoungeInfoSO;

    private void Start()
    {
        RegisterEvents();
    }

    private void RegisterEvents()
    {
        GameEventReference.Instance.OnShowLoungeInfo.AddListener(OnShowLoungeInfo);
        GameEventReference.Instance.OnCloseLoungeInfo.AddListener(OnCloseLoungeInfo);
        GameEventReference.Instance.OnLanguageChanged.AddListener(OnLanguageChanged);
    }

    private void OnShowLoungeInfo(params object[] param)
    {
        LoungeInfoSO loungeInfoSO = (LoungeInfoSO)param[0];

        m_currentLoungeInfoSO = loungeInfoSO;

        UIElementReference.Instance.m_loungeInformationPanel.SetActive(true);
        UIElementReference.Instance.m_npcReminder.SetActive(false);
        UpdateText(GameManager.Instance.GetCurrentLanguage());
    }

    private void OnCloseLoungeInfo(params object[] param)
    {
        UIElementReference.Instance.m_loungeInformationPanel.SetActive(false);
        UIElementReference.Instance.m_npcReminder.SetActive(true);
    }

    private void OnLanguageChanged(params object[] param)
    {
        int language = (int)param[0];

        if (m_currentLoungeInfoSO)
        {
            UpdateText(language);
        }
    }

    private void UpdateText(int language)
    {
        switch (language)
        {
            case Class_Language.English:
                UIElementReference.Instance.m_loungeInfoTitle.GetComponent<TMP_Text>().text =
                    m_currentLoungeInfoSO.m_title_ENG;
                UIElementReference.Instance.m_loungeInfoContent.GetComponent<TMP_Text>().text =
                    m_currentLoungeInfoSO.m_content_ENG;
                break;
            case Class_Language.SimplifiedChinese:
                UIElementReference.Instance.m_loungeInfoTitle.GetComponent<TMP_Text>().text =
                    m_currentLoungeInfoSO.m_title_SC;
                UIElementReference.Instance.m_loungeInfoContent.GetComponent<TMP_Text>().text =
                    m_currentLoungeInfoSO.m_content_SC;
                break;
            case Class_Language.TraditionalChinese:
                UIElementReference.Instance.m_loungeInfoTitle.GetComponent<TMP_Text>().text =
                    m_currentLoungeInfoSO.m_title_TC;
                UIElementReference.Instance.m_loungeInfoContent.GetComponent<TMP_Text>().text =
                    m_currentLoungeInfoSO.m_content_TC;
                break;
        }
    }
}

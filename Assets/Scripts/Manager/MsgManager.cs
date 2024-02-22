using System.Collections;
using System.Collections.Generic;
using Reference;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MsgManager : Singleton<MsgManager>
{
    [SerializeField] private TextMeshProUGUI m_titleText;
    [SerializeField] private TextMeshProUGUI m_messageText;

    private InfoSO m_currentInfoSO;

    private void Start()
    {
        GameEventReference.Instance.OnInteractInfo.AddListener(OnInteractInfo);
        GameEventReference.Instance.OnLanguageChanged.AddListener(OnLanguageChanged);
        GameEventReference.Instance.OnGameReset.AddListener(OnGameReset);
    }

    private void OnInteractInfo(params object[] param)
    {
        InfoSO info = (InfoSO)param[0];

        m_currentInfoSO = info;

        UIElementReference.Instance.m_MessagePanel.SetActive(true);
        UpdateMSGText(GameManager.Instance.GetCurrentLanguage());
    }

    private void OnLanguageChanged(params object[] param)
    {
        int language = (int)param[0];

        if (m_currentInfoSO != null)
        {
            UpdateMSGText(language);
        }
    }

    private void UpdateMSGText(int language)
    {
        switch (language)
        {
            case Class_Language.English:
                this.m_titleText.text = m_currentInfoSO.m_title_ENG;
                this.m_messageText.text = m_currentInfoSO.m_content_ENG;
                break;
            case Class_Language.SimplifiedChinese:
                this.m_titleText.text = m_currentInfoSO.m_title_SC;
                this.m_messageText.text = m_currentInfoSO.m_content_SC;
                break;
            case Class_Language.TraditionalChinese:
                this.m_titleText.text = m_currentInfoSO.m_title_TC;
                this.m_messageText.text = m_currentInfoSO.m_content_TC;
                break;
        }
    }

    public void OnGameReset(params object[] param)
    {
        UIElementReference.Instance.m_MessagePanel.SetActive(false);
    }
}


using System.Collections;
using System.Collections.Generic;
using Reference;
using UnityEngine;
using TMPro;

public class TMPTranslater : MonoBehaviour
{
    [SerializeField] private bool m_enable = true;

    [SerializeField] private float m_englishTextMargin = 0f;
    [SerializeField] private float m_chineseTextMargin = 0f;

    [SerializeField] private bool m_updateMultiLanguageText = true;
    [SerializeField] [TextAreaAttribute] private string m_englishText;
    [SerializeField] [TextAreaAttribute] private string m_simplifiedChineseText;
    [SerializeField] [TextAreaAttribute] private string m_traditionalChineseText;

    [SerializeField] private bool m_isBold = false;

    private FontAssetReference fontRefer;


    private void Start()
    {
        if (m_enable)
        {
            SetUpListeners();
            Init();
            UpdateTMP(GameManager.Instance.GetCurrentLanguage());
        }
    }

    private void Init()
    {
        fontRefer = FontAssetReference.Instance;
    }

    private void SetUpListeners()
    {
        GameEventReference.Instance.OnLanguageChanged.AddListener(OnLanguageChanged);
    }

    private void OnLanguageChanged(params object[] param)
    {
        int language = (int)param[0];
        UpdateTMP(language);
    }

    private void UpdateTMP(int language)
    {
        switch (language)
        {
            case Class_Language.English:
                if (m_updateMultiLanguageText)
                {
                    GetComponent<TMP_Text>().text = m_englishText;
                }

                GetComponent<TMP_Text>().font = m_isBold ? fontRefer.m_englishBoldFont : fontRefer.m_englishRegularFont;
                GetComponent<TMP_Text>().characterSpacing = m_englishTextMargin;
                break;
            case Class_Language.SimplifiedChinese:
                if (m_updateMultiLanguageText)
                {
                    GetComponent<TMP_Text>().text = m_simplifiedChineseText;
                }

                GetComponent<TMP_Text>().font = m_isBold
                    ? fontRefer.m_simplifiedChineseBoldFont
                    : fontRefer.m_simplifiedChineseRegularFont;
                GetComponent<TMP_Text>().characterSpacing = m_chineseTextMargin;
                break;
            case Class_Language.TraditionalChinese:
                if (m_updateMultiLanguageText)
                {
                    GetComponent<TMP_Text>().text = m_traditionalChineseText;
                }

                GetComponent<TMP_Text>().font = m_isBold
                    ? fontRefer.m_traditionalChineseBoldFont
                    : fontRefer.m_traditionalChineseRegularFont;
                GetComponent<TMP_Text>().characterSpacing = m_chineseTextMargin;
                break;
        }
    }
}
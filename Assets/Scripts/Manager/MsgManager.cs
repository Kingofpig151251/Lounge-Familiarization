using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MsgManager : Singleton<MsgManager>
{
    [SerializeField] private TextMeshProUGUI m_titleText;
    [SerializeField] private TextMeshProUGUI m_messageText;

    private void Start()
    {
        GameEventReference.Instance.OnInteractInfo.AddListener(InteractUIMessage);
    }
    private void InteractUIMessage(params object[] param)
    {
        InfoSO info = (InfoSO)param[0];
        ;
        UIElementReference.Instance.m_MessagePanel.SetActive(true);
        this.m_titleText.text = info.m_title;
        this.m_messageText.text = info.m_content;
    }
}


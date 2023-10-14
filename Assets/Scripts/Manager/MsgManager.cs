using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MsgManager : Singleton<MsgManager>
{
    [SerializeField] private TextMeshProUGUI m_messageText;

    private void Start()
    {
        GameEventReference.Instance.OnInteractInfo.AddListener(InteractUIMessage);
        gameObject.SetActive(false);
    }
    private void InteractUIMessage(params object[] param)
    {
        gameObject.SetActive(true);
        string messageText = (string)param[0];
        this.m_messageText.text = messageText;
    }
}


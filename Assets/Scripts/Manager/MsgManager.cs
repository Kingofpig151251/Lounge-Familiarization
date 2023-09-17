using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MsgManager : Singleton<MsgManager>
{
    [SerializeField] private Text m_messageText;
    private void Start()
    {
        GameEventReference.Instance.OnInteractUIMessage.AddListener(showMessagePanel);
        gameObject.SetActive(false);
    }
    private void showMessagePanel(params object[] param)
    {
        gameObject.SetActive(true);
        string messageText = (string)param[0];
        this.m_messageText.text = messageText;
    }
}


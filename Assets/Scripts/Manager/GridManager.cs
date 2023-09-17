using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : Singleton<GridManager>
{
    [SerializeField] private GameObject m_featurePoint;

    [SerializeField] private List<GameObject> m_loungeHeader = new List<GameObject>();

    [SerializeField] private List<MessageSO> m_loungelMessageList = new List<MessageSO>();
    [SerializeField] private List<MessageSO> m_lounge2MessageList = new List<MessageSO>();
    [SerializeField] private List<MessageSO> m_lounge3MessageList = new List<MessageSO>();
    [SerializeField] private List<MessageSO> m_lounge4MessageList = new List<MessageSO>();

    private bool[] m_availableLoungeHeader = { true, true, true, true };
    private List<MessageSO>[] m_loungeMessageList = new List<MessageSO>[4];

    private List<GameObject> m_existMSGList = new List<GameObject>();

    private bool[] m_isHeaderExpanded = { false, false, false, false };

    public string m_regionIndex { get; private set; }

    private void Start()
    {
        SetUpListeners();
        foreach (GameObject message in m_loungeHeader)
        {
            m_existMSGList.Add(Instantiate(message, gameObject.transform));
        }
    }

    override protected void Init()
    {
        m_loungeMessageList[0] = m_loungelMessageList;
        m_loungeMessageList[1] = m_lounge2MessageList;
        m_loungeMessageList[2] = m_lounge3MessageList;
        m_loungeMessageList[3] = m_lounge4MessageList;
    }

    private void SetUpListeners()
    {
        GameEventReference.Instance.OnClickHeaderButton.AddListener(onClickHeaderButton);
        GameEventReference.Instance.OnChangeRegion.AddListener(OnChangeRegion);
    }

    private void onClickHeaderButton(params object[] param)
    {
        int index = (int)param[0];

        m_isHeaderExpanded[index] = !m_isHeaderExpanded[index];

        RefreshFeaturePointList();
    }

    private void RefreshFeaturePointList()
    {
        ClearFeaturePointList();

        for (int i = 0; i < m_loungeHeader.Count; i++)
        {
            if (m_availableLoungeHeader[i])
            {
                m_existMSGList.Add(Instantiate(m_loungeHeader[i], gameObject.transform));
                if (m_isHeaderExpanded[i])
                {
                    GameObject currentMSG;
                    foreach (MessageSO message in m_loungeMessageList[i])
                    {
                        m_existMSGList.Add(currentMSG = Instantiate(m_featurePoint, gameObject.transform));
                        currentMSG.GetComponent<UIElement_FeaturePoint>().m_content = message.m_info;
                    }
                }
            }
        }
    }

    private void ClearFeaturePointList()
    {
        if (m_existMSGList == null)
        {
            return;
        }

        foreach (GameObject message in m_existMSGList)
        {
            Destroy(message);
        }
        m_existMSGList.Clear();
    }

    private void OnChangeRegion(params object[] param)
    {
        string regionID = (string)param[0];

        m_regionIndex = regionID;

        for (int i = 0; i < m_availableLoungeHeader.Length; i++)
        {
            m_availableLoungeHeader[i] = false;
        }

        if (m_regionIndex == "")
        {
            m_availableLoungeHeader[0] = true;
            m_availableLoungeHeader[1] = true;
            m_availableLoungeHeader[2] = true;
            m_availableLoungeHeader[3] = true;
            RefreshFeaturePointList();
            return;
        }

        if (m_regionIndex[0] == '0')
        {
            m_availableLoungeHeader[3] = true;
        }
        else if (m_regionIndex == "1")
        {
            m_availableLoungeHeader[0] = true;
            m_availableLoungeHeader[1] = true;
            m_availableLoungeHeader[2] = true;
        }
        else if (m_regionIndex[0] == '1')
        {
            m_availableLoungeHeader[Convert.ToInt32(m_regionIndex[1].ToString(), 10)] = true;
        }
        RefreshFeaturePointList();
    }

}

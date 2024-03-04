using System.Collections;
using System.Collections.Generic;
using Reference;
using UnityEngine;

public class InterfaceItem_Info : InterfaceItem
{
    public InfoSO m_info;

    public InfoSO GetInfo() => m_info;

    public override void OnClick()
    {
        GameEventReference.Instance.OnInteractInfo.Trigger(m_info);
    }
}
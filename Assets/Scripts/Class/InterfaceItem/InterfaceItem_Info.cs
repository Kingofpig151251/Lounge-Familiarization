using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceItem_Info : InterfaceItem
{
    public string m_info;

    public string GetInfo() => m_info;

    public override void OnClick()
    {
        GameEventReference.Instance.OnInteractInfo.Trigger(m_info);
    }
}

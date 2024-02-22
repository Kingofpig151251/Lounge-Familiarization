using System.Collections;
using System.Collections.Generic;
using Reference;
using UnityEngine;

public class InterfaceItemMananger : Singleton<InterfaceItemMananger>
{
    private List<InterfaceItem> m_interfaceItemList = new List<InterfaceItem>();

    private void Start()
    {
        GameEventReference.Instance.OnInteract.AddListener(OnInteract);
    }

    private void OnInteract(params object[] param)
    {
        int index = (int)param[0];
        m_interfaceItemList[index].OnClick();
    }

    public int Register(InterfaceItem item)
    {
        m_interfaceItemList.Add(item);
        return m_interfaceItemList.IndexOf(item);
    }
}
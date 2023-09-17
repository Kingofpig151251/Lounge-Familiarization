using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceItem : MonoBehaviour
{
    private int m_index;

    private void Awake()
    {
        m_index = InterfaceItemMananger.Instance.Register(this);
    }

    public virtual void OnClick()
    {
    }

    public int GetIndex() => m_index;
}

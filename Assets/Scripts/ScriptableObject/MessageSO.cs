using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMessage", menuName = "New Message", order = 2)]
public class MessageSO : ScriptableObject
{
    public string m_info;
    public int viewPoint;
}
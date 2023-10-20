using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInfo", menuName = "New Info", order = 2)]
public class InfoSO : ScriptableObject
{
    public Vector3 m_position;
    public Vector3 m_rotation;
    public Vector3 m_size;

    public string m_title;
    public string m_content;
}
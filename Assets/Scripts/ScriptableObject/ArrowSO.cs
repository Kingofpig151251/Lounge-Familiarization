using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "NewArrow", menuName = "New Arrow", order = 1)]
public class ArrowSO : ScriptableObject
{
    public Vector3 m_position;
    public Vector3 m_rotation;
    public Vector3 m_size;

    public int m_nextViewPointIndex;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewViewPoint", menuName = "New ViewPoint", order = 0)]
public class ViewPoint : ScriptableObject
{
    public int m_index;
    public Lounge m_loungeName;
    public int[] m_relation;
    public ArrowSO[] m_arrowSO;
    public InfoSO[] m_infoSO;
    public Texture m_texture;
    public float m_rotation;
}
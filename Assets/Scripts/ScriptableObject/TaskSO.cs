using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New TaskConfigSO", menuName = "New TaskConfigSO", order = 4)]
public class TaskSO : ScriptableObject
{
    public int m_Regionid;
    [Space]
    public string m_question_TC;
    public string m_question_SC;
    public string m_question_ENG;
    public int m_navigateIndex;
}
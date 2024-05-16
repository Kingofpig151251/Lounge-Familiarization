using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New TaskConfigSO", menuName = "New TaskConfigSO", order = 4)]
public class TaskSO : ScriptableObject
{
    public ulong m_taskID;
    [Space][FormerlySerializedAs("m_Regionid")] public Lounge m_lounge;
    [Space] public string m_question_TC;
    public string m_question_SC;
    public string m_question_ENG;
    public List<int> m_navigateIndex;
}
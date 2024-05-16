using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New TaskConfigSO", menuName = "New TaskConfigSO", order = 4)]
public class TaskSO : ScriptableObject
{
    private const string TaskMask = "task-mask";
    public static int Mask => PlayerPrefs.GetInt(TaskMask);
    
    public int m_taskID;
    [Space][FormerlySerializedAs("m_Regionid")] public Lounge m_lounge;
    [Space] public string m_question_TC;
    public string m_question_SC;
    public string m_question_ENG;
    public List<int> m_navigateIndex;
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    public static void Initialize()
    {
        if (PlayerPrefs.HasKey(TaskMask)) return;
        PlayerPrefs.SetInt(TaskMask, 0);
    }
    
    public void CompleteTask()
    {
        PlayerPrefs.SetInt(TaskMask, Mask | m_taskID);
    }
    
    public bool IsCompleted()
    {
        return (Mask & m_taskID) != 0;
    }
}
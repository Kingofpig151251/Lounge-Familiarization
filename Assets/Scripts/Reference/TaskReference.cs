using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskReference : Singleton<TaskReference>
{
    public List<TaskSO> m_taskConfigSO = new List<TaskSO>();
}
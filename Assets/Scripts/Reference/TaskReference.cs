using System.Collections.Generic;

namespace Reference
{
    public class TaskReference : Singleton<TaskReference>
    {
        public List<TaskSO> m_taskConfigSO = new List<TaskSO>();
    }
}
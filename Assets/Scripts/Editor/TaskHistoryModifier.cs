using UnityEditor;
using UnityEngine;

namespace Editor
{
    public static class TaskHistoryModifier
    {
        [MenuItem("Tools/Reset tasks history")]
        private static void ResetTasksHistory()
        {
            PlayerPrefs.DeleteKey("task-mask");
        }
        
        [MenuItem("Tools/Reset QA history")]
        private static void ResetQAHistory()
        {
            PlayerPrefs.DeleteKey("qa-mask");
        }
    }
}
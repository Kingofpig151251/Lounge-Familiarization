using Reference;
using UnityEngine;
using UnityEngine.UI;

public class TaskListButton : MonoBehaviour
{
    private bool _isExpanded = true;
    
    public void OnClick()
    {
        // Determine whether to expand or collapse the task list
        if (_isExpanded) { CollapseTaskList(); }
        if (!_isExpanded) { ExpandTaskList(); }
        
        // Invert status
        _isExpanded = !_isExpanded;
    }
    
    private void CollapseTaskList()
    {
        GameEventReference.Instance.OnTaskListCollapse.Trigger();
        GetComponentInChildren<Text>().text = "Expand";
    }
    
    private void ExpandTaskList()
    {
        GameEventReference.Instance.OnTaskListExpand.Trigger();
        GetComponentInChildren<Text>().text = "Collapse";
    }
}
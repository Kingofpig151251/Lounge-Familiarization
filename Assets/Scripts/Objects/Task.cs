using UnityEngine;

// ReSharper disable once CheckNamespace
public class Task
{
    public readonly GameObject TaskListItem;
    public LanguagePlus TaskName;

    public Task(ref GameObject taskListItem, LanguagePlus taskName)
    {
        TaskListItem = taskListItem;
        TaskName = taskName;
    }
}
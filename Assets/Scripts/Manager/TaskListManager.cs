using System.Collections.Generic;
using System.Linq;
using Reference;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class TaskListManager : Singleton<TaskListManager>
{
    public int language;
    private int _taskCount;
    private List<Task> _taskList;

    protected override void Init()
    {
        _taskList = new List<Task>();
    }

    private void Start()
    {
        // Add event listeners to the UI events
        SetUpListeners();

        // Test code
        GameEventReference.Instance.OnTaskAdded.Trigger(new LanguagePlus("關掉電腦", "Turn off the computer"));
    }

    private void SetUpListeners()
    {
        GameEventReference.Instance.OnTaskListExpand.AddListener(OnTaskListExpand);
        GameEventReference.Instance.OnTaskListCollapse.AddListener(OnTaskListCollapse);
        GameEventReference.Instance.OnTaskAdded.AddListener(OnTaskAdded);
        GameEventReference.Instance.OnTaskCompleted.AddListener(OnTaskCompleted);
        GameEventReference.Instance.OnTaskUpdate.AddListener(OnTaskUpdate);
        GameEventReference.Instance.OnTaskRemoved.AddListener(OnTaskRemoved);
        GameEventReference.Instance.OnLanguageChanged.AddListener(OnLanguageChanged);
    }

    // Event listeners
    private void OnTaskListExpand(params object[] param)
    {
        Debug.Log("LanguagePlus list expanded");

        foreach (var item in _taskList)
        {
            item.TaskListItem.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,
                item.TaskListItem.GetComponent<RectTransform>().anchoredPosition.y);
        }

        this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 75 * (_taskCount + 1));
    }

    private void OnTaskListCollapse(params object[] param)
    {
        Debug.Log("LanguagePlus list collapsed");

        foreach (var item in _taskList)
        {
            item.TaskListItem.GetComponent<RectTransform>().anchoredPosition = new Vector2(-500,
                item.TaskListItem.GetComponent<RectTransform>().anchoredPosition.y);
        }

        this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 75);
    }

    private void OnTaskAdded(params object[] param)
    {
        // Sequence of parameters:
        // 0: LanguagePlus title (typeof LanguagePlus)

        // Debug & Assertion phase
        Assert.AreEqual(1, param.Length);
        Debug.Log($"Task added with title: {(LanguagePlus)param[0]}");

        _taskCount++;

        var listItem = Instantiate(Resources.Load("TaskListItem"), this.gameObject.transform) as GameObject;

        var task = new Task(ref listItem, (LanguagePlus)param[0]);

        // Adjust RectTransform properties
        // ReSharper disable once PossibleNullReferenceException
        RectTransform itemTransform = listItem.GetComponent<RectTransform>();
        itemTransform.anchorMin = new Vector2(0, 1);
        itemTransform.anchorMax = new Vector2(0, 1);
        itemTransform.pivot = new Vector2(0, 1f);

        // Adjust RectTransform position and size
        itemTransform.anchoredPosition = new Vector2(0, 0 - 75 * _taskCount);
        itemTransform.sizeDelta = new Vector2(500, 75);

        // Adjust RectTransform properties of the task list
        RectTransform taskList = GameObject.Find("Task List").GetComponent<RectTransform>();
        taskList.sizeDelta = taskList.sizeDelta + new Vector2(0, 75);

        // Set Title text
        listItem.transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().text =
            (LanguagePlus)param[0] ?? string.Empty;

        // Set GameObject name
        listItem.name = (LanguagePlus)param[0] ?? string.Empty;

        // Add the new task list item to the list
        _taskList.Add(task);
    }

    private void OnTaskCompleted(params object[] param)
    {
        // Sequence of parameters:
        // 0: LanguagePlus title

        // Debug & Assertion phase
        Assert.AreEqual(1, param.Length);
        Debug.Log("LanguagePlus completed");

        // Find the corresponding task list items
        var listItem = _taskList.Find(item => item.TaskListItem.name == (string)param[0]);

        // Set the task list item to completed
        listItem.TaskListItem.transform.GetChild(0).transform.GetChild(1).GetComponent<Toggle>().isOn = true;
    }

    private void OnTaskUpdate(params object[] param)
    {
        // Sequence of parameters:
        // 0: Victim task title
        // 1: New Chinese task title
        // 2: New English task title

        // Debug & Assertion phase
        Assert.AreEqual(3, param.Length);
        Debug.Log("LanguagePlus updated");

        // Find the corresponding task list item
        var listItem = _taskList.Find(item => item.TaskListItem.name == (LanguagePlus)param[0]);

        // Set the new task title
        listItem.TaskName = new LanguagePlus((string)param[1], (string)param[2]);

        // Set GameObject name
        listItem.TaskListItem.name = (LanguagePlus)param[language + 1] ?? string.Empty;
    }

    private void OnTaskRemoved(params object[] param)
    {
        // Sequence of parameters:
        // 0: Victim

        // Find the corresponding task list item
        var listItem = _taskList.Find(item => item.TaskListItem.name == (LanguagePlus)param[0]);

        // Update transform of other list items
        foreach (var item in _taskList.Where(item =>
                     item.TaskListItem.GetComponent<RectTransform>().anchoredPosition.y <
                     listItem.TaskListItem.GetComponent<RectTransform>().anchoredPosition.y))
        {
            var rectTransform = item.TaskListItem.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(0, rectTransform.anchoredPosition.y + 75);
        }

        // Destroy GameObject
        Destroy(listItem.TaskListItem.gameObject);


        // Remove the task list item from the list
        _taskList.Remove(listItem);

        // Update taskCount and taskList size
        _taskCount--;
        this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 75 * (_taskCount + 1));
    }

    private void OnLanguageChanged(params object[] param)
    {
        // Sequence of parameters:
        // 0: Language index

        // Debug & Assertion phase
        Assert.AreEqual(1, param.Length);

        language = (int)param[0];

        // Update all texts
        foreach (var item in _taskList)
        {
            item.TaskListItem.transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).GetComponent<Text>()
                    .text =
                item.TaskName ?? string.Empty;
        }
    }
}
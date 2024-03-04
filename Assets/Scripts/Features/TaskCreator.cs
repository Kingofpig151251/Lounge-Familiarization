using Reference;
using UnityEngine;
using UnityEngine.UI;

public class TaskCreator : MonoBehaviour
{
    // ReSharper disable once InconsistentNaming
    [SerializeField] private InputField _taskNameZH;

    // ReSharper disable once InconsistentNaming
    [SerializeField] private InputField _taskNameEN;

    public void OnClick()
    {
        if (_taskNameEN.text == "" || _taskNameZH.text == "")
        {
            Debug.Log("Inadequate information provided, operation cannot be completed.");
            return;
        }

        GameEventReference.Instance.OnTaskAdded.Trigger(new LanguagePlus(_taskNameZH.text, _taskNameEN.text));
    }
}
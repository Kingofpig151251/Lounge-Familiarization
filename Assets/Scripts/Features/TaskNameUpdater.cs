using Reference;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

// ReSharper disable InconsistentNaming

public class TaskNameUpdater : MonoBehaviour
{
    [SerializeField] private InputField victim;
    [SerializeField] private InputField _newNameZH;
    [SerializeField] private InputField _newNameEN;

    public void OnClick()
    {
        // This line can be safely removed in production
        if (victim.text == "" || _newNameZH.text == "" || _newNameEN.text == "")
        {
            return;
        }

        // Rename task
        GameEventReference.Instance.OnTaskUpdate.Trigger(victim.text, _newNameZH.text, _newNameEN);

        // Reset input fields
        victim.text = "";
        _newNameZH.text = "";
        _newNameEN.text = "";
    }
}
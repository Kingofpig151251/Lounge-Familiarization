using System.Collections;
using System.Collections.Generic;
using Reference;
using UnityEngine;
using UnityEngine.UI;

public class TaskMarker : MonoBehaviour
{
    [SerializeField] private InputField victim;
    
    public void OnClick()
    {
        // This line can be safely removed in production
        if (victim.text == "") { return; }

        // Mark task as completed
        GameEventReference.Instance.OnTaskCompleted.Trigger(victim.text);
        
        // Reset input field
        victim.text = "";
        victim.text = "";
    }
}

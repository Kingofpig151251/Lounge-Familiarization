using Reference;
using UnityEngine;
using UnityEngine.UI;

public class TaskRemover : MonoBehaviour
{
    [SerializeField] private InputField victim;
    
    public void OnClick()
    {
        // This line can be safely removed in production
        if (victim.text == "") { return; }

        // Remove task
        GameEventReference.Instance.OnTaskRemoved.Trigger(victim.text);
        
        // Reset input field
        victim.text = "";
        victim.text = "";
    }
}

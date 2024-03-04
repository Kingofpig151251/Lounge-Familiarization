using UnityEngine;

public class TaskListItem : MonoBehaviour
{
    public void OnRemove()
    {
        Destroy(this);
    }
}
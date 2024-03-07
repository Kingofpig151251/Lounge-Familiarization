using UnityEngine;

[CreateAssetMenu(fileName = "New DialogueSO", menuName = "New Dialogue", order = 4)]
public class DialogueSO : ScriptableObject
{
    [TextAreaAttribute] public string m_messageEng;
    [TextAreaAttribute] public string m_messageTC;
    [TextAreaAttribute] public string m_messageSC;
}
using UnityEngine;

[CreateAssetMenu(fileName = "NewLoungeInfo", menuName = "New Lounge Info", order = 3)]
public class LoungeInfoSO : ScriptableObject
{
    public string m_title_ENG;
    public string m_title_TC;
    public string m_title_SC;
    [TextAreaAttribute] public string m_content_ENG;
    [TextAreaAttribute] public string m_content_TC;
    [TextAreaAttribute] public string m_content_SC;
}
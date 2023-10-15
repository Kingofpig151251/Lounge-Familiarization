using UnityEngine;

public class ChangeLanguage : MonoBehaviour
{
    public void ChangeToEnglish()
    {
        GameEventReference.Instance.OnLanguageChanged.Trigger(1);
    }
    
    public void ChangeToTraditionalChinese()
    {
        GameEventReference.Instance.OnLanguageChanged.Trigger(0);
    }
}

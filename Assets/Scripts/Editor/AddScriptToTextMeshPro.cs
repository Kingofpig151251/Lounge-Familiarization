using UnityEngine;
using UnityEditor;
using TMPro;

public class AddScriptToTextMeshPro : EditorWindow
{
    [MenuItem("Tools/Add Script to TextMeshPro GameObjects")]
    public static void AddScripts()
    {
        TMP_Text[] texts = FindObjectsOfType<TMP_Text>();
        foreach (TMP_Text text in texts)
        {
            GameObject go = text.gameObject;
            if (go.GetComponent<TMPTranslater>() == null)
            {
                go.AddComponent<TMPTranslater>();
            }
        }
    }
}
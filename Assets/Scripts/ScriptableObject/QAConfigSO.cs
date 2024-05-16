using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New QAConfigSO", menuName = "New QAConfigSO", order = 3)]
public class QAConfigSO : ScriptableObject
{
    private const string QAMask = "qa-mask";
    public static int Mask => PlayerPrefs.GetInt(QAMask);
    
    public int m_questionID;
    [Space] public int m_Regionid;
    [Space] public string m_question;
    public string m_wrongAnswer1;
    public string m_wrongAnswer2;
    public string m_correctAnswer;
    public List<int> m_navigateIndex;
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    public static void Initialize()
    {
        if (PlayerPrefs.HasKey(QAMask)) return;
        PlayerPrefs.SetInt(QAMask, 0);
    }
    
    public void OnQACompleted()
    {
        PlayerPrefs.SetInt(QAMask, Mask | m_questionID);
    }

    public bool IsCompleted()
    {
        return (Mask & m_questionID) != 0;
    }
}
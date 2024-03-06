using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New QAConfigSO", menuName = "New QAConfigSO", order = 3)]
public class QAConfigSO : ScriptableObject
{
    public int m_Regionid;
    [Space] public string m_question;
    public string m_wrongAnswer1;
    public string m_wrongAnswer2;
    public string m_correctAnswer;
    public List<int> m_navigateIndex;
}
using System.Collections;
using System.Collections.Generic;
using Reference;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class TestPanelManager : Singleton<TestPanelManager>
{
    public int m_questionIndex;
    private int m_correctAnswerIndex;
    private int m_clickedAnswerIndex;
    private bool m_answerClicked = false;
    public bool m_isQuestionPanelActive = false;

    private void Start()
    {
        GameEventReference.Instance.OpenTestPanel.AddListener(OpenTestPanel);
        GameEventReference.Instance.OnClickTestOption.AddListener(OnClickTestOption);
        GameEventReference.Instance.OnEnterNavigatePhase.AddListener(OnEnterNavigatePhase);
    }

    private bool isQuestionIndexPremit()
    {
        switch (ViewPointManager.Instance.m_currentViewPoint.m_index)
        {
            case 1:
            case 2:
            case 3:
            case 4:
                if (TaskReference.Instance.m_taskConfigSO[m_questionIndex].m_lounge == Lounge.WingBusinessLounge)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
                if (TaskReference.Instance.m_taskConfigSO[m_questionIndex].m_lounge == Lounge.WingBusinessLounge)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            default:
                Debug.LogError("checkQuestionIndex fuction returning a unknown value!");
                return true;
        }
    }

    private void OpenTestPanel(params object[] param)
    {
        if (m_isQuestionPanelActive || LayerManager.Instance.m_isLayerActive)
            return;

        m_isQuestionPanelActive = true;
        bool wrongAnswerUsed = false;
        m_answerClicked = false;
        System.DateTime now = System.DateTime.Now;

        UIElementReference.Instance.m_questionPanel.SetActive(true);
        UIElementReference.Instance.m_nextQuestionButton.SetActive(false);
        UIElementReference.Instance.m_enterNavigateButton.SetActive(false);
        UIElementReference.Instance.m_answerList[m_clickedAnswerIndex].GetComponent<Image>().color = Color.white;

        do
        {
            int seed = (int)((now.Day) * now.Millisecond * Time.realtimeSinceStartup / now.Minute);
            Random.InitState(seed);
            m_questionIndex = Mathf.Clamp(Random.Range(0, UIElementReference.Instance.m_questionList.Count), 0,
                UIElementReference.Instance.m_questionList.Count - 1);
            m_correctAnswerIndex = Mathf.Clamp(Random.Range(0, 3), 0, 2);

            UIElementReference.Instance.m_questionBox.GetComponentInChildren<TMP_Text>().text =
                UIElementReference.Instance.m_questionList[m_questionIndex].m_question;
        } while (isQuestionIndexPremit());


        //Replace Question Text
        for (int i = 0; i < UIElementReference.Instance.m_answerList.Count; i++)
        {
            if (i == m_correctAnswerIndex)
            {
                UIElementReference.Instance.m_answerList[i].GetComponentInChildren<TMP_Text>().text =
                    UIElementReference.Instance.m_questionList[m_questionIndex].m_correctAnswer;
            }
            else if (wrongAnswerUsed == false)
            {
                UIElementReference.Instance.m_answerList[i].GetComponentInChildren<TMP_Text>().text =
                    UIElementReference.Instance.m_questionList[m_questionIndex].m_wrongAnswer1;
                wrongAnswerUsed = true;
            }
            else
            {
                UIElementReference.Instance.m_answerList[i].GetComponentInChildren<TMP_Text>().text =
                    UIElementReference.Instance.m_questionList[m_questionIndex].m_wrongAnswer2;
            }
        }
    }

    private void OnEnterNavigatePhase(params object[] param)
    {
        ResetTestPanel();
        UIElementReference.Instance.m_enterNavigateButton.SetActive(false);
    }

    private void ResetTestPanel()
    {
        UIElementReference.Instance.m_questionPanel.SetActive(false);
        UIElementReference.Instance.m_answerList[m_clickedAnswerIndex].GetComponent<Image>().color = Color.white;
        m_isQuestionPanelActive = false;
    }

    private void OnClickTestOption(params object[] param)
    {
        if (m_answerClicked) return;
        m_answerClicked = true;
        m_clickedAnswerIndex = (int)param[0];
        if (m_clickedAnswerIndex == m_correctAnswerIndex)
        {
            UIElementReference.Instance.m_answerList[m_clickedAnswerIndex].GetComponent<Image>().color = Color.green;
        }
        else
        {
            UIElementReference.Instance.m_answerList[m_clickedAnswerIndex].GetComponent<Image>().color = Color.red;
        }

        UIElementReference.Instance.m_nextQuestionButton.SetActive(true);
        UIElementReference.Instance.m_enterNavigateButton.SetActive(true);
    }
}
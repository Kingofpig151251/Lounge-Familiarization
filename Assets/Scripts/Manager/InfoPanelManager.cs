using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanelManager : Singleton<InfoPanelManager>
{
    private bool m_isPanelExpanded = false;
    
    private bool[] m_isLoungeHeaderExpanded = { false, false, false };
    private bool[] m_isClassHeaderExpanded = { false, false, false, false, false, false };

    private float[] m_loungeViewSizeYOffset = { 100, 100, 50 };

    void Start()
    {
        SetUpListeners();
    }

    //Extended Functions
    private void SetUpListeners()
    {
        GameEventReference.Instance.OnClickInfoExpandButton.AddListener(OnClickInfoExpandButton);
        GameEventReference.Instance.OnClickLoungeHeader.AddListener(OnClickLoungeHeader);
        GameEventReference.Instance.OnClickClassHeader.AddListener(OnClickClassHeader);
    }

    private void OnClickInfoExpandButton(params object[] param)
    {
        m_isPanelExpanded = !m_isPanelExpanded;

        if (!m_isPanelExpanded)
        {
            StartCoroutine(ChangeViewLocationX(UIElementReference.Instance.m_InfoPanel, 310f, -620f));
            UIElementReference.Instance.m_InfoPanelExpandButton.GetComponent<Image>().sprite = UIElementReference.Instance.m_expandButton;
        }
        else
        {
            StartCoroutine(ChangeViewLocationX(UIElementReference.Instance.m_InfoPanel, -310f, 620f));
            UIElementReference.Instance.m_InfoPanelExpandButton.GetComponent<Image>().sprite = UIElementReference.Instance.m_collapseButton;
        }
    }
    private void StartRotateButton(GameObject button)
    {
        if (button.GetComponent<RectTransform>().rotation.eulerAngles.z == 0f)
        {
            StartCoroutine(RotateButton(button, 0f, -180f));

        }
        else if (button.GetComponent<RectTransform>().rotation.eulerAngles.z == 180f)
        {
            StartCoroutine(RotateButton(button, 180f, 0f));
        }
    }
    private void OnClickLoungeHeader(params object[] param)
    {
        int headerIndex = (int)param[0];

        m_isLoungeHeaderExpanded[headerIndex] = !m_isLoungeHeaderExpanded[headerIndex];
        StartRotateButton(UIElementReference.Instance.m_infoPanel_LoungeHeaderButton[headerIndex]);

        float targetDeltaSizeY = m_isLoungeHeaderExpanded[headerIndex] ? m_loungeViewSizeYOffset[headerIndex] : -m_loungeViewSizeYOffset[headerIndex];

        GameObject LoungeView = UIElementReference.Instance.m_infoPanel_LoungeView[headerIndex];
        StartCoroutine(ChangeViewSize(LoungeView, LoungeView.GetComponent<RectTransform>().sizeDelta.y, targetDeltaSizeY));
    }
    private void OnClickClassHeader(params object[] param)
    {
        int classIndex = (int)param[0];

        m_isClassHeaderExpanded[classIndex] = !m_isClassHeaderExpanded[classIndex];
        StartRotateButton(UIElementReference.Instance.m_infoPanel_ClassHeaderButton[classIndex]);

        float targetDeltaSizeY = -1f;

        GameObject nextClassView = classIndex + 1 >= UIElementReference.Instance.m_infoPanel_ClassHeader.Count ? null : UIElementReference.Instance.m_infoPanel_ClassHeader[classIndex + 1];
        GameObject LoungeView = UIElementReference.Instance.m_infoPanel_LoungeView[classIndex / 2];


        switch (classIndex)
        {
            case 0:
                targetDeltaSizeY = m_isClassHeaderExpanded[classIndex] ? 270f : -270f;
                StartCoroutine(ChangeViewLocationY(nextClassView, nextClassView.GetComponent<RectTransform>().anchoredPosition.y, -targetDeltaSizeY));
                break;
            case 1:
                targetDeltaSizeY = m_isClassHeaderExpanded[classIndex] ? 135f : -135f;
                break;
            case 2:
                targetDeltaSizeY = m_isClassHeaderExpanded[classIndex] ? 450f : -450f;
                StartCoroutine(ChangeViewLocationY(nextClassView, nextClassView.GetComponent<RectTransform>().anchoredPosition.y, -targetDeltaSizeY));
                break;
            case 3:
                targetDeltaSizeY = m_isClassHeaderExpanded[classIndex] ? 450f : -450f;
                break;
            case 4:
                targetDeltaSizeY = m_isClassHeaderExpanded[classIndex] ? 450f : -450f;
                break;
        }

        m_loungeViewSizeYOffset[classIndex / 2] += targetDeltaSizeY;

        GameObject ClassView = UIElementReference.Instance.m_infoPanel_ClassView[classIndex];
        StartCoroutine(ChangeViewSize(LoungeView, LoungeView.GetComponent<RectTransform>().sizeDelta.y, targetDeltaSizeY));
        StartCoroutine(ChangeViewSize(ClassView, ClassView.GetComponent<RectTransform>().sizeDelta.y, targetDeltaSizeY));
    }

    private IEnumerator RotateButton(GameObject button, float originalTargetAngleZ, float targetAngleZ)
    {
        const float duration = 0.25f;
        float timeCounter = 0;

        while (true)
        {
            timeCounter += Time.deltaTime;

            float currentAngleZ;

            if (timeCounter >= duration)
            {
                button.GetComponent<RectTransform>().rotation = Quaternion.Euler(new Vector3(0, 0, targetAngleZ));
                break;
            }
            else
            {
                currentAngleZ = Mathf.Lerp(originalTargetAngleZ, targetAngleZ, timeCounter / duration);
                button.GetComponent<RectTransform>().rotation = Quaternion.Euler(new Vector3(0, 0, currentAngleZ));
            }
            yield return null;
        }
    }
    private IEnumerator ChangeViewLocationX(GameObject view, float originalPosX, float targetPosX)
    {
        const float duration = 0.25f;
        float timeCounter = 0;

        while (true)
        {
            timeCounter += Time.deltaTime;

            float currentPosX;

            if (timeCounter >= duration)
            {
                view.GetComponent<RectTransform>().anchoredPosition = new Vector2(originalPosX + targetPosX, view.GetComponent<RectTransform>().anchoredPosition.y);
                break;
            }
            else
            {
                currentPosX = Mathf.Lerp(0, targetPosX, timeCounter / duration);
                view.GetComponent<RectTransform>().anchoredPosition = new Vector2(originalPosX + currentPosX, view.GetComponent<RectTransform>().anchoredPosition.y);
            }
            yield return null;
        }
    }
    private IEnumerator ChangeViewLocationY(GameObject view, float originalPosY, float targetPosY)
    {
        const float duration = 0.25f;
        float timeCounter = 0;

        while (true)
        {
            timeCounter += Time.deltaTime;

            float currentPosY;

            if (timeCounter >= duration)
            {
                view.GetComponent<RectTransform>().anchoredPosition = new Vector2(view.GetComponent<RectTransform>().anchoredPosition.x, originalPosY + targetPosY);
                break;
            }
            else
            {
                currentPosY = Mathf.Lerp(0, targetPosY, timeCounter / duration);
                view.GetComponent<RectTransform>().anchoredPosition = new Vector2(view.GetComponent<RectTransform>().anchoredPosition.x, originalPosY + currentPosY);
            }
            yield return null;
        }
    }
    private IEnumerator ChangeViewSize(GameObject view, float originalSizeY, float targetSizeY)
    {
        const float duration = 0.25f;
        float timeCounter = 0;

        while (true)
        {
            timeCounter += Time.deltaTime;

            float currentSizeY;

            if (timeCounter >= duration)
            {
                view.GetComponent<RectTransform>().sizeDelta = new Vector2(view.GetComponent<RectTransform>().sizeDelta.x, originalSizeY + targetSizeY);
                break;
            }
            else
            {
                currentSizeY = Mathf.Lerp(0, targetSizeY, timeCounter / duration);
                view.GetComponent<RectTransform>().sizeDelta = new Vector2(view.GetComponent<RectTransform>().sizeDelta.x, originalSizeY + currentSizeY);
            }
            yield return null;
        }
    }

}

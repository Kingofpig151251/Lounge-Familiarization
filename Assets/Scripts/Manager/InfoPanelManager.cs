using System;
using System.Collections;
using Reference;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Icons;

public class InfoPanelManager : Singleton<InfoPanelManager>
{
    private bool m_isPanelExpanded = false;

    private bool[] m_isLoungeHeaderExpanded = { false, false, false };
    private bool[] m_isClassHeaderExpanded = { false, false, false, false, false, false };

    private float[]
        m_loungeViewSizeYOffset = { 100, 100, 45 }; //each class need 45px eg. first class(45px) + business(45px) = 90px

    private float[] m_loungeViewSizeY = new float[5];

    private const float m_viewportSizeY = 70;

    private bool m_isAnimating = false;

    private void Start()
    {
        for (int i = 0; i < UIElementReference.Instance.m_infoPanel_ClassView.Count; i++)
        {
            m_loungeViewSizeY[i] = UIElementReference.Instance.m_infoPanel_ClassView[i].transform.childCount *
                                   m_viewportSizeY;
        }

        SetUpListeners();
    }

    //Extended Functions
    private void SetUpListeners()
    {
        GameEventReference.Instance.OnClickInfoExpandButton.AddListener(OnClickInfoExpandButton);
        GameEventReference.Instance.OnClickLoungeHeader.AddListener(OnClickLoungeHeader);
        GameEventReference.Instance.OnClickClassHeader.AddListener(OnClickClassHeader);
        GameEventReference.Instance.OnGameReset.AddListener(OnGameReset);
        GameEventReference.Instance.OnLanguageChanged.AddListener(UpdateCurentLoungh);
        GameEventReference.Instance.OnEnterViewPoint.AddListener(UpdateCurentLoungh);
    }

    private void ExpandInfoPanel()
    {
        m_isPanelExpanded = !m_isPanelExpanded;

        float panelWidth = UIElementReference.Instance.m_InfoPanel.GetComponent<RectTransform>().sizeDelta.x;

        UIElementReference.Instance.m_popupWindow.SetActive(false);

        if (!m_isPanelExpanded)
        {
            StartCoroutine(ChangeViewLocationX(UIElementReference.Instance.m_InfoPanel, panelWidth / 2, -panelWidth));
            UIElementReference.Instance.m_InfoPanelExpandButton.GetComponent<Image>().sprite =
                UIElementReference.Instance.m_expandButton;
        }
        else
        {
            StartCoroutine(ChangeViewLocationX(UIElementReference.Instance.m_InfoPanel, -panelWidth / 2, panelWidth));
            UIElementReference.Instance.m_InfoPanelExpandButton.GetComponent<Image>().sprite =
                UIElementReference.Instance.m_collapseButton;
        }
    }

    private void OnClickInfoExpandButton(params object[] param)
    {
        if (m_isAnimating)
        {
            return;
        }

        ExpandInfoPanel();
    }

    private void OnClickLoungeHeader(params object[] param)
    {
        int headerIndex = (int)param[0];

        if (m_isAnimating)
        {
            return;
        }

        m_isLoungeHeaderExpanded[headerIndex] = !m_isLoungeHeaderExpanded[headerIndex];
        StartRotateButton(UIElementReference.Instance.m_infoPanel_LoungeHeaderButton[headerIndex]);

        float targetDeltaSizeY = m_isLoungeHeaderExpanded[headerIndex]
            ? m_loungeViewSizeYOffset[headerIndex]
            : -m_loungeViewSizeYOffset[headerIndex];

        GameObject LoungeView = UIElementReference.Instance.m_infoPanel_LoungeView[headerIndex];
        StartCoroutine(ChangeViewSize(LoungeView, LoungeView.GetComponent<RectTransform>().sizeDelta.y,
            targetDeltaSizeY));
    }

    private void OnClickClassHeader(params object[] param)
    {
        int classIndex = (int)param[0];

        if (m_isAnimating)
        {
            return;
        }

        m_isClassHeaderExpanded[classIndex] = !m_isClassHeaderExpanded[classIndex];
        StartRotateButton(UIElementReference.Instance.m_infoPanel_ClassHeaderButton[classIndex]);

        float targetDeltaSizeY = -1f;

        GameObject nextClassView = classIndex + 1 >= UIElementReference.Instance.m_infoPanel_ClassHeader.Count
            ? null
            : UIElementReference.Instance.m_infoPanel_ClassHeader[classIndex + 1];
        GameObject LoungeView = UIElementReference.Instance.m_infoPanel_LoungeView[classIndex / 2];


        switch (classIndex)
        {
            case 0:
                targetDeltaSizeY = m_isClassHeaderExpanded[classIndex] ? m_loungeViewSizeY[0] : -m_loungeViewSizeY[0];
                StartCoroutine(ChangeViewLocationY(nextClassView,
                    nextClassView.GetComponent<RectTransform>().anchoredPosition.y, -targetDeltaSizeY));
                break;
            case 1:
                targetDeltaSizeY = m_isClassHeaderExpanded[classIndex] ? m_loungeViewSizeY[1] : -m_loungeViewSizeY[1];
                break;
            case 2:
                targetDeltaSizeY = m_isClassHeaderExpanded[classIndex] ? m_loungeViewSizeY[2] : -m_loungeViewSizeY[2];
                StartCoroutine(ChangeViewLocationY(nextClassView,
                    nextClassView.GetComponent<RectTransform>().anchoredPosition.y, -targetDeltaSizeY));
                break;
            case 3:
                targetDeltaSizeY = m_isClassHeaderExpanded[classIndex] ? m_loungeViewSizeY[3] : -m_loungeViewSizeY[3];
                break;
            case 4:
                targetDeltaSizeY = m_isClassHeaderExpanded[classIndex] ? m_loungeViewSizeY[4] : -m_loungeViewSizeY[4];
                break;
        }

        m_loungeViewSizeYOffset[classIndex / 2] += targetDeltaSizeY;

        GameObject ClassView = UIElementReference.Instance.m_infoPanel_ClassView[classIndex];
        StartCoroutine(ChangeViewSize(LoungeView, LoungeView.GetComponent<RectTransform>().sizeDelta.y,
            targetDeltaSizeY));
        StartCoroutine(ChangeViewSize(ClassView, ClassView.GetComponent<RectTransform>().sizeDelta.y,
            targetDeltaSizeY));
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

    private void OnGameReset(params object[] param)
    {
        if (m_isAnimating)
        {
            StartCoroutine(WaitForAnimated());
        }
        else
        {
            ClosePanel();
        }
    }

    private void ClosePanel()
    {
        if (m_isPanelExpanded)
        {
            ExpandInfoPanel();
        }
    }

    private IEnumerator WaitForAnimated()
    {
        while (true)
        {
            if (!m_isAnimating)
            {
                ClosePanel();
                break;
            }

            yield return null;
        }
    }

    private IEnumerator RotateButton(GameObject button, float originalTargetAngleZ, float targetAngleZ)
    {
        m_isAnimating = true;
        const float duration = 0.25f;
        float timeCounter = 0;

        while (true)
        {
            timeCounter += Time.deltaTime;

            float currentAngleZ;

            if (timeCounter >= duration)
            {
                button.GetComponent<RectTransform>().rotation = Quaternion.Euler(new Vector3(0, 0, targetAngleZ));
                m_isAnimating = false;
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
        m_isAnimating = true;
        const float duration = 0.25f;
        float timeCounter = 0;

        while (true)
        {
            timeCounter += Time.deltaTime;

            float currentPosX;

            if (timeCounter >= duration)
            {
                view.GetComponent<RectTransform>().anchoredPosition = new Vector2(originalPosX + targetPosX,
                    view.GetComponent<RectTransform>().anchoredPosition.y);
                m_isAnimating = false;
                break;
            }
            else
            {
                currentPosX = Mathf.Lerp(0, targetPosX, timeCounter / duration);
                view.GetComponent<RectTransform>().anchoredPosition = new Vector2(originalPosX + currentPosX,
                    view.GetComponent<RectTransform>().anchoredPosition.y);
            }

            yield return null;
        }
    }

    private IEnumerator ChangeViewLocationY(GameObject view, float originalPosY, float targetPosY)
    {
        m_isAnimating = true;
        const float duration = 0.25f;
        float timeCounter = 0;

        while (true)
        {
            timeCounter += Time.deltaTime;

            float currentPosY;

            if (timeCounter >= duration)
            {
                view.GetComponent<RectTransform>().anchoredPosition =
                    new Vector2(view.GetComponent<RectTransform>().anchoredPosition.x, originalPosY + targetPosY);
                m_isAnimating = false;
                break;
            }
            else
            {
                currentPosY = Mathf.Lerp(0, targetPosY, timeCounter / duration);
                view.GetComponent<RectTransform>().anchoredPosition =
                    new Vector2(view.GetComponent<RectTransform>().anchoredPosition.x, originalPosY + currentPosY);
            }

            yield return null;
        }
    }

    private IEnumerator ChangeViewSize(GameObject view, float originalSizeY, float targetSizeY)
    {
        m_isAnimating = true;

        const float duration = 0.25f;
        float timeCounter = 0;

        while (true)
        {
            timeCounter += Time.deltaTime;

            float currentSizeY;

            if (timeCounter >= duration)
            {
                view.GetComponent<RectTransform>().sizeDelta =
                    new Vector2(view.GetComponent<RectTransform>().sizeDelta.x, originalSizeY + targetSizeY);
                m_isAnimating = false;
                break;
            }
            else
            {
                currentSizeY = Mathf.Lerp(0, targetSizeY, timeCounter / duration);
                view.GetComponent<RectTransform>().sizeDelta =
                    new Vector2(view.GetComponent<RectTransform>().sizeDelta.x, originalSizeY + currentSizeY);
            }

            yield return null;
        }
    }

    private void UpdateCurentLoungh(params object[] param)
    {
        TMP_Text curentLoungh = UIElementReference.Instance.m_curentLounghText.GetComponent<TMP_Text>();

        switch (ViewPointManager.Instance.m_currentLounge)
        {
            case Lounge.DeckBusinessLounge:
                if (GameManager.Instance.GetCurrentLanguage() == Class_Language.English)
                {
                    curentLoungh.text = "The Deck";
                }
                else if (GameManager.Instance.GetCurrentLanguage() == Class_Language.TraditionalChinese)
                {
                    curentLoungh.text = "玲瓏堂";
                }
                else if (GameManager.Instance.GetCurrentLanguage() == Class_Language.SimplifiedChinese)
                {
                    curentLoungh.text = "玲珑堂";
                }
                break;
            case Lounge.WingFristClassLounge:
                if (GameManager.Instance.GetCurrentLanguage() == Class_Language.English)
                {
                    curentLoungh.text = "The Wing(First)";
                }
                else if (GameManager.Instance.GetCurrentLanguage() == Class_Language.TraditionalChinese)
                {
                    curentLoungh.text = "寰宇堂(頭等)";
                }
                else if (GameManager.Instance.GetCurrentLanguage() == Class_Language.SimplifiedChinese)
                {
                    curentLoungh.text = "寰宇堂(头等)";
                }
                break;
            case Lounge.WingBusinessLounge:
                if (GameManager.Instance.GetCurrentLanguage() == Class_Language.English)
                {
                    curentLoungh.text = "The Wing(Business)";
                }
                else if (GameManager.Instance.GetCurrentLanguage() == Class_Language.TraditionalChinese)
                {
                    curentLoungh.text = "寰宇堂(商務)";
                }
                else if (GameManager.Instance.GetCurrentLanguage() == Class_Language.SimplifiedChinese)
                {
                    curentLoungh.text = "寰宇堂(商务)";
                }
                break;
            case Lounge.PierFirstClassLounge:
                if (GameManager.Instance.GetCurrentLanguage() == Class_Language.English)
                {
                    curentLoungh.text = "The Pier(First)";
                }
                else if (GameManager.Instance.GetCurrentLanguage() == Class_Language.TraditionalChinese)
                {
                    curentLoungh.text = "玉衡堂(頭等)";
                }
                else if (GameManager.Instance.GetCurrentLanguage() == Class_Language.SimplifiedChinese)
                {
                    curentLoungh.text = "玉衡堂(头等)";
                }
                break;
            case Lounge.PierBusinessLounge:
                if (GameManager.Instance.GetCurrentLanguage() == Class_Language.English)
                {
                    curentLoungh.text = "The Pier(Business)";
                }
                else if (GameManager.Instance.GetCurrentLanguage() == Class_Language.TraditionalChinese)
                {
                    curentLoungh.text = "玉衡堂(商務)";
                }
                else if (GameManager.Instance.GetCurrentLanguage() == Class_Language.SimplifiedChinese)
                {
                    curentLoungh.text = "玉衡堂(商务)";
                }
                break;
        }
    }
    public bool GetIsExpanded() => m_isPanelExpanded;
}
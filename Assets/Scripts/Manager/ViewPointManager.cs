using System.Collections;
using System.Collections.Generic;
using Reference;
using TMPro;
using UnityEditor;
using UnityEngine;

public class ViewPointManager : Singleton<ViewPointManager>
{
    [SerializeField] private GameObject m_arrowObect;
    [SerializeField] private GameObject m_infoObect;

    [SerializeField] public GameObject m_firstViewPoint;

    [SerializeField] private GameObject m_camera;

    public ViewPoint m_currentViewPoint;
    public Lounge m_currentLounge;
    private List<GameObject> m_currentIcons = new List<GameObject>();

    protected override void Init()
    {
        EnterViewPoint(0);
    }

    private void Start()
    {
        GameEventReference.Instance.OnEnterViewPoint.AddListener(OnEnterViewPoint);
        GameEventReference.Instance.OnInteractInfo.AddListener(OnInteractInfo);
        GameEventReference.Instance.OnChangeRegion.AddListener(OnChangeRegion);
    }

    private void OnEnterViewPoint(params object[] param)
    {
        int nextViewPointIndex = (int)param[0];
        EnterViewPoint(nextViewPointIndex);

        LayerManager.Instance.m_isLayerActive = false;
    }


    private void OnInteractInfo(params object[] param)
    {
        InfoSO info = (InfoSO)param[0];
    }

    private void OnChangeRegion(params object[] param)
    {
        string regionIndex = (string)param[0];
        switch (regionIndex)
        {
            case "10":
                m_currentLounge = Lounge.DeckBusinessLounge;
                break;
            case "11":
                m_currentLounge = Lounge.WingFristClassLounge;
                break;
            case "12":
                m_currentLounge = Lounge.WingBusinessLounge;
                break;
            case "13":
                m_currentLounge = Lounge.PierFirstClassLounge;
                break;
            case "14":
                m_currentLounge = Lounge.PierBusinessLounge;
                break;
        }

        GameEventReference.Instance.OnEnterViewPoint.Trigger(0);
        CameraController.Instance.SetRotation(m_currentViewPoint.m_infoSO[0].m_position);
    }

    private void EnterViewPoint(int viewPointIndex)
    {
        //Clear Interface Item
        if (m_currentViewPoint != null)
        {
            foreach (GameObject item in m_currentIcons)
            {
                Destroy(item);
            }

            m_currentIcons.Clear();
        }

        StartCoroutine(CrossViewPoint(viewPointIndex));
    }

    private void InstantiateInterfaceItem()
    {
        GameObject currentObj;
        for (int i = 0; i < m_currentViewPoint.m_arrowSO.Length; i++)
        {
            m_currentIcons.Add(currentObj = Instantiate(m_arrowObect, m_firstViewPoint.transform));
            currentObj.transform.localPosition = m_currentViewPoint.m_arrowSO[i].m_position;
            currentObj.transform.localEulerAngles = m_currentViewPoint.m_arrowSO[i].m_rotation;
            currentObj.transform.localScale = m_currentViewPoint.m_arrowSO[i].m_size;
            currentObj.GetComponent<InterfaceItem_Arrow>().m_nextViewPointIndex =
                m_currentViewPoint.m_arrowSO[i].m_nextViewPointIndex;
            currentObj.transform.GetComponent<Renderer>().material.renderQueue = 3001;
        }

        for (int i = 0; i < m_currentViewPoint.m_infoSO.Length; i++)
        {
            m_currentIcons.Add(currentObj = Instantiate(m_infoObect, m_firstViewPoint.transform));
            currentObj.transform.localPosition = m_currentViewPoint.m_infoSO[i].m_position;
            currentObj.transform.localEulerAngles = m_currentViewPoint.m_infoSO[i].m_rotation;
            currentObj.transform.localScale = m_currentViewPoint.m_infoSO[i].m_size;
            currentObj.GetComponent<InterfaceItem_Info>().m_info = m_currentViewPoint.m_infoSO[i];
            currentObj.GetComponent<Renderer>().material.renderQueue = 3002;
        }
    }

    private IEnumerator CrossViewPoint(int viewPointIndex)
    {
        const float duration = 1f;

        float timer = 0;
        float fEffectReduceVal = 45f;

        Camera.main.fieldOfView = (150f - fEffectReduceVal);

        Camera.main.GetComponent<RapidBlurEffect>().enabled = true;

        m_currentViewPoint =
            ViewPointReference.Instance.m_viewPointSO[
                viewPointIndex + ViewPointReference.Instance.m_loungeStartIndex[(int)m_currentLounge]];

        m_currentViewPoint.SetVisited();

        UIElementReference.Instance.m_nextViewPoint.GetComponent<Renderer>().material
            .SetTexture("mainTexture", m_currentViewPoint.m_texture);

        UIElementReference.Instance.m_nextViewPoint.transform.localEulerAngles = new Vector3(
            this.transform.localEulerAngles.x, m_currentViewPoint.m_rotation, this.transform.localEulerAngles.z);

        UIElementReference.Instance.m_waterMark.SetActive(false);

        while (true)
        {
            if (timer >= duration)
            {
                UIElementReference.Instance.m_firstViewPoint.GetComponent<Renderer>().material
                    .SetTexture("mainTexture", m_currentViewPoint.m_texture);
                UIElementReference.Instance.m_firstViewPoint.GetComponent<MeshRenderer>().material.SetFloat("Alpha", 1);
                UIElementReference.Instance.m_nextViewPoint.GetComponent<MeshRenderer>().material.SetFloat("Alpha", 0);

                Vector3 temp = UIElementReference.Instance.m_firstViewPoint.transform.localEulerAngles;
                UIElementReference.Instance.m_firstViewPoint.transform.localEulerAngles =
                    UIElementReference.Instance.m_nextViewPoint.transform.localEulerAngles;
                UIElementReference.Instance.m_nextViewPoint.transform.localEulerAngles = temp;

                UIElementReference.Instance.m_waterMark.SetActive(true);

                break;
            }
            else
            {
                timer += Time.deltaTime;
                UIElementReference.Instance.m_firstViewPoint.GetComponent<MeshRenderer>().material
                    .SetFloat("Alpha", Mathf.Lerp(1f, 0f, timer / duration));
                UIElementReference.Instance.m_nextViewPoint.GetComponent<MeshRenderer>().material
                    .SetFloat("Alpha", Mathf.Lerp(0f, 1f, timer / duration));
                Camera.main.fieldOfView = (150f - fEffectReduceVal) - (timer / duration) * (90f - fEffectReduceVal);
            }

            yield return null;
        }

        Camera.main.GetComponent<RapidBlurEffect>().enabled = false;

        InstantiateInterfaceItem();
    }

    public void PurgeLocationHistory()
    {
        PlayerPrefs.SetInt(Lounge.DeckBusinessLounge.ToString(), 0);
        PlayerPrefs.SetInt(Lounge.WingFristClassLounge.ToString(), 0);
        PlayerPrefs.SetInt(Lounge.WingBusinessLounge.ToString(), 0);
        PlayerPrefs.SetInt(Lounge.PierFirstClassLounge.ToString(), 0);
        PlayerPrefs.SetInt(Lounge.PierBusinessLounge.ToString(), 0);
    }

#if UNITY_EDITOR
    // Reload All arrows on current viewpoint
    // @Jason please forgive me orz orz
    [ContextMenu("Live reload")]
    public void ReloadViewPointDrawArrowsAndInfoWithRespectToChangesOnScriptableObjects()
    {
        if (m_currentViewPoint == null) return;
        foreach (var item in m_currentIcons)
        {
            Destroy(item);
        }

        m_currentIcons.Clear();

        GameObject currentObj;
        foreach (var t in m_currentViewPoint.m_arrowSO)
        {
            m_currentIcons.Add(currentObj = Instantiate(m_arrowObect, m_firstViewPoint.transform));
            currentObj.transform.localPosition = t.m_position;
            currentObj.transform.localEulerAngles = t.m_rotation;
            currentObj.transform.localScale = t.m_size;
            currentObj.GetComponent<InterfaceItem_Arrow>().m_nextViewPointIndex = t.m_nextViewPointIndex;
            currentObj.transform.GetComponent<Renderer>().material.renderQueue = 3001;
        }

        foreach (var t in m_currentViewPoint.m_infoSO)
        {
            m_currentIcons.Add(currentObj = Instantiate(m_infoObect, m_firstViewPoint.transform));
            currentObj.transform.localPosition = t.m_position;
            currentObj.transform.localEulerAngles = t.m_rotation;
            currentObj.transform.localScale = t.m_size;


            currentObj.GetComponent<InterfaceItem_Info>().m_info = t;
            currentObj.GetComponent<Renderer>().material.renderQueue = 3002;
        }
    }

    // Apply the changes on the arrows to the ScriptableObjects
    // Useful when the arrows are moved in the scene
    // For development use only
    [MenuItem("Tools/Burn data to ScriptableObjects")]
    public static void ApplyDataToScriptableObjectsWithRespectToValueChangeInGameObjects()
    {
        var arrows = Instance.m_firstViewPoint.GetComponentsInChildren<InterfaceItem_Arrow>();
        AssetDatabase.StartAssetEditing();
        for (var i = 0; i < arrows.Length; i++)
        {
            var recordedPosition = arrows[i].transform.localPosition;
            var path =
                $"Assets/ScriptableObject/Arrow/Pier_Business/VP{Instance.m_currentViewPoint.m_index}/VP{Instance.m_currentViewPoint.m_index} Arrow {i}.asset";
            var targetAsset = AssetDatabase.LoadAssetAtPath<ArrowSO>(path);
            if (targetAsset is null)
            {
                Debug.LogWarning($"Asset not found at {path}");
                continue;
            }

            targetAsset.m_position = recordedPosition;
            EditorUtility.SetDirty(targetAsset);
        }

        AssetDatabase.StopAssetEditing(); // Stupid Unity
        AssetDatabase.SaveAssets();
    }


    // Display the next view point index on the arrows
    // For development use only
    [ContextMenu("Display arrow destination")]
    public void FlexArrowDestination()
    {
        var arrows = Instance.m_firstViewPoint.GetComponentsInChildren<InterfaceItem_Arrow>();
        foreach (var t in arrows)
        {
            if (!Application.isPlaying) return;
            var indicator = Instantiate(new GameObject(), GameObject.Find("Canvas").transform);
            var rectTransform = indicator.AddComponent<RectTransform>();
            rectTransform.anchorMax = new Vector2(0.5f, 0);
            rectTransform.anchorMin = new Vector2(0.5f, 0);
            rectTransform.position = Camera.main.WorldToScreenPoint(t.transform.position);
            indicator.AddComponent<TextMeshProUGUI>().text = t.m_nextViewPointIndex.ToString();
            indicator.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
            indicator.GetComponent<TextMeshProUGUI>().horizontalAlignment = HorizontalAlignmentOptions.Center;
            indicator.GetComponent<TextMeshProUGUI>().fontSize = 72;
            Destroy(indicator, 2.5f);
        }
    }
#endif
}
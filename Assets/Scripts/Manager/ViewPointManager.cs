using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewPointManager : Singleton<ViewPointManager>
{
    [SerializeField] private GameObject m_arrowObect;
    [SerializeField] private GameObject m_infoObect;
    
    [SerializeField] private GameObject m_firstViewPoint;

    public ViewPoint m_currentViewPoint;
    private List<GameObject> m_currentIcons = new List<GameObject>();

    protected override void Init()
    {
        EnterViewPoint(0);
    }

    private void Start()
    {
        GameEventReference.Instance.OnEnterViewPoint.AddListener(OnEnterViewPoint);
        GameEventReference.Instance.OnInteractInfo.AddListener(OnInteractInfo);
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
            currentObj.GetComponent<InterfaceItem_Arrow>().m_nextViewPointIndex = m_currentViewPoint.m_arrowSO[i].m_nextViewPointIndex;
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
        const float duration = 0.6f;

        float timer = 0;

        Camera.main.fieldOfView = 150f;

        Camera.main.GetComponent<RapidBlurEffect>().enabled = true;

        m_currentViewPoint = ViewPointReference.Instance.m_viewPointSO[viewPointIndex];
        UIElementReference.Instance.m_nextViewPoint.GetComponent<Renderer>().material.SetTexture("mainTexture", m_currentViewPoint.m_texture);

        UIElementReference.Instance.m_nextViewPoint.transform.localEulerAngles = new Vector3(this.transform.localEulerAngles.x, m_currentViewPoint.m_rotation, this.transform.localEulerAngles.z);

        UIElementReference.Instance.m_waterMark.SetActive(false);

        while (true)
        {
            if (timer >= duration)
            {
                UIElementReference.Instance.m_firstViewPoint.GetComponent<Renderer>().material.SetTexture("mainTexture", m_currentViewPoint.m_texture);
                UIElementReference.Instance.m_firstViewPoint.GetComponent<MeshRenderer>().material.SetFloat("Alpha", 1);
                UIElementReference.Instance.m_nextViewPoint.GetComponent<MeshRenderer>().material.SetFloat("Alpha", 0);

                Vector3 temp = UIElementReference.Instance.m_firstViewPoint.transform.localEulerAngles;
                UIElementReference.Instance.m_firstViewPoint.transform.localEulerAngles = UIElementReference.Instance.m_nextViewPoint.transform.localEulerAngles;
                UIElementReference.Instance.m_nextViewPoint.transform.localEulerAngles = temp;

                UIElementReference.Instance.m_waterMark.SetActive(true);
                break;
            }
            else
            {
                timer += Time.deltaTime;
                UIElementReference.Instance.m_firstViewPoint.GetComponent<MeshRenderer>().material.SetFloat("Alpha", Mathf.Lerp(1f, 0f, timer / duration));
                UIElementReference.Instance.m_nextViewPoint.GetComponent<MeshRenderer>().material.SetFloat("Alpha", Mathf.Lerp(0f, 1f, timer / duration));
                Camera.main.fieldOfView = 150f - (timer / duration) * 90f;
            }
            yield return null;
        }

        Camera.main.GetComponent<RapidBlurEffect>().enabled = false;

        InstantiateInterfaceItem();
    }
}

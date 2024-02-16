using System.Collections;
using System.Collections.Generic;
using Reference;
using UnityEngine;
using UnityEngine.UI;

public class UIElement_LoungeHeader : MonoBehaviour
{
    public int m_index;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }
    private void OnClick()
    {
        GameEventReference.Instance.OnClickHeaderButton.Trigger(m_index);
    }
    private void OnDestroy()
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
    }
}

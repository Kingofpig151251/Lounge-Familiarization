using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewPointReference : Singleton<ViewPointReference>
{   
    public List<int> m_loungeStartIndex; // 0 = Deck, 1 = Wing. 2 = Pier
    public List<ViewPoint> m_viewPointSO;
}

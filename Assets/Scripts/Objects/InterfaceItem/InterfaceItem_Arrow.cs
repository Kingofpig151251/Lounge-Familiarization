using Reference;

public class InterfaceItem_Arrow : InterfaceItem
{
    public int m_nextViewPointIndex;

    public override void OnClick()
    {
        GameEventReference.Instance.OnEnterViewPoint.Trigger(m_nextViewPointIndex);
    }
}
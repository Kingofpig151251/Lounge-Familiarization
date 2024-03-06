using System.Collections.Generic;

namespace Reference
{
    public class ViewPointReference : Singleton<ViewPointReference>
    {
        public List<int>
            m_loungeStartIndex; //     DeckBusinessLounge,WingFristClassLounge,WingBusinessLounge,PierFirstClassLounge,PierBusinessLounge

        public List<ViewPoint> m_viewPointSO;
    }
}
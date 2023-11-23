namespace Manager
{
    public class IntroducePanelManager:Singleton<IntroducePanelManager>
    {
        public int step = 0;

        private void Start()
        {
            GameEventReference.Instance.OnClickNextButton.AddListener(OnClickNextButton);
        }

        private void OnClickNextButton(params object[] param)
        {
            for (int i = 0; i < UIElementReference.Instance.m_IntroduceText.Count; i++)
            {
                UIElementReference.Instance.m_IntroduceText[i].SetActive(false);
            }

            if (step < UIElementReference.Instance.m_IntroduceText.Count)
            {
                UIElementReference.Instance.m_IntroduceText[step].SetActive(true);
                step++;
            }
            else
            {
                UIElementReference.Instance.m_IntroducePanel.SetActive(false);
                UIElementReference.Instance.m_CityMapPanel.SetActive(true);
                step = 3;
            }
        }
    }
}
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Reference
{
    public class UIElementReference : Singleton<UIElementReference>
    {
        [Header("UI Panels")] public GameObject m_CityMapPanel;
        public GameObject m_FloorPlanPanel;
        public GameObject m_MessagePanel;
        public GameObject m_IntroducePanel;
        public GameObject m_InfoPanel;
        public GameObject m_360teachingPanel;
        public GameObject m_TaskteachingPanel;
        public GameObject m_questionPanel;
        public GameObject m_navigatePanel;
        public GameObject m_popupWindow;
        public GameObject m_correctPanel;
        public GameObject m_wrongPanel;

        [Header("UI Buttons")] public GameObject m_InfoPanelExpandButton;
        public List<GameObject> m_ButtonENG;
        public List<GameObject> m_ButtonSC;
        public List<GameObject> m_ButtonTC;
        public GameObject m_GameModeSwitcher;
        public GameObject m_FloorPlanButton;
        public GameObject m_firstViewPoint;
        public GameObject m_nextViewPoint;
        public GameObject m_homeButton;
        public GameObject m_taskModeButton;
        public GameObject m_exitNavigateButton;
        public GameObject m_confirmNavigateButton;
        public GameObject m_popupWindowEnterButton;
        public GameObject m_popupWindowExitButton;
        public GameObject m_OkButton;
        public GameObject m_nextTaskButton;
        public GameObject m_confirmButton;

        [Header("UI Texts")] public TextMeshProUGUI m_taskText;
        public TextMeshProUGUI m_taskCorrectRate;
        public List<GameObject> m_curentLounghText;

        [Header("UI Lists")] public List<GameObject> m_FloorPlanImage = new List<GameObject>();
        public List<GameObject> m_infoPanel_LoungeView = new List<GameObject>();
        public List<GameObject> m_infoPanel_LoungeHeaderButton = new List<GameObject>();
        public List<GameObject> m_infoPanel_ClassView = new List<GameObject>();
        public List<GameObject> m_infoPanel_ClassHeader = new List<GameObject>();
        public List<GameObject> m_infoPanel_ClassHeaderButton = new List<GameObject>();
        public List<GameObject> m_floorPlan_LocationButton = new List<GameObject>();
        public List<QAConfigSO> m_questionList;
        public List<DialogueSO> m_dialogueList;

        [Header("Sprites")] public Sprite m_expandButton;
        public Sprite m_collapseButton;
        public Sprite m_locationButton;
        public Sprite m_activeLocationButton;

        [Header("Others")] public GameObject m_Grid;
        public GameObject m_waterMark;
        public GameObject m_questionBox;
        public GameObject m_nextQuestionButton;
        public GameObject m_taskList;
        public GameObject m_popupWindowTitle;
        public GameObject m_popupWindowMessage;

        [Header("SO")] public InfoSO m_LoungeInfoSo;
    }
}
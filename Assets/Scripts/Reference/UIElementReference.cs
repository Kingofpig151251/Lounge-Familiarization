using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class UIElementReference : Singleton<UIElementReference>
{
    public GameObject m_WorldMapPanel;
    public GameObject m_CityMapPanel;
    public GameObject m_FloorPlanPanel;
    public GameObject m_MessagePanel;

    public GameObject m_Grid;

    //public GameObject m_Layer2sheKouBlock;
    //public GameObject m_Layer2hongKongBlock;
    public GameObject m_IntroducePanel;
    public List<GameObject> m_IntroduceText = new List<GameObject>();
    public GameObject m_InfoPanel;
    public GameObject m_InfoPanelExpandButton;
    public GameObject m_TopBar;
    public GameObject m_TopBarENG;
    public GameObject m_TopBarSC;
    public GameObject m_TopBarTC;
    public GameObject m_GameModeSwitcher;
    public GameObject m_FloorPlanButton;

    public GameObject m_firstViewPoint;
    public GameObject m_nextViewPoint;
    public GameObject m_waterMark;

    public List<GameObject> m_infoPanel_LoungeView = new List<GameObject>();
    public List<GameObject> m_infoPanel_LoungeHeaderButton = new List<GameObject>();
    public List<GameObject> m_infoPanel_ClassView = new List<GameObject>();
    public List<GameObject> m_infoPanel_ClassHeader = new List<GameObject>();
    public List<GameObject> m_infoPanel_ClassHeaderButton = new List<GameObject>();
    public List<GameObject> m_floorPlan_LocationButton = new List<GameObject>();

    public GameObject m_questionPanel;
    public GameObject m_questionBox;
    public List<GameObject> m_answerList;

    public List<QAConfigSO> m_questionList;

    public GameObject m_navigatePanel;
    public GameObject m_nextQuestionButton;
    public GameObject m_enterNavigateButton;
    public GameObject m_exitNavigateButton;
    public GameObject m_confirmNavigateButton;

    public GameObject m_taskList;
    public TextMeshProUGUI m_taskText;
    public TextMeshProUGUI m_taskCorrectRate;

    public Sprite m_expandButton;
    public Sprite m_collapseButton;
    public Sprite m_locationButton;
    public Sprite m_activeLocationButton;
}
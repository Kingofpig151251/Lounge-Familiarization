using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElementReference : Singleton<UIElementReference>
{
    public GameObject m_layerPanel1;
    public GameObject m_layerPanel2;
    public GameObject m_layerPanel3;
    public GameObject m_messagePanel;
    public GameObject m_Grid;
    public GameObject m_Layer2sheKouBlock;
    public GameObject m_Layer2hongKongBlock;
    public GameObject m_infoPanel;
    public GameObject m_infoPanelExpandButton;

    public GameObject m_firstViewPoint;
    public GameObject m_nextViewPoint;
    public GameObject m_waterMark;

    public List<GameObject> m_infoPanel_LoungeView = new List<GameObject>();
    public List<GameObject> m_infoPanel_LoungeHeaderButton = new List<GameObject>();
    public List<GameObject> m_infoPanel_ClassView = new List<GameObject>();
    public List<GameObject> m_infoPanel_ClassHeader = new List<GameObject>();
    public List<GameObject> m_infoPanel_ClassHeaderButton = new List<GameObject>();

    public Sprite m_expandButton;
    public Sprite m_collapseButton;

    public GameObject m_questionPanel;
    public GameObject m_questionBox;
    public List<GameObject> m_answerList;

    public List<QAConfigSO> m_questionList;

    public GameObject m_navigatePanel;
    public GameObject m_nextQuestionButton;
    public GameObject m_enterNavigateButton;
    public GameObject m_exitNavigateButton;
    public GameObject m_confirmNavigateButton;
}

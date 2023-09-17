using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventReference : Singleton<GameEventReference>
{
    public GameEvent OnInteract = new GameEvent();
    public GameEvent OnEnterViewPoint = new GameEvent();
    public GameEvent OnInteractInfo = new GameEvent();
    public GameEvent OnInteractUIMessage = new GameEvent();
    public GameEvent OnClickHeaderButton = new GameEvent();
    public GameEvent OnClickInfoButton = new GameEvent();
    public GameEvent OnClickInfoExpandButton = new GameEvent();
    public GameEvent OnChangeRegion = new GameEvent();
    public GameEvent OnClickLoungeHeader = new GameEvent();
    public GameEvent OnClickClassHeader = new GameEvent();
    public GameEvent OnClickFeaturePointHeader = new GameEvent();
    public GameEvent OpenTestPanel = new GameEvent();
    public GameEvent OnClickTestOption = new GameEvent();
    public GameEvent OnEnterNavigatePhase = new GameEvent();
    public GameEvent OnExitNavigatePhase = new GameEvent();
    public GameEvent OnConfirmNavigate = new GameEvent();
}
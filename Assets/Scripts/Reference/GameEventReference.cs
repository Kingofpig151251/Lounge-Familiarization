namespace Reference
{
    public class GameEventReference : Singleton<GameEventReference>
    {
        #region Interaction Events
        public GameEvent OnInteract = new GameEvent();
        public GameEvent OnInteractInfo = new GameEvent();
        public GameEvent OnInteractUIMessage = new GameEvent();
        #endregion

        #region View Events
        public GameEvent OnEnterViewPoint = new GameEvent();
        public GameEvent OnEnter360Mode = new GameEvent();
        public GameEvent OnEnterTaskMode = new GameEvent();
        #endregion

        #region Click Events
        public GameEvent OnClickHeaderButton = new GameEvent();
        public GameEvent OnClickInfoExpandButton = new GameEvent();
        public GameEvent OnClickLoungeHeader = new GameEvent();
        public GameEvent OnClickClassHeader = new GameEvent();
        public GameEvent OnClickTestOption = new GameEvent();
        public GameEvent OnClickFloorPlanButton = new GameEvent();
        public GameEvent OnClickSwitchClassButton = new GameEvent();
        public GameEvent OnClickIntroducePanelNextButton = new GameEvent();
        public GameEvent OnFeaturePointListExpandButtonClicked = new GameEvent();
        #endregion

        #region Navigation Events
        public GameEvent OnChangeRegion = new GameEvent();
        public GameEvent OnEnterNavigatePhase = new GameEvent();
        public GameEvent OnExitNavigatePhase = new GameEvent();
        public GameEvent OnConfirmNavigate = new GameEvent();
        #endregion

        #region Task Events
        public GameEvent OnTaskListExpand = new GameEvent();
        public GameEvent OnTaskListCollapse = new GameEvent();
        public GameEvent OnTaskAdded = new GameEvent();
        public GameEvent OnTaskCompleted = new GameEvent();
        public GameEvent OnTaskUpdate = new GameEvent();
        public GameEvent OnTaskRemoved = new GameEvent();
        #endregion

        #region Other Events
        public GameEvent OnLanguageChanged = new GameEvent();
        public GameEvent OnGameReset = new GameEvent();
        public GameEvent OpenTestPanel = new GameEvent();
        #endregion
    }
}
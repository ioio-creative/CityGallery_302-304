using RoboRyanTron.Unite2017.Events;
using SOVariables;
using System.Collections;
using UnityEngine;

public class Game303Manager : StateMachine {

	public static Game303Manager instance;

    [SerializeField] private Language currentLanguage;
    [SerializeField] private bool leftCircleSelected;
    [SerializeField] private bool rightCircleSelected;
    [SerializeField] private bool confirmPageShowed;
    [SerializeField] private int yearIndex;

    public float sandEffectClearBackObjectTime;
    public float transitionClearBackObjectTime;

    void Awake () {
        instance = this;
    }

    void Start () {
        Game303ConfigData.instance.LoadConfig ();
        sandEffectClearBackObjectTime = Game303ConfigData.instance.sandEffectMoveTime / 2f;
        transitionClearBackObjectTime = Game303ConfigData.instance.genericTransitionMoveTime / 2f;
        Game303Mediator.instance.setColorCoverAlphaDelegate += SetColorCoverAlpha;
        Game303Mediator.instance.selectDelegate += Select;
        Game303Mediator.instance.selectYearDelegate += SelectYear;
        Game303Mediator.instance.selectLanguageDelegate += SelectLanguage;
        Game303Mediator.instance.changeStatusDelegate += ChangeStatus;
        ChangeStatus (Status.Idle);
    }

    protected override void OnAnyStatusStay () {

    }

    protected override void OnIdleStatusEnter () {
        if (previousStatus == Status.Confirm ||
            previousStatus == Status.SelectYear) {
            Game303FlipdotView.instance.PassSand (true);
            Game303FlipdotView.instance.HideSelectYearPage ();
            Game303FlipdotView.instance.HideMountain (transitionClearBackObjectTime);
            Game303SequenceView.instance.Clear (transitionClearBackObjectTime);
            Game303TutorialView.instance.UnactivePageBlock(Game303ConfigData.instance.genericTransitionMoveTime);
        }
        else {
            Game303FlipdotView.instance.HideTutorialPage ();
            Game303TutorialView.instance.UnactivePageBlock();
        }
        Game303TutorialView.instance.HideSelectLanguagePage ();
        Game303TutorialView.instance.HideLeftHandPage ();
        Game303TutorialView.instance.HideRightHandPage ();
        Game303TutorialView.instance.HideConfirmPage ();
        Game303TutorialView.instance.HideReadyPage ();
        
        Game303TutorialView.instance.ShowIdlePage ();
        Game303FlipdotView.instance.HideLine();
        //SandIO
        SandIOControl.SendSandIO(SandIOControl.SandTransition.IDLE);
        //IdleBuilding
        IdleBuilding303.instance.ShowIdleBuilding();
    }

    protected override void OnIdleStatusStay () {

    }

    protected override void OnPlayerInStatusEnter () {

    }

    protected override void OnPlayerInStatusStay () {

    }

    protected override void OnSelectLanguageStatusEnter () {
        if (previousStatus == Status.Confirm ||
            previousStatus == Status.SelectYear) 
        {
            Game303FlipdotView.instance.PassSand (true);
            Game303FlipdotView.instance.HideSelectYearPage ();
            Game303FlipdotView.instance.HideMountain (transitionClearBackObjectTime);
            Game303SequenceView.instance.Clear (transitionClearBackObjectTime);
        }
        Game303TutorialView.instance.HideIdlePage ();
        Game303TutorialView.instance.HideLeftHandPage ();
        Game303TutorialView.instance.HideRightHandPage ();
        Game303TutorialView.instance.HideConfirmPage ();
        Game303TutorialView.instance.HideReadyPage ();
        Game303TutorialView.instance.UnactivePageBlock ();
        Game303TutorialView.instance.ShowSelectLanguagePage ();

        //SandIO
        SandIOControl.SendSandIO(SandIOControl.SandTransition.ACTIVATE);
        //IdleBuilding
        IdleBuilding303.instance.HideIdleBuilding();
    }

    protected override void OnSelectLanguageStatusStay () {

    }

    protected override void OnTutorialStatusEnter () {
        leftCircleSelected = false;
        rightCircleSelected = false;
        confirmPageShowed = false;
        Game303FlipdotView.instance.HideLine ();
        Game303TutorialView.instance.HideReadyPage ();
        Game303FlipdotView.instance.ShowTutorialPage ();
        Game303TutorialView.instance.HideSelectLanguagePage ();
        Game303TutorialView.instance.ShowLeftHandPage ();

        RaiseTutorialSOEvent();

        //SandIO
        if (previousStatus == Status.Ready)
        {
            SandIOControl.SendSandIO(SandIOControl.SandTransition.RESTART);
        }
        else
        {
            SandIOControl.SendSandIO(SandIOControl.SandTransition.L0);
        }
    }

    protected override void OnTutorialStatusStay () {
        if (leftCircleSelected && rightCircleSelected) {
            if (!confirmPageShowed) {
                confirmPageShowed = true;
                Game303TutorialView.instance.HideRightHandPage ();

                //Skip Confirm Page and directly go to Ready Status
                //Game303TutorialView.instance.ShowConfirmPage ();
                ChangeStatus(Status.Ready);

                //SandIO
                SandIOControl.SendSandIO(SandIOControl.SandTransition.R1);
            }
        }
    }

    protected override void OnReadyStatusEnter () {
        //Game303FlipdotView.instance.ShowLine ();
        //Game303FlipdotView.instance.HideTutorialPage ();
        Game303TutorialView.instance.HideConfirmPage ();
        Game303TutorialView.instance.ShowReadyPage ();

        
    }

    protected override void OnReadyStatusStay () {

    }

    protected override void OnSelectYearStatusEnter () {
        Game303FlipdotView.instance.HideTutorialPage ();
        Game303TutorialView.instance.ActivePageBlock ();
        if (previousStatus == Status.SelectYear)
        {
            return;
        }
        else if (previousStatus == Status.Confirm) {
            Game303FlipdotView.instance.PassSand (true);
            Game303FlipdotView.instance.SelectYear (yearIndex, transitionClearBackObjectTime);
            Game303SequenceView.instance.Clear (transitionClearBackObjectTime);

            //SandIO
            SandIOControl.SendSandIO(SandIOControl.SandTransition.SKIP);
        }
        else {

            switch (previousStatus)
            {
                case Status.SelectYear:
                case Status.Confirm:
                    break;

                case Status.Ready:
                    SandIOControl.SendSandIO(SandIOControl.SandTransition.C1);
                    RaisePlayerEnterSOEventFromIdle();
                    goto default;
                
                case Status.Tutorial:
                    RaisePlayerEnterSOEventFromIdle();
                    SandIOControl.SendSandIO(SandIOControl.SandTransition.SKIP);
                    goto default;

                case Status.SelectLanguage:
                case Status.Idle:
                    SandIOControl.SendSandIO(SandIOControl.SandTransition.SKIP);
                    goto default;
                
                default:
                    Game303FlipdotView.instance.PassSand(true);
                    Game303FlipdotView.instance.HideLine(transitionClearBackObjectTime);
                    Game303FlipdotView.instance.ShowMountain(transitionClearBackObjectTime);
                    Game303FlipdotView.instance.SelectYear(0, transitionClearBackObjectTime);
                    yearIndex = 0;

                    
                    break;
            }
            
        }
    }

    protected override void OnSelectYearStatusStay () {

    }

    protected override void OnConfirmStatusEnter () {
        Game303FlipdotView.instance.PassSand (false);
        Game303FlipdotView.instance.HideSelectYearPage ();
        Game303SequenceView.instance.Play (yearIndex, Game303ConfigData.instance.sandEffectMoveTime);
    }

    protected override void OnConfirmStatusStay () {

    }

    public void Select (Direction direction) {
        if (CheckStatus (Status.Tutorial) || CheckStatus(Status.Ready)) {
            if (direction == Direction.Left) {
                if (!leftCircleSelected) {
                    leftCircleSelected = true;
                    Game303TutorialView.instance.HideLeftHandPage ();
                    Game303TutorialView.instance.ShowRightHandPage ();

                    //SandIO
                    SandIOControl.SendSandIO(SandIOControl.SandTransition.L1);
                }
                Game303FlipdotView.instance.SelectLeftCircle ();
            }
            if (direction == Direction.Right) {
                if (leftCircleSelected) {
                    rightCircleSelected = true;
                }
                Game303FlipdotView.instance.SelectRightCircle ();
            }
        }
        if (CheckStatus (Status.SelectYear)) {
            if (direction == Direction.Left) {
                if (yearIndex > 0) {
                    yearIndex -= 1;
                    Game303FlipdotView.instance.SelectYear(yearIndex);
                }
            }
            if (direction == Direction.Right) {
                if (yearIndex < Game303FlipdotView.instance.YearTextCount() - 1) {
                    yearIndex += 1;
                    Game303FlipdotView.instance.SelectYear (yearIndex);
                }
            }
        }
    }

    public int SelectYear (int index) {
        if (index >= 0 && index < Game303FlipdotView.instance.YearTextCount()) {
            yearIndex = index;
            Game303FlipdotView.instance.SelectYear (yearIndex);
        }
        return yearIndex;
    }

    public void SetColorCoverAlpha (float alpha) {
        Game303TutorialView.instance.SetColorCoverAlpha (alpha);
    }

    public void SelectLanguage (int index) {
        SelectLanguage((Language)index);
    }

    public void SelectLanguage (Language language) {
        currentLanguage = language;
        Game303TutorialView.instance.SwitchLanguage (language);
        //By Hugo
        RaiseLanguageSelectSOEvent();
    }

    //By Hugo
    [SerializeField] private GameIntEvent onSelectLangEvnt;
    [SerializeField] private GameEvent onTutorialEvnt;
    [SerializeField] private GameEvent onPlayerEnterEvnt;
    [SerializeField] private GameIntEvent onNaviIdxEvnt;

    private void RaiseLanguageSelectSOEvent()
    {
        onSelectLangEvnt.Raise((int)currentLanguage);
    }

    private void RaiseTutorialSOEvent()
    {
        onTutorialEvnt.Raise();
    }

    private void RaisePlayerEnterSOEventFromIdle()
    {  
        onPlayerEnterEvnt.Raise();
        onNaviIdxEvnt.Raise(0);
    }
}

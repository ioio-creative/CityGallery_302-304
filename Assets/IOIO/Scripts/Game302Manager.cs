using RoboRyanTron.Unite2017.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game302Manager : StateMachine {

	public static Game302Manager instance;

    [SerializeField] private Language currentLanguage;
    [SerializeField] private Direction circleSelect;
    [SerializeField] private bool leftCircleSelected;
    [SerializeField] private bool rightCircleSelected;
    [SerializeField] private bool confirmPageShowed;
    [SerializeField] public int previousYearIndex;
    [SerializeField] public int currentYearIndex;

    void Awake () {
        instance = this;
    }

    void Start () {
        Game302Mediator.instance.setColorCoverAlphaDelegate += SetColorCoverAlpha;
        Game302Mediator.instance.selectDelegate += Select;
        Game302Mediator.instance.selectYearDelegate += SelectYear;
        Game302Mediator.instance.selectLanguageDelegate += SelectLanguage;
        Game302Mediator.instance.changeStatusDelegate += ChangeStatus;
        ChangeStatus (Status.Idle);
    }

    protected override void OnAnyStatusStay () {

    }

    protected override void OnIdleStatusEnter () {
        if (previousStatus == Status.Confirm ||
            previousStatus == Status.SelectYear) {
            Game302View.instance.HideConfirmPage ();
        }
        else {
            Game302View.instance.HideTutorialPage ();
        }
        Game303TutorialView.instance.HideSelectLanguagePage ();
        Game303TutorialView.instance.HideLeftHandPage ();
        Game303TutorialView.instance.HideRightHandPage ();
        Game303TutorialView.instance.HideConfirmPage ();
        Game303TutorialView.instance.HideReadyPage ();
        Game303TutorialView.instance.UnactivePageBlock (1);
        Game303TutorialView.instance.ShowIdlePage ();
    }

    protected override void OnIdleStatusStay () {

    }

    protected override void OnPlayerInStatusEnter () {

    }

    protected override void OnPlayerInStatusStay () {

    }

    protected override void OnSelectLanguageStatusEnter () {
        if (previousStatus == Status.Confirm ||
            previousStatus == Status.SelectYear) {

        }
        Game303TutorialView.instance.HideIdlePage ();
        Game303TutorialView.instance.HideLeftHandPage ();
        Game303TutorialView.instance.HideRightHandPage ();
        Game303TutorialView.instance.HideConfirmPage ();
        Game303TutorialView.instance.HideReadyPage ();
        Game303TutorialView.instance.UnactivePageBlock (1);
        Game303TutorialView.instance.ShowSelectLanguagePage ();
    }

    protected override void OnSelectLanguageStatusStay () {

    }

    protected override void OnTutorialStatusEnter () {
        leftCircleSelected = false;
        rightCircleSelected = false;
        confirmPageShowed = false;
        Game303TutorialView.instance.HideReadyPage ();
        Game303TutorialView.instance.HideSelectLanguagePage ();
        Game303TutorialView.instance.ShowLeftHandPage ();

        Game302View.instance.HideConfirmPage ();
        Game302View.instance.ShowTutorialPage ();

        RaiseTutorialSOEvent ();       
    }

    protected override void OnTutorialStatusStay () {
        if (leftCircleSelected && rightCircleSelected) {
            if (!confirmPageShowed) {
                confirmPageShowed = true;
                Game303TutorialView.instance.HideRightHandPage ();
                Game303TutorialView.instance.ShowConfirmPage ();
            }
        }
    }

    protected override void OnReadyStatusEnter () {
        Game303TutorialView.instance.HideConfirmPage ();
        Game303TutorialView.instance.ShowReadyPage ();
    }

    protected override void OnReadyStatusStay () {

    }

    protected override void OnSelectYearStatusEnter () {
        currentYearIndex = 0;
        Game302View.instance.HideTutorialPage ();
        Game302View.instance.ShowSelectPage ();
        Game303TutorialView.instance.ActivePageBlock ();

        if (previousStatus == Status.Ready || previousStatus == Status.Tutorial)
        {
            RaisePlayerEnterSOEventFromIdle();
        }
    }

    protected override void OnSelectYearStatusStay () {

    }

    protected override void OnConfirmStatusEnter () {
        Game302View.instance.ShowConfirmPage ();
    }

    protected override void OnConfirmStatusStay () {

    }

    public void Select (Direction direction) {
        if (CheckStatus (Status.Tutorial)) {
            if (direction == Direction.Left) {
                if (!leftCircleSelected) {
                    leftCircleSelected = true;
                    Game303TutorialView.instance.HideLeftHandPage ();
                    Game303TutorialView.instance.ShowRightHandPage ();
                }
                Game302View.instance.SelectLeftCircle ();
            }
            if (direction == Direction.Right) {
                if (leftCircleSelected) {
                    rightCircleSelected = true;
                }
                Game302View.instance.SelectRightCircle ();
            }
        }
        if (CheckStatus (Status.SelectYear) ||
            CheckStatus (Status.Confirm)) {
            if (direction == Direction.Left) {
                if (currentYearIndex > 0) {
                    previousYearIndex = currentYearIndex;
                    currentYearIndex -= 1;
                    Game302View.instance.SelectYear (currentYearIndex);
                }
            }
            if (direction == Direction.Right) {
                if (currentYearIndex < Game302View.instance.YearTextCount () - 1) {
                    previousYearIndex = currentYearIndex;
                    currentYearIndex += 1;
                    Game302View.instance.SelectYear (currentYearIndex);
                }
            }
        }
    }

    public void NextStage () {
        if (CheckStatus (Status.Tutorial)) {
            ChangeStatus (Status.SelectYear);
        }
        else if (CheckStatus (Status.SelectYear)) {
            ChangeStatus (Status.Confirm);
        }
        else if (CheckStatus (Status.Confirm)) {
            ChangeStatus (Status.Tutorial);
        }
    }

    public int SelectYear (int index) {
        previousYearIndex = currentYearIndex;
        currentYearIndex = index;
        Game302View.instance.SelectYear (currentYearIndex);
        return currentYearIndex;
    }

        public void SetColorCoverAlpha (float alpha) {
        Game303TutorialView.instance.SetColorCoverAlpha (alpha);
    }

    public void SelectLanguage (int index) {
        currentLanguage = (Language)index;
        Game303TutorialView.instance.SwitchLanguage ((Language)index);
        //By Hugo
        RaiseLanguageSelectSOEvent ();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game303Manager : StateMachine {

	public static Game303Manager instance;

    [SerializeField] private Language currentLanguage;
    [SerializeField] private bool leftCircleSelected;
    [SerializeField] private bool rightCircleSelected;
    [SerializeField] private bool confirmPageShowed;
    [SerializeField] private int yearIndex;

    public float sandEffectClearBackObjectTime;

    void Awake () {
        instance = this;
    }

    void Start () {
        Game303ConfigData.instance.LoadConfig ();
        sandEffectClearBackObjectTime = Game303ConfigData.instance.sandEffectMoveTime / 4;
        Game303Mediator.instance.selectDelegate += Select;
        Game303Mediator.instance.selectYearDelegate += SelectYear;
        Game303Mediator.instance.changeStatusDelegate += ChangeStatus;
        ChangeStatus (Status.SelectLanguage);
    }

    protected override void OnAnyStatusStay () {

    }

    protected override void OnIdleStatusEnter () {
        if (previousStatus == Status.Confirm ||
            previousStatus == Status.SelectYear) {
            Game303FlipdotView.instance.PassSand ();
            Game303FlipdotView.instance.HideSelectYearPage ();
            Game303FlipdotView.instance.HideMountain (sandEffectClearBackObjectTime);
            Game303SequenceView.instance.Clear (sandEffectClearBackObjectTime);
            Game303TutorialView.instance.UnactivePageBlock (Game303ConfigData.instance.sandEffectMoveTime);
            Game303TutorialView.instance.HideReadyPage ();
        }
        else {
            Game303FlipdotView.instance.HideTutorialPage ();
        }
    }

    protected override void OnIdleStatusStay () {

    }

    protected override void OnPlayerInStatusEnter () {
        Game303FlipdotView.instance.HideLine ();
    }

    protected override void OnPlayerInStatusStay () {

    }

    protected override void OnSelectLanguageStatusEnter () {
        Game303TutorialView.instance.UnactivePageBlock (Game303ConfigData.instance.sandEffectMoveTime);
        Game303TutorialView.instance.ShowSelectLanguagePage ();
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
        Game303FlipdotView.instance.ShowLine ();
        Game303FlipdotView.instance.HideTutorialPage ();
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
            Game303FlipdotView.instance.PassSand ();
            Game303FlipdotView.instance.SelectYear (yearIndex, sandEffectClearBackObjectTime);
            Game303SequenceView.instance.Clear (sandEffectClearBackObjectTime);
        }
        else {
            Game303FlipdotView.instance.PassSand ();
            Game303FlipdotView.instance.HideLine (sandEffectClearBackObjectTime);
            Game303FlipdotView.instance.ShowMountain (sandEffectClearBackObjectTime);
            Game303FlipdotView.instance.SelectYear (0, sandEffectClearBackObjectTime);
            yearIndex = 0;
        }
    }

    protected override void OnSelectYearStatusStay () {

    }

    protected override void OnConfirmStatusEnter () {
        Game303FlipdotView.instance.PassSand ();
        Game303FlipdotView.instance.HideSelectYearPage ();
        Game303SequenceView.instance.Play (yearIndex, Game303ConfigData.instance.sandEffectMoveTime);
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
                    Game303FlipdotView.instance.SelectYear (yearIndex);
                }
            }
            if (direction == Direction.Right) {
                if (yearIndex < Game303FlipdotView.instance.YearTextCount () - 1) {
                    yearIndex += 1;
                    Game303FlipdotView.instance.SelectYear (yearIndex);
                }
            }
        }
    }

    public void SelectYear (int index) {
        if (index > 0 && index < Game303FlipdotView.instance.YearTextCount ()) {
            yearIndex = index;
            Game303FlipdotView.instance.SelectYear (yearIndex);
        }
    }

    public void SelectLanguage (Language language) {
        currentLanguage = language;
        Game303TutorialView.instance.SwitchLanguage (language);
    }

}

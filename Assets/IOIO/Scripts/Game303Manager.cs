using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game303Manager : StateMachine {

	public static Game303Manager instance;

    [SerializeField] private Direction circleSelect;
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
        ChangeStatus (Status.Idle);
    }

    protected override void OnAnyStatusStay () {

    }

    protected override void OnIdleStatusEnter () {
        if (previousStatus == Status.Confirm ||
            previousStatus == Status.SelectYear) {
            Game303View.instance.PassSand ();
            Game303View.instance.HideSelectYearPage ();
            Game303View.instance.HideMountain (sandEffectClearBackObjectTime);
            Game303SequenceView.instance.Clear (sandEffectClearBackObjectTime);
            //Game303View.instance.ShowLine (2);
        }
        else {
            Game303View.instance.HideTutorialPage ();
            //Game303View.instance.ShowLine ();
        }
    }

    protected override void OnIdleStatusStay () {

    }

    protected override void OnPlayerInStatusEnter () {
        Game303View.instance.HideLine ();
    }

    protected override void OnPlayerInStatusStay () {

    }

    protected override void OnSelectLanguageStatusEnter () {
        //Game303View.instance.ShowLine ();
    }

    protected override void OnSelectLanguageStatusStay () {

    }

    protected override void OnTutorialStatusEnter () {
        Game303View.instance.HideLine ();
        Game303View.instance.ShowTutorialPage ();
        Select (Direction.Left);
    }

    protected override void OnTutorialStatusStay () {

    }

    protected override void OnReadyStatusEnter () {
        Game303View.instance.ShowLine ();
        Game303View.instance.HideTutorialPage ();
    }

    protected override void OnReadyStatusStay () {

    }

    protected override void OnSelectYearStatusEnter () {
        if (previousStatus == Status.Confirm) {
            Game303View.instance.PassSand ();
            Game303SequenceView.instance.Clear (sandEffectClearBackObjectTime);
            Game303View.instance.SelectYear (yearIndex, sandEffectClearBackObjectTime);
        }
        else {
            Game303View.instance.PassSand ();
            Game303View.instance.HideLine (sandEffectClearBackObjectTime);
            Game303View.instance.ShowMountain (sandEffectClearBackObjectTime);
            Game303View.instance.SelectYear (0, sandEffectClearBackObjectTime);
            yearIndex = 0;
        }
    }

    protected override void OnSelectYearStatusStay () {

    }

    protected override void OnConfirmStatusEnter () {
        Game303View.instance.PassSand ();
        Game303View.instance.HideSelectYearPage ();
        Game303SequenceView.instance.Play (yearIndex, Game303ConfigData.instance.sandEffectMoveTime);
    }

    protected override void OnConfirmStatusStay () {

    }

    public void Select (Direction direction) {
        if (CheckStatus (Status.Tutorial)) {
            if (direction == Direction.Left) {
                circleSelect = Direction.Left;
                Game303View.instance.SelectLeftCircle ();
            }
            if (direction == Direction.Right) {
                circleSelect = Direction.Right;
                Game303View.instance.SelectRightCircle ();
            }
        }
        if (CheckStatus (Status.SelectYear)) {
            if (direction == Direction.Left) {
                if (yearIndex > 0) {
                    yearIndex -= 1;
                    Game303View.instance.SelectYear (yearIndex);
                }
            }
            if (direction == Direction.Right) {
                if (yearIndex < Game303View.instance.YearTextCount () - 1) {
                    yearIndex += 1;
                    Game303View.instance.SelectYear (yearIndex);
                }
            }
        }
    }

    public void SelectYear (int index) {
        yearIndex = index;
        Game303View.instance.SelectYear (yearIndex);
    }

}

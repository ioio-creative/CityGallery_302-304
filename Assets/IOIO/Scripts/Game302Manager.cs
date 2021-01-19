using RoboRyanTron.Unite2017.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game302Manager : StateMachine {

	public static Game302Manager instance;

    [SerializeField] private Direction circleSelect;
    [SerializeField] public int previousYearIndex;
    [SerializeField] public int currentYearIndex;

    void Awake () {
        instance = this;
    }

    void Start () {
        Game302Mediator.instance.selectDelegate += Select;
        Game302Mediator.instance.selectYearDelegate += SelectYear;
        Game302Mediator.instance.changeStatusDelegate += ChangeStatus;
        ChangeStatus (Status.Tutorial);
    }

    protected override void OnAnyStatusStay () {

    }

    protected override void OnTutorialStatusEnter () {
        Game302View.instance.HideConfirmPage ();
        Game302View.instance.ShowTutorialPage ();
        Select (Direction.Left);
    }

    protected override void OnTutorialStatusStay () {

    }

    protected override void OnSelectYearStatusEnter () {
        currentYearIndex = 0;
        Game302View.instance.HideTutorialPage ();
        Game302View.instance.ShowSelectPage ();
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
                circleSelect = Direction.Left;
                Game302View.instance.SelectLeftCircle ();
            }
            if (direction == Direction.Right) {
                circleSelect = Direction.Right;
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

    public void SelectYear (int index) {
        previousYearIndex = currentYearIndex;
        currentYearIndex = index;
        Game302View.instance.SelectYear (currentYearIndex);
    }


    //By Hugo
    [SerializeField] private GameIntEvent onSelectLangEvnt;
    [SerializeField] private GameEvent onTutorialEvnt;
    [SerializeField] private GameEvent onPlayerEnterEvnt;
    [SerializeField] private GameIntEvent onNaviIdxEvnt;
    private void RaiseLanguageSelectSOEvent()
    {
        //onSelectLangEvnt.Raise((int)currentLanguage);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour {

    [Header ("Status - Setting")]
    [SerializeField] private bool useFixedUpdate = false;

    [Header ("Status - Debug")]
    [SerializeField] protected Status previousStatus;
    [SerializeField] protected Status currentStatus;
    [SerializeField] protected float statusTimer;
    [SerializeField] private bool changeStatus;

    void FixedUpdate () {
        if (useFixedUpdate) {
            UpdateStatus ();
        }
    }

    void Update () {
        if (!useFixedUpdate) {
            UpdateStatus ();
        }
    }

    private void UpdateStatus () {
        statusTimer += Time.deltaTime;

        OnAnyStatusStay ();

        if (currentStatus == Status.Idle) {
            if (changeStatus) {
                OnIdleStatusEnter ();
                changeStatus = false;
            }
            OnIdleStatusStay ();
        }
        if (currentStatus == Status.PlayerIn) {
            if (changeStatus) {
                OnPlayerInStatusEnter ();
                changeStatus = false;
            }
            OnPlayerInStatusStay ();
        }
        else if (currentStatus == Status.SelectLanguage) {
            if (changeStatus) {
                OnSelectLanguageStatusEnter ();
                changeStatus = false;
            }
            OnSelectLanguageStatusStay ();
        }
        else if (currentStatus == Status.Tutorial) {
            if (changeStatus) {
                OnTutorialStatusEnter ();
                changeStatus = false;
            }
            OnTutorialStatusStay ();
        }
        else if (currentStatus == Status.Ready) {
            if (changeStatus) {
                OnReadyStatusEnter ();
                changeStatus = false;
            }
            OnReadyStatusStay ();
        }
        else if (currentStatus == Status.SelectYear) {
            if (changeStatus) {
                OnSelectYearStatusEnter ();
                changeStatus = false;
            }
            OnSelectYearStatusStay ();
        }
        else if (currentStatus == Status.Confirm) {
            if (changeStatus) {
                OnConfirmStatusEnter ();
                changeStatus = false;
            }
            OnConfirmStatusStay ();
        }
    }

    protected virtual void OnAnyStatusStay () {

    }

    protected virtual void OnIdleStatusEnter () {

    }

    protected virtual void OnIdleStatusStay () {

    }

    protected virtual void OnPlayerInStatusEnter () {

    }

    protected virtual void OnPlayerInStatusStay () {

    }

    protected virtual void OnSelectLanguageStatusEnter () {

    }

    protected virtual void OnSelectLanguageStatusStay () {

    }

    protected virtual void OnTutorialStatusEnter () {

    }

    protected virtual void OnTutorialStatusStay () {

    }

    protected virtual void OnReadyStatusEnter () {

    }

    protected virtual void OnReadyStatusStay () {

    }

    protected virtual void OnSelectYearStatusEnter () {

    }

    protected virtual void OnSelectYearStatusStay () {

    }

    protected virtual void OnConfirmStatusEnter () {

    }

    protected virtual void OnConfirmStatusStay () {

    }

    public void ChangeStatus (Status targetStatus) {
        previousStatus = currentStatus;
        currentStatus = targetStatus;
        statusTimer = 0;
        changeStatus = true;
    }

    public void ChangeStatus (Status targetStatus, bool checkStatus) {
        if (!checkStatus || !CheckStatus (targetStatus)) {
            ChangeStatus (targetStatus);
        }
    }

    public bool CheckStatus (Status status) {
        if (currentStatus == status) {
            return true;
        }
        else {
            return false;
        }
    }

}

public enum Status {
    Idle,
    PlayerIn,
    SelectLanguage,
    Tutorial,
    Ready,
    SelectYear,
    Confirm,
}

public enum Direction {
    None, Left, Right,
}

public enum Language {
    TC, EN,
}
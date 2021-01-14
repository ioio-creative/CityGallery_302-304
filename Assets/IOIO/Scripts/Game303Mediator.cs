using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game303Mediator : MonoBehaviour {

	public static Game303Mediator instance;

    public delegate void SetColorCoverAlphaDelegate (float alpha);
    public SetColorCoverAlphaDelegate setColorCoverAlphaDelegate;

    public delegate void SelectDelegate (Direction direction);
    public SelectDelegate selectDelegate;

    public delegate void SelectYearDelegate (int index);
    public SelectYearDelegate selectYearDelegate;

    public delegate void ChangeStatusDelegate (Status status);
    public ChangeStatusDelegate changeStatusDelegate;

    void Awake () {
        instance = this;
    }

    public void SetColorCoverAlpha (float alpha) {
        setColorCoverAlphaDelegate.Invoke (alpha);
    }

    public void SelectLeft () {
        selectDelegate.Invoke (Direction.Left);
    }

    public void SelectRight () {
        selectDelegate.Invoke (Direction.Right);
    }

    public void SelectYear (int index) {
        selectYearDelegate.Invoke (index);
    }

    public void ChangeStatus (Status targetStatus) {
        changeStatusDelegate.Invoke (targetStatus);
    }

}

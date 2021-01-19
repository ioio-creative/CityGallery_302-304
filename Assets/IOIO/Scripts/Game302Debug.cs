using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game302Debug : MonoBehaviour {

    public static Game302Debug instance;

    void Awake () {
        instance = this;
    }

    void Start () {

    }

    void Update () {
        if (Input.GetKeyDown (KeyCode.Keypad1)) {
            Game302Mediator.instance.SetColorCoverAlpha (0.1f);
        }
        if (Input.GetKeyDown (KeyCode.Keypad2)) {
            Game302Mediator.instance.SetColorCoverAlpha (0.2f);
        }
        if (Input.GetKeyDown (KeyCode.Keypad3)) {
            Game302Mediator.instance.SetColorCoverAlpha (0.3f);
        }

        if (Input.GetKeyDown (KeyCode.A)) {
            Game302Mediator.instance.SelectLeft ();
        }
        if (Input.GetKeyDown (KeyCode.D)) {
            Game302Mediator.instance.SelectRight ();
        }
        if (Input.GetKeyDown (KeyCode.Alpha1)) {
            Game302Mediator.instance.ChangeStatus (Status.Idle);
        }
        if (Input.GetKeyDown (KeyCode.Alpha2)) {
            Game302Mediator.instance.ChangeStatus (Status.PlayerIn);
        }
        if (Input.GetKeyDown (KeyCode.Alpha3)) {
            Game302Mediator.instance.ChangeStatus (Status.SelectLanguage);
        }
        if (Input.GetKeyDown (KeyCode.Alpha4)) {
            Game302Mediator.instance.ChangeStatus (Status.Tutorial);
        }
        if (Input.GetKeyDown (KeyCode.Alpha5)) {
            Game302Mediator.instance.ChangeStatus (Status.Ready);
        }
        if (Input.GetKeyDown (KeyCode.Alpha6)) {
            Game302Mediator.instance.ChangeStatus (Status.SelectYear);
        }
        if (Input.GetKeyDown (KeyCode.Alpha7)) {
            Game302Mediator.instance.ChangeStatus (Status.Confirm);
        }

        if (Input.GetKeyDown (KeyCode.Z)) {
            Game302Mediator.instance.SelectYear (0);
        }
        if (Input.GetKeyDown (KeyCode.X)) {
            Game302Mediator.instance.SelectYear (1);
        }
        if (Input.GetKeyDown (KeyCode.C)) {
            Game302Mediator.instance.SelectYear (2);
        }
        if (Input.GetKeyDown (KeyCode.V)) {
            Game302Mediator.instance.SelectYear (3);
        }
        if (Input.GetKeyDown (KeyCode.B)) {
            Game302Mediator.instance.SelectYear (4);
        }

        if (Input.GetKeyDown (KeyCode.LeftArrow)) {
            Game302Mediator.instance.SelectLanguage (0);
        }
        if (Input.GetKeyDown (KeyCode.RightArrow)) {
            Game302Mediator.instance.SelectLanguage (1);
        }
    }

}

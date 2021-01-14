using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game303Debug : MonoBehaviour {

	public static Game303Debug instance;

    void Awake () {
        instance = this;
    }

    void Start () {
        
    }

    void Update () {
        if (Input.GetKeyDown (KeyCode.Keypad1)) {
            Game303Mediator.instance.SetColorCoverAlpha (0.1f);
        }
        if (Input.GetKeyDown (KeyCode.Keypad2)) {
            Game303Mediator.instance.SetColorCoverAlpha (0.2f);
        }
        if (Input.GetKeyDown (KeyCode.Keypad3)) {
            Game303Mediator.instance.SetColorCoverAlpha (0.3f);
        }

        if (Input.GetKeyDown (KeyCode.A)) {
            Game303Mediator.instance.SelectLeft ();
        }
        if (Input.GetKeyDown (KeyCode.D)) {
            Game303Mediator.instance.SelectRight ();
        }
        if (Input.GetKeyDown (KeyCode.Alpha1)) {
            Game303Mediator.instance.ChangeStatus (Status.Idle);
        }
        if (Input.GetKeyDown (KeyCode.Alpha2)) {
            Game303Mediator.instance.ChangeStatus (Status.PlayerIn);
        }
        if (Input.GetKeyDown (KeyCode.Alpha3)) {
            Game303Mediator.instance.ChangeStatus (Status.SelectLanguage);
        }
        if (Input.GetKeyDown (KeyCode.Alpha4)) {
            Game303Mediator.instance.ChangeStatus (Status.Tutorial);
        }
        if (Input.GetKeyDown (KeyCode.Alpha5)) {
            Game303Mediator.instance.ChangeStatus (Status.Ready);
        }
        if (Input.GetKeyDown (KeyCode.Alpha6)) {
            Game303Mediator.instance.ChangeStatus (Status.SelectYear);
        }
        if (Input.GetKeyDown (KeyCode.Alpha7)) {
            Game303Mediator.instance.ChangeStatus (Status.Confirm);
        }

        if (Input.GetKeyDown (KeyCode.Z)) {
            Game303Mediator.instance.SelectYear (0);
        }
        if (Input.GetKeyDown (KeyCode.X)) {
            Game303Mediator.instance.SelectYear (1);
        }
        if (Input.GetKeyDown (KeyCode.C)) {
            Game303Mediator.instance.SelectYear (2);
        }
        if (Input.GetKeyDown (KeyCode.V)) {
            Game303Mediator.instance.SelectYear (3);
        }
    }

}

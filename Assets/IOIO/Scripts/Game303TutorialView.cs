using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game303TutorialView : MonoBehaviour {

    public static Game303TutorialView instance;

    [Header ("Global")]
    [SerializeField] private Animator waveAnimator;
    [SerializeField] private Transform colorCover;
    [SerializeField] private Transform mainCircle;
    [SerializeField] private Transform welcomeLeft;
    [SerializeField] private Transform welcomeRight;
    [SerializeField] private Transform tutorialLeft;
    [SerializeField] private Transform tutorialRight;
    [SerializeField] private Transform skipButton;
    [SerializeField] private Transform replayButton;
    [SerializeField] private Transform tcButtonSmall;
    [SerializeField] private Transform enButtonSmall;

    [Header ("Select Language")]
    [SerializeField] private Transform tcButtonBig;
    [SerializeField] private Transform enButtonBig;
    [SerializeField] private Transform selectLanguageDescTC;
    [SerializeField] private Transform selectLanguageDescEN;

    [Header ("Left Hand")]
    [SerializeField] private Transform leftHandMan;
    [SerializeField] private Transform leftHandDescShortTC;
    [SerializeField] private Transform leftHandDescLongTC;

    [Header ("Right Hand")]
    [SerializeField] private Transform rightHandMan;
    [SerializeField] private Transform rightHandDescShortTC;
    [SerializeField] private Transform rightHandDescLongTC;

    [Header ("Confirm")]
    [SerializeField] private Transform confirmButton;
    [SerializeField] private Transform confirmDescShortTC;
    [SerializeField] private Transform confirmDescLongTC;

    [Header ("Ready")]
    [SerializeField] private Transform readyTextTC;
    [SerializeField] private Transform readyDescShortTC;
    [SerializeField] private Transform readyDescLongTC;

    void Awake () {
        instance = this;
    }

    void Start () {

    }

    void Update () {
        if (Input.GetKeyDown (KeyCode.N)) {
            PlayWaveEffect ();
        }
    }

    public void PlayWaveEffect () {
        waveAnimator.SetTrigger ("Play");
    }

    public void OnSelectPageTCButtonClick () {

    }

    public void OnSelectPageENButtonClick () {

    }

    public void OnGlobalPageTCButtonClick () {

    }

    public void OnGlobalPageENButtonClick () {

    }

    public void OnSkipTutorialButtonClick () {

    }

    public void OnConfirmButtonClick () {

    }

    public void OnReadyButtonClick () {

    }

    public void OnReplayButtonClick () {

    }

}

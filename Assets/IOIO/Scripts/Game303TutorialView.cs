﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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

    public void HideSelectLanguagePage () {
        tcButtonBig.DOScale (0, 0.5f);
        enButtonBig.DOScale (0, 0.5f);
        selectLanguageDescTC.DOScale (0, 0.5f);
        selectLanguageDescEN.DOScale (0, 0.5f);
        welcomeLeft.DOScale (0, 0.5f);
        welcomeRight.DOScale (0, 0.5f);
    }

    public void ShowLeftHandPage () {
        leftHandMan.DOScale (1, 0.5f);
        leftHandDescShortTC.DOScale (1, 0.5f);
        leftHandDescLongTC.DOScale (1, 0.5f);
        tutorialLeft.DOScale (1, 0.5f);
        tutorialRight.DOScale (1, 0.5f);
        skipButton.DOScale (1, 0.5f);
        tcButtonSmall.DOScale (1, 0.5f);
        enButtonSmall.DOScale (1, 0.5f);
    }

    public void HideLeftHandPage () {
        leftHandMan.DOScale (0, 0.5f);
        leftHandDescShortTC.DOScale (0, 0.5f);
        leftHandDescLongTC.DOScale (0, 0.5f);
    }

    public void SwitchLanguage (Language language) {
        if (language == Language.TC) {
            tcButtonSmall.Find ("Fill").GetComponent<MaskableGraphic> ().DOFade (1, 0);
            enButtonSmall.Find ("Fill").GetComponent<MaskableGraphic> ().DOFade (0, 0);
            tcButtonSmall.Find ("Text").GetComponent<MaskableGraphic> ().color = Color.black;
            enButtonSmall.Find ("Text").GetComponent<MaskableGraphic> ().color = Color.white;
        }
        else if (language == Language.EN) {
            tcButtonSmall.Find ("Fill").GetComponent<MaskableGraphic> ().DOFade (0, 0);
            enButtonSmall.Find ("Fill").GetComponent<MaskableGraphic> ().DOFade (1, 0);
            tcButtonSmall.Find ("Text").GetComponent<MaskableGraphic> ().color = Color.white;
            enButtonSmall.Find ("Text").GetComponent<MaskableGraphic> ().color = Color.black;
        }
    }

    public void OnSelectPageTCButtonClick () {
        Game303Manager.instance.SelectLanguage (Language.TC);
        Game303Manager.instance.ChangeStatus (Status.Tutorial);
    }

    public void OnSelectPageENButtonClick () {
        Game303Manager.instance.SelectLanguage (Language.EN);
        Game303Manager.instance.ChangeStatus (Status.Tutorial);
    }

    public void OnGlobalPageTCButtonClick () {
        Game303Manager.instance.SelectLanguage (Language.TC);
    }

    public void OnGlobalPageENButtonClick () {
        Game303Manager.instance.SelectLanguage (Language.EN);
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
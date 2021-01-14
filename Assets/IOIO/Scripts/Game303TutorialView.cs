using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Game303TutorialView : MonoBehaviour {

    public static Game303TutorialView instance;

    [Header ("Color")]
    [SerializeField] private Color pageCoverIdleColor;
    [SerializeField] private Color pageCoverEffectColor;
    [SerializeField] private Color mainCircleFillbarIdleColor;
    [SerializeField] private Color mainCircleFillbarEffectColor;

    [Header ("Global")]
    [SerializeField] private Animator waveAnimator;
    [SerializeField] private Transform colorCover;
    [SerializeField] private Transform mainCircle;
    [SerializeField] private Transform mainCircleFillbar;
    [SerializeField] private Transform welcomeLeft;
    [SerializeField] private Transform welcomeRight;
    [SerializeField] private Transform tutorialLeft;
    [SerializeField] private Transform tutorialRight;
    [SerializeField] private Transform skipButton;
    [SerializeField] private Transform replayButton;
    [SerializeField] private Transform tcButtonSmall;
    [SerializeField] private Transform enButtonSmall;
    [SerializeField] private Transform pageBlock;

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
    [SerializeField] private Transform readyButton;
    [SerializeField] private Transform readyTextTC;
    [SerializeField] private Transform readyDescShortTC;
    [SerializeField] private Transform readyDescLongTC;

    private IEnumerator unactivePageBlockCoroutine;

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

    public void ShowSelectLanguagePage () {
        mainCircle.DOScale (1, 0.5f);
        mainCircleFillbar.GetComponent<Image> ().DOFillAmount (0, 0.5f);
        tcButtonBig.DOScale (1, 0.5f);
        enButtonBig.DOScale (1, 0.5f);
        selectLanguageDescTC.DOScale (1, 0.5f);
        selectLanguageDescEN.DOScale (1, 0.5f);
        welcomeLeft.DOScale (1, 0.5f);
        welcomeRight.DOScale (1, 0.5f);
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
        PlayWaveEffect ();
        WholePageColorEffect ();
        mainCircleFillbar.GetComponent<Image> ().DOFillAmount (0.25f, 0.5f);
        mainCircle.DOScale (1, 0f);
        leftHandMan.DOScaleX (1, 0.5f);
        leftHandDescShortTC.DOScale (1, 0.5f);
        leftHandDescLongTC.DOScale (1, 0.5f);
        tutorialLeft.DOScale (1, 0.5f);
        tutorialRight.DOScale (1, 0.5f);
        skipButton.DOScale (1, 0.5f);
        tcButtonSmall.DOScale (1, 0.5f);
        enButtonSmall.DOScale (1, 0.5f);
    }

    public void HideLeftHandPage () {
        leftHandMan.DOScaleX (0, 0.5f);
        leftHandDescShortTC.DOScale (0, 0.5f);
        leftHandDescLongTC.DOScale (0, 0.5f);
    }

    public void ShowRightHandPage () {
        PlayWaveEffect ();
        WholePageColorEffect ();
        mainCircleFillbar.GetComponent<Image> ().DOFillAmount (0.5f, 0.5f);
        rightHandMan.DOScaleX (1, 0.5f);
        rightHandDescShortTC.DOScale (1, 0.5f);
        rightHandDescLongTC.DOScale (1, 0.5f);
        tutorialLeft.DOScale (1, 0.5f);
        tutorialRight.DOScale (1, 0.5f);
        skipButton.DOScale (1, 0.5f);
        tcButtonSmall.DOScale (1, 0.5f);
        enButtonSmall.DOScale (1, 0.5f);
    }

    public void HideRightHandPage () {
        rightHandMan.DOScaleX (0, 0.5f);
        rightHandDescShortTC.DOScale (0, 0.5f);
        rightHandDescLongTC.DOScale (0, 0.5f);
    }

    public void ShowConfirmPage () {
        PlayWaveEffect ();
        WholePageColorEffect ();
        mainCircleFillbar.GetComponent<Image> ().DOFillAmount (0.75f, 0.5f);
        confirmButton.DOScale (1, 0.5f);
        confirmDescShortTC.DOScale (1, 0.5f);
        confirmDescLongTC.DOScale (1, 0.5f);
    }

    public void HideConfirmPage () {
        confirmButton.DOKill ();
        confirmButton.DOScale (0, 0);
        confirmDescShortTC.DOScale (0, 0.5f);
        confirmDescLongTC.DOScale (0, 0.5f);
    }

    public void ShowReadyPage () {
        PlayWaveEffect ();
        WholePageColorEffect ();
        mainCircleFillbar.GetComponent<Image> ().DOFillAmount (1, 0.5f);
        mainCircle.DOScale (0, 0f);
        readyButton.DOScale (1, 0f);
        readyTextTC.DOScale (1, 0.5f);
        readyDescShortTC.DOScale (1, 0.5f);
        readyDescLongTC.DOScale (1, 0.5f);
        skipButton.DOScale (0, 0.5f);
        replayButton.DOScale (1, 0.5f);
    }

    public void HideReadyPage () {
        tutorialLeft.DOScale (0, 0.5f);
        tutorialRight.DOScale (0, 0.5f);
        readyButton.DOScale (0, 0f);
        readyTextTC.DOScale (0, 0.5f);
        readyDescShortTC.DOScale (0, 0.5f);
        readyDescLongTC.DOScale (0, 0.5f);
        replayButton.DOScale (0, 0.5f);
        skipButton.DOScale (0, 0.5f);
        tcButtonSmall.DOScale (0, 0.5f);
        enButtonSmall.DOScale (0, 0.5f);
    }

    public void ActivePageBlock () {
        StopCoroutine (unactivePageBlockCoroutine);
        pageBlock.GetComponent<MaskableGraphic> ().raycastTarget = true;
        pageBlock.GetComponent<MaskableGraphic> ().DOFade (1, 0.5f);
    }

    public void UnactivePageBlock () {
        pageBlock.GetComponent<MaskableGraphic> ().raycastTarget = false;
        pageBlock.GetComponent<MaskableGraphic> ().DOFade (0, 0.5f);
    }

    public void UnactivePageBlock (float delay) {
        unactivePageBlockCoroutine = Flow ();
        StartCoroutine (unactivePageBlockCoroutine);
        IEnumerator Flow () {
            yield return new WaitForSeconds (delay);
            UnactivePageBlock ();
        }
    }

    public void WholePageColorEffect () {
        float effectTime = 0.3f;

        colorCover.DOKill ();
        mainCircleFillbar.DOKill ();

        colorCover.GetComponent<MaskableGraphic> ().DOColor (pageCoverEffectColor, effectTime);
        colorCover.GetComponent<MaskableGraphic> ().DOColor (pageCoverIdleColor, effectTime).SetDelay (effectTime);
        mainCircleFillbar.GetComponent<MaskableGraphic> ().DOColor (mainCircleFillbarEffectColor, effectTime);
        mainCircleFillbar.GetComponent<MaskableGraphic> ().DOColor (mainCircleFillbarIdleColor, effectTime).SetDelay (effectTime);
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
        Game303Manager.instance.ChangeStatus (Status.SelectYear);
    }

    public void OnConfirmButtonClick () {
        Game303Manager.instance.ChangeStatus (Status.Ready);
    }

    public void OnReadyButtonClick () {
        if (Game303Manager.instance.CheckStatus (Status.Ready)) {
            Game303Manager.instance.ChangeStatus (Status.SelectYear);
        }
    }

    public void OnReplayButtonClick () {
        Game303Manager.instance.ChangeStatus (Status.Tutorial);
    }

}

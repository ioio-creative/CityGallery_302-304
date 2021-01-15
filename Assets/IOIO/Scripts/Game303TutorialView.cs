using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Game303TutorialView : MonoBehaviour {

    public static Game303TutorialView instance;

    [Header ("Setting")]
    [SerializeField] private float waveCooldownTime;
    [SerializeField] private Color pageCoverNoPlayerColor;
    [SerializeField] private Color pageCoverIdleColor;
    [SerializeField] private Color pageCoverEffectColor;
    [SerializeField] private Color mainCircleFillbarIdleColor;
    [SerializeField] private Color mainCircleFillbarEffectColor;
    [SerializeField] private Sprite skipButtonTC;
    [SerializeField] private Sprite skipButtonEN;
    [SerializeField] private Sprite replayButtonTC;
    [SerializeField] private Sprite replayButtonEN;

    [Header ("Global")]
    [SerializeField] private Animator waveAnimator;
    [SerializeField] private Transform colorCover;
    [SerializeField] private Transform leftLine;
    [SerializeField] private Transform rightLine;
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

    [Header ("Idle Page")]
    [SerializeField] private Transform idlePageText;

    [Header ("Select Language")]
    [SerializeField] private Transform tcButtonBig;
    [SerializeField] private Transform enButtonBig;
    [SerializeField] private Transform selectLanguageDescTC;
    [SerializeField] private Transform selectLanguageDescEN;

    [Header ("Left Hand")]
    [SerializeField] private Transform leftHandMan;
    [SerializeField] private Transform leftHandDescShort;
    [SerializeField] private Transform leftHandDescLong;

    [Header ("Right Hand")]
    [SerializeField] private Transform rightHandMan;
    [SerializeField] private Transform rightHandDescShort;
    [SerializeField] private Transform rightHandDescLong;

    [Header ("Confirm")]
    [SerializeField] private Transform confirmButton;
    [SerializeField] private Transform confirmDescShort;
    [SerializeField] private Transform confirmDescLong;

    [Header ("Ready")]
    [SerializeField] private Transform readyButton;
    [SerializeField] private Transform readyText;
    [SerializeField] private Transform readyDescShort;
    [SerializeField] private Transform readyDescLong;
   
    [Header ("Debug")]
    [SerializeField] private float waveCooldownTimer;
    private IEnumerator unactivePageBlockCoroutine;

    void Awake () {
        instance = this;
    }

    void Start () {

    }

    void Update () {
        waveCooldownTimer += Time.deltaTime;
    }

    public void PlayWaveEffect () {
        if (waveCooldownTimer >= waveCooldownTime) {
            waveCooldownTimer = 0;
            waveAnimator.SetTrigger ("Play");
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

    public void SetColorCoverAlpha (float alpha) {
        colorCover.DOKill ();
        colorCover.GetComponent<MaskableGraphic> ().DOFade (alpha, 0);
    }

    public void ShowIdlePage () {
        colorCover.DOKill ();
        colorCover.GetComponent<MaskableGraphic> ().DOColor (pageCoverNoPlayerColor, 0.5f);
        idlePageText.GetComponent<MaskableGraphic> ().DOFade (1, 0.5f);
        mainCircle.DOScale (0, 0f);
        mainCircleFillbar.DOScale (0, 0f);
        leftLine.DOScaleX (1.5f, 0.5f);
        rightLine.DOScaleX (1.5f, 0.5f);
    }

    public void HideIdlePage () {
        colorCover.DOKill ();
        colorCover.GetComponent<MaskableGraphic> ().DOColor (pageCoverNoPlayerColor, 0.5f);
        idlePageText.GetComponent<MaskableGraphic> ().DOFade (0, 0.5f);
    }

    public void ShowSelectLanguagePage () {
        colorCover.DOKill ();
        colorCover.GetComponent<MaskableGraphic> ().DOColor (pageCoverIdleColor, 0.5f);
        mainCircle.DOScale (1, 0.5f);
        mainCircleFillbar.DOScale (0, 0.5f);
        mainCircleFillbar.GetComponent<Image> ().DOFillAmount (0, 0.5f);
        tcButtonBig.DOScale (1, 0.5f);
        enButtonBig.DOScale (1, 0.5f);
        selectLanguageDescTC.DOScale (1, 0.5f);
        selectLanguageDescEN.DOScale (1, 0.5f);
        welcomeLeft.DOScale (1, 0.5f);
        welcomeRight.DOScale (1, 0.5f);
        leftLine.DOScaleX (1, 0).SetDelay (0.5f);
        rightLine.DOScaleX (1, 0).SetDelay (0.5f);
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
        leftHandDescShort.DOScale (1, 0.5f);
        leftHandDescLong.DOScale (1, 0.5f);
        tutorialLeft.DOScale (1, 0.5f);
        tutorialRight.DOScale (1, 0.5f);
        skipButton.DOScale (1, 0.5f);
        tcButtonSmall.DOScale (1, 0.5f);
        enButtonSmall.DOScale (1, 0.5f);
    }

    public void HideLeftHandPage () {
        leftHandMan.DOScaleX (0, 0.5f);
        leftHandDescShort.DOScale (0, 0.5f);
        leftHandDescLong.DOScale (0, 0.5f);
    }

    public void ShowRightHandPage () {
        PlayWaveEffect ();
        WholePageColorEffect ();
        mainCircleFillbar.GetComponent<Image> ().DOFillAmount (0.5f, 0.5f);
        rightHandMan.DOScaleX (1, 0.5f);
        rightHandDescShort.DOScale (1, 0.5f);
        rightHandDescLong.DOScale (1, 0.5f);
        tutorialLeft.DOScale (1, 0.5f);
        tutorialRight.DOScale (1, 0.5f);
        skipButton.DOScale (1, 0.5f);
        tcButtonSmall.DOScale (1, 0.5f);
        enButtonSmall.DOScale (1, 0.5f);
    }

    public void HideRightHandPage () {
        rightHandMan.DOScaleX (0, 0.5f);
        rightHandDescShort.DOScale (0, 0.5f);
        rightHandDescLong.DOScale (0, 0.5f);
    }

    public void ShowConfirmPage () {
        PlayWaveEffect ();
        WholePageColorEffect ();
        mainCircleFillbar.GetComponent<Image> ().DOFillAmount (0.75f, 0.5f);
        confirmButton.DOScale (1, 0.5f);
        confirmDescShort.DOScale (1, 0.5f);
        confirmDescLong.DOScale (1, 0.5f);
    }

    public void HideConfirmPage () {
        confirmButton.DOKill ();
        confirmButton.DOScale (0, 0);
        confirmDescShort.DOScale (0, 0.5f);
        confirmDescLong.DOScale (0, 0.5f);
    }

    public void ShowReadyPage () {
        PlayWaveEffect ();
        WholePageColorEffect ();
        mainCircleFillbar.GetComponent<Image> ().DOFillAmount (1, 0.5f);
        mainCircle.DOScale (0, 0f);
        readyButton.DOScale (1, 0f);
        readyText.DOScale (1, 0.5f);
        readyDescShort.DOScale (1, 0.5f);
        readyDescLong.DOScale (1, 0.5f);
        skipButton.DOScale (0, 0.5f);
        replayButton.DOScale (1, 0.5f);
    }

    public void HideReadyPage () {
        tutorialLeft.DOScale (0, 0.5f);
        tutorialRight.DOScale (0, 0.5f);
        readyButton.DOScale (0, 0f);
        readyText.DOScale (0, 0.5f);
        readyDescShort.DOScale (0, 0.5f);
        readyDescLong.DOScale (0, 0.5f);
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

    public void SwitchLanguage (Language language) {
        if (language == Language.TC) {
            tcButtonSmall.Find ("Fill").GetComponent<MaskableGraphic> ().DOFade (1, 0);
            enButtonSmall.Find ("Fill").GetComponent<MaskableGraphic> ().DOFade (0, 0);
            tcButtonSmall.Find ("Text").GetComponent<MaskableGraphic> ().color = Color.black;
            enButtonSmall.Find ("Text").GetComponent<MaskableGraphic> ().color = Color.white;

            skipButton.GetComponent<Image> ().sprite = skipButtonTC;
            replayButton.GetComponent<Image> ().sprite = replayButtonTC;

            leftHandDescShort.Find ("TC").DOScale (1, 0);
            leftHandDescLong.Find ("TC").DOScale (1, 0);
            rightHandDescShort.Find ("TC").DOScale (1, 0);
            rightHandDescLong.Find ("TC").DOScale (1, 0);
            confirmButton.Find ("TC").DOScale (1, 0);
            confirmDescShort.Find ("TC").DOScale (1, 0);
            confirmDescLong.Find ("TC").DOScale (1, 0);
            readyDescShort.Find ("TC").DOScale (1, 0);
            readyDescLong.Find ("TC").DOScale (1, 0);
            readyText.Find ("TC").DOScale (1, 0);

            leftHandDescShort.Find ("EN").DOScale (0, 0);
            leftHandDescLong.Find ("EN").DOScale (0, 0);
            rightHandDescShort.Find ("EN").DOScale (0, 0);
            rightHandDescLong.Find ("EN").DOScale (0, 0);
            confirmButton.Find ("EN").DOScale (0, 0);
            confirmDescShort.Find ("EN").DOScale (0, 0);
            confirmDescLong.Find ("EN").DOScale (0, 0);
            readyDescShort.Find ("EN").DOScale (0, 0);
            readyDescLong.Find ("EN").DOScale (0, 0);
            readyText.Find ("EN").DOScale (0, 0);
        }
        else if (language == Language.EN) {
            tcButtonSmall.Find ("Fill").GetComponent<MaskableGraphic> ().DOFade (0, 0);
            enButtonSmall.Find ("Fill").GetComponent<MaskableGraphic> ().DOFade (1, 0);
            tcButtonSmall.Find ("Text").GetComponent<MaskableGraphic> ().color = Color.white;
            enButtonSmall.Find ("Text").GetComponent<MaskableGraphic> ().color = Color.black;

            skipButton.GetComponent<Image> ().sprite = skipButtonEN;
            replayButton.GetComponent<Image> ().sprite = replayButtonEN;

            leftHandDescShort.Find ("TC").DOScale (0, 0);
            leftHandDescLong.Find ("TC").DOScale (0, 0);
            rightHandDescShort.Find ("TC").DOScale (0, 0);
            rightHandDescLong.Find ("TC").DOScale (0, 0);
            confirmButton.Find ("TC").DOScale (0, 0);
            confirmDescShort.Find ("TC").DOScale (0, 0);
            confirmDescLong.Find ("TC").DOScale (0, 0);
            readyDescShort.Find ("TC").DOScale (0, 0);
            readyDescLong.Find ("TC").DOScale (0, 0);
            readyText.Find ("TC").DOScale (0, 0);

            leftHandDescShort.Find ("EN").DOScale (1, 0);
            leftHandDescLong.Find ("EN").DOScale (1, 0);
            rightHandDescShort.Find ("EN").DOScale (1, 0);
            rightHandDescLong.Find ("EN").DOScale (1, 0);
            confirmButton.Find ("EN").DOScale (1, 0);
            confirmDescShort.Find ("EN").DOScale (1, 0);
            confirmDescLong.Find ("EN").DOScale (1, 0);
            readyDescShort.Find ("EN").DOScale (1, 0);
            readyDescLong.Find ("EN").DOScale (1, 0);
            readyText.Find ("EN").DOScale (1, 0);
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

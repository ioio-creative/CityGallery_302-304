﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Game303FlipdotView : MonoBehaviour {

	public static Game303FlipdotView instance;

    [Header ("Global")]
    [SerializeField] private Transform line;
    [SerializeField] private Transform mountain;
    [SerializeField] private Transform sand;

    [Header ("Tutorial")]
    [SerializeField] private Transform leftCircle;
    [SerializeField] private Transform rightCircle;
    [SerializeField] private Transform leftCircleSelect;
    [SerializeField] private Transform rightCircleSelect;
    //[SerializeField] private Transform tutorialSelector;
    [SerializeField] private Transform tutorialMidLine;

    [Header ("Select")]
    [SerializeField] private List<Transform> yearTexts = new List<Transform> ();
    [SerializeField] private List<Transform> yearTextBigs = new List<Transform> ();
    [SerializeField] private List<Transform> yearSelectors = new List<Transform> ();

    [Header ("Debug")]
    [SerializeField] private Vector2 tutorialSelectorStartPosition;
    [SerializeField] private Vector2 sandStartLocalPosition;

    void Awake () {
        instance = this;
    }

    void Start () {
        //tutorialSelectorStartPosition = tutorialSelector.position;
        sandStartLocalPosition = sand.localPosition;
    }

    public void ShowLine () {
        line.DOScaleX (1, 0.5f);
    }

    public void ShowLine (float delay) {
        line.DOScaleX (1, 0.5f).SetDelay (delay);
    }

    public void HideLine () {
        line.DOScaleX (0, 0.5f);
    }

    public void HideLine (float delay) {
        line.DOScaleX (0, 0.5f).SetDelay (delay);
    }

    public void ShowMountain (float delay) {
        mountain.DOScale (1, 0).SetDelay (delay);
    }

    public void HideMountain (float delay) {
        mountain.DOScale (0, 0).SetDelay (delay);
    }

    public int YearTextCount () {
        return yearTexts.Count;
    }

    public void SelectLeftCircle () {
        leftCircle.DOScale (1f, 0.2f);      
        rightCircle.DOScale (0.5f, 0.2f);
        leftCircleSelect.DOScale (1, 0).SetDelay (0.2f);
        rightCircleSelect.DOScale (0, 0);
        //tutorialSelector.DOScale (1, 0.5f);
        //tutorialSelector.DOMoveX (tutorialSelectorStartPosition.x, 0.2f);
    }

    public void SelectRightCircle () {
        leftCircle.DOScale (0.5f, 0.2f);
        rightCircle.DOScale (1f, 0.2f);
        leftCircleSelect.DOScale (0, 0);
        rightCircleSelect.DOScale (1, 0).SetDelay (0.2f);
        //tutorialSelector.DOScale (1, 0.5f);
        //tutorialSelector.DOMoveX (-tutorialSelectorStartPosition.x, 0.2f);
    }

    public void ShowTutorialPage () {
        leftCircle.DOScale (0.5f, 0.2f);
        rightCircle.DOScale (0.5f, 0.2f);

        tutorialMidLine.DOScaleY(1, 0.2f);
    }

    public void HideTutorialPage () {
        leftCircle.DOScale (0, 0.5f);
        rightCircle.DOScale (0, 0.5f);
        leftCircleSelect.DOScale (0, 0);
        rightCircleSelect.DOScale (0, 0);
        //tutorialSelector.DOScale (0, 0.5f);

        tutorialMidLine.DOScaleY(0, 0.2f);
    }

    public void SelectYear (int index) {
        for (int cnt = 0; cnt < YearTextCount(); cnt++) {
            if (cnt != index) {
                yearTexts[cnt].DOScale (1, 0);
                yearTextBigs[cnt].DOScale (0, 0);
                yearSelectors[cnt].DOScale (0, 0);
            }
        }
        yearTexts[index].DOScale (0, 0);
        yearTextBigs[index].DOScale (1, 0);
        yearSelectors[index].DOScale (1, 0);
    }

    public void SelectYear (int index, float delay) {
        StartCoroutine (Flow ());
        IEnumerator Flow () {
            yield return new WaitForSeconds (delay);
            SelectYear (index);
        }
    }

    public void HideSelectYearPage () {
        float clearTime = Game303Manager.instance.CheckStatus(Status.Confirm) ? Game303Manager.instance.sandEffectClearBackObjectTime : Game303Manager.instance.transitionClearBackObjectTime;

        for (int cnt = 0; cnt < YearTextCount (); cnt++) {
            yearTexts[cnt].DOScale (0, 0).SetDelay (clearTime);
            yearTextBigs[cnt].DOScale (0, 0).SetDelay (clearTime);
            yearSelectors[cnt].DOScale (0, 0).SetDelay (clearTime);
        }
    }

    public void PassSand (bool generic = true) {
        //sand.DOMoveX (-sandStartPosition.x, Game303ConfigData.instance.sandEffectMoveTime).SetEase (Ease.Linear);
        //sand.DOMoveX (sandStartPosition.x, 0).SetDelay (Game303ConfigData.instance.sandEffectMoveTime);

        var startPositionY = generic ? -sandStartLocalPosition.y : sandStartLocalPosition.y;

        var effectMoveTime = generic ? Game303ConfigData.instance.genericTransitionMoveTime : Game303ConfigData.instance.sandEffectMoveTime; 

        sand.DOLocalMoveY(startPositionY, 0);
        sand.DOLocalMoveY(-startPositionY, effectMoveTime).SetEase(Ease.Linear);
    }

}
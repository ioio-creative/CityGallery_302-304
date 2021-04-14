using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Game302View : MonoBehaviour {

	public static Game302View instance;

    [Header ("Global")]
    [SerializeField] private Transform viewParent;
    [SerializeField] private Transform line;

    [Header ("Tutorial")]
    [SerializeField] private Transform leftCircle;
    [SerializeField] private Transform rightCircle;
    [SerializeField] private Transform leftCircleSelect;
    [SerializeField] private Transform rightCircleSelect;
    //[SerializeField] private Transform triangle;
    [SerializeField] private Transform tutorialMidLine;

    [Header ("Select")]
    [SerializeField] Transform yearContainer;
    [SerializeField] private List<Transform> yearTextSmalls = new List<Transform> ();
    [SerializeField] private List<Transform> yearTextBigs = new List<Transform> ();

    [Header ("Debug")]
    [SerializeField] private Vector2 viewScale;
    [SerializeField] private Vector2 triangleStartPosition;

    void Awake () {
        instance = this;
    }

    void Start () {
        viewScale = viewParent.localScale;
        //triangleStartPosition = triangle.position;
    }

    public void ShowLine()
    {
        line.DOScaleX(1, 0.5f);
    }

    public void ShowLine(float delay)
    {
        line.DOScaleX(1, 0.5f).SetDelay(delay);
    }

    public void HideLine()
    {
        line.DOScaleX(0, 0.5f);
    }

    public void HideLine(float delay)
    {
        line.DOScaleX(0, 0.5f).SetDelay(delay);
    }

    public int YearTextCount () {
        return yearTextSmalls.Count;
    }

    public void SelectLeftCircle () {
        leftCircle.DOScale (1f, 0.2f);
        rightCircle.DOScale (0.5f, 0.2f);
        leftCircleSelect.DOScale (1, 0).SetDelay (0.2f);
        rightCircleSelect.DOScale (0, 0);
        //triangle.DOMoveX (triangleStartPosition.x, 0.2f);
        //triangle.DOScale (1, 0);
    }

    public void SelectRightCircle () {
        leftCircle.DOScale (0.5f, 0.2f);
        rightCircle.DOScale (1f, 0.2f);
        leftCircleSelect.DOScale (0, 0);
        rightCircleSelect.DOScale (1, 0).SetDelay (0.2f);
        //triangle.DOMoveX (-triangleStartPosition.x, 0.2f);
        //triangle.DOScale (1, 0);
    }

    public void ShowTutorialPage () {
        leftCircle.DOScale (0.5f, 0.2f);
        rightCircle.DOScale (0.5f, 0.2f);
        leftCircleSelect.DOScale (0, 0);
        rightCircleSelect.DOScale (0, 0);
        //triangle.DOScale (0, 0.5f);
        
        tutorialMidLine.DOScaleY(1, 0.2f);
    }

    public void HideTutorialPage () {
        leftCircle.DOScale (0, 0.5f);
        rightCircle.DOScale (0, 0.5f);
        leftCircleSelect.DOScale (0, 0);
        rightCircleSelect.DOScale (0, 0);
        //triangle.DOScale (0, 0.5f);

        tutorialMidLine.DOScaleY(0, 0.2f);
    }

    public void ShowSelectPage () {
        yearContainer.DOMoveX (0, 0);
        yearContainer.DOScale (1, 0).SetDelay (0.5f);
        for (int cnt = 0; cnt < YearTextCount (); cnt++) {
            yearTextSmalls[cnt].DOScale (1, 0);
            yearTextBigs[cnt].DOScale (0, 0);    
        }
    }

    public void SelectYear (int index) {
        yearContainer.DOMoveX (index * -112 * viewScale.x, 0.5f);
    }

    public void ShowConfirmPage () {
        for (int cnt = 0; cnt < YearTextCount (); cnt++) {
            yearTextSmalls[cnt].DOScale (0, 0);
            yearTextBigs[cnt].DOScale (1, 0);
        }
    }

    public void HideConfirmPage () {
        for (int cnt = 0; cnt < YearTextCount (); cnt++) {
            yearContainer.DOKill ();
            yearContainer.DOScale (0, 0.5f);
        }
    }

}

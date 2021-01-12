using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TouchCircleView : MonoBehaviour {

    [Header ("Setup")]
    [SerializeField] private List<Transform> rings = new List<Transform> ();

    [Header ("Debug")]
    [SerializeField] private MaskableGraphic circleImage;
    [SerializeField] private bool active;

    void Start () {
        circleImage = GetComponent<MaskableGraphic> ();
        StartTweening ();
    }

    void Update () {
        
    }

    public void StartTweening () {
        StartCoroutine (Run ());
        IEnumerator Run () {
            float effectTime = 1.8f;
            while (true) {
                rings[0].DOScale (0.5f, 0);
                rings[0].DOScale (1, effectTime).SetEase (Ease.Linear);
                circleImage.DOFade (0.36f, effectTime / 4);
                circleImage.DOFade (0.5f, effectTime / 4).SetDelay (effectTime / 4);
                yield return new WaitForSeconds (effectTime / 2);
                rings[1].DOScale (0.5f, 0);
                rings[1].DOScale (1, effectTime).SetEase (Ease.Linear);
                circleImage.DOFade (0.36f, effectTime / 2);
                circleImage.DOFade (0.5f, effectTime / 4).SetDelay (effectTime / 4);
                yield return new WaitForSeconds (effectTime / 2);
            }
        } 
    }

}

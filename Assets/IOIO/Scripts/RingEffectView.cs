using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RingEffectView : MonoBehaviour {

    [Header ("Setup")]
    [SerializeField] private List<Transform> rings = new List<Transform> ();

    [Header ("Setup")]
    [SerializeField] private float effectTime;
    [SerializeField] private Vector2 minScale;
    [SerializeField] private Vector2 maxScale;

    [Header ("Debug")]
    [SerializeField] private MaskableGraphic mainImage;

    void Start () {
        mainImage = GetComponent<MaskableGraphic> ();
        rings[0].DOScale (minScale, 0);
        rings[1].DOScale (minScale, 0);
        StartTweening ();
    }

    void Update () {
        
    }

    public void StartTweening () {
        StartCoroutine (Run ());
        IEnumerator Run () {
            while (true) {
                rings[0].DOScale (minScale, 0);
                rings[0].DOScale (maxScale, effectTime).SetEase (Ease.Linear);
                mainImage.DOFade (0.36f, effectTime / 4);
                mainImage.DOFade (0.5f, effectTime / 4).SetDelay (effectTime / 4);
                yield return new WaitForSeconds (effectTime / 2);
                rings[1].DOScale (minScale, 0);
                rings[1].DOScale (maxScale, effectTime).SetEase (Ease.Linear);
                mainImage.DOFade (0.36f, effectTime / 2);
                mainImage.DOFade (0.5f, effectTime / 4).SetDelay (effectTime / 4);
                yield return new WaitForSeconds (effectTime / 2);
            }
        } 
    }

}

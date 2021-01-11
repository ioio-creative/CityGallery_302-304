using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game303SequenceView : MonoBehaviour {

	public static Game303SequenceView instance;

    [Header ("Setup")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header ("Setting")]
    [SerializeField] private float framePreSecond;

    [Header ("Debug")]
    [SerializeField] public List<SpriteSequence> buildingSequences = new List<SpriteSequence> ();
    [SerializeField] private int currentIndex;
    [SerializeField] private float virtualFrame;
    [SerializeField] private int currentFrame;
    [SerializeField] public bool playing;

    void Awake () {
        instance = this;
    }

    void Start () {

    }

    void Update () {
        if (playing) {
            virtualFrame += Time.deltaTime * framePreSecond;
            currentFrame = Mathf.FloorToInt (virtualFrame);
            if (currentFrame < buildingSequences[currentIndex].sprites.Count) {
                spriteRenderer.sprite = buildingSequences[currentIndex].sprites[currentFrame];
            }
            else {
                playing = false;
            }
        }
    }

    public void Play (int index) {
        virtualFrame = 0;
        currentFrame = 0;
        currentIndex = index;
        spriteRenderer.sprite = buildingSequences[currentIndex].sprites[0];
        playing = true;
    }

    public void Play (int index, float delay) {
        StartCoroutine (Flow ());
        IEnumerator Flow () {
            yield return new WaitForSeconds (delay);
            Play (index);
        }
    }

    public void Clear () {
        StopAllCoroutines ();
        spriteRenderer.sprite = null;
        playing = false;
    }

    public void Clear (float delay) {
        StartCoroutine (Flow ());
        IEnumerator Flow () {
            yield return new WaitForSeconds (delay);
            Clear ();
        }
    }

}

[System.Serializable]
public class SpriteSequence {
    public List<Sprite> sprites = new List<Sprite> ();
}
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Game303ImageLoader : MonoBehaviour {

    public static Game303ImageLoader instance;

    public Texture2D mountain;
    public Texture2D building;
    public List<Texture2D> textures1900 = new List<Texture2D> ();
    public List<Texture2D> textures1945 = new List<Texture2D> ();
    public List<Texture2D> textures1985 = new List<Texture2D> ();
    public List<Texture2D> textures2019 = new List<Texture2D> ();

    private string folderPath1900;
    private string folderPath1945;
    private string folderPath1985;
    private string folderPath2019;

    void Awake () {
        instance = this;
    }

    [System.Obsolete]
    void Start () {
        LoadMountain ();
        LoadBuilding ();
        folderPath1900 = string.Concat (Application.dataPath, "/../External/1900/");
        folderPath1945 = string.Concat (Application.dataPath, "/../External/1945/");
        folderPath1985 = string.Concat (Application.dataPath, "/../External/1985/");
        folderPath2019 = string.Concat (Application.dataPath, "/../External/2019/");
        StartCoroutine (LoadTextures ());
    }

    void Update () {

    }

    private void LoadMountain () {
        StartCoroutine (Flow ());
        IEnumerator Flow () {
            string path = string.Concat (Application.dataPath, "/../External/Mountain.png");
            byte[] fileData = File.ReadAllBytes (path);
            Texture2D rawTexture = new Texture2D (2, 2);
            yield return new WaitForEndOfFrame ();
            rawTexture.LoadImage (fileData);
            yield return new WaitForEndOfFrame ();
            mountain = rawTexture;

            Sprite sprite = Sprite.Create (mountain,
            new Rect (0.0f, 0.0f, mountain.width, mountain.height),
            new Vector2 (0.5f, 0.5f), 1);
            GameObject.Find ("Mountain").GetComponent<SpriteRenderer> ().sprite = sprite;
        }
    }

    private void LoadBuilding()
    {
        StartCoroutine(Flow());
        IEnumerator Flow()
        {
            string path = string.Concat(Application.dataPath, "/../External/Building.png");
            byte[] fileData = File.ReadAllBytes(path);
            Texture2D rawTexture = new Texture2D(2, 2);
            rawTexture.filterMode = FilterMode.Point;
            yield return new WaitForEndOfFrame();
            rawTexture.LoadImage(fileData);
            yield return new WaitForEndOfFrame();
            building = rawTexture;

            Sprite sprite = Sprite.Create(building,
            new Rect(0.0f, 0.0f, building.width, building.height),
            new Vector2(0.5f, 0.5f), 1);

            //Also Add Last Image to Idle Building
            yield return new WaitUntil(() => IdleBuilding303.instance != null);
            IdleBuilding303.instance.SetBuildingSprite(sprite);
        }
    }

    [System.Obsolete]
    IEnumerator LoadTextures () {

        string[] files1900 = Directory.GetFiles (folderPath1900);
        string[] files1945 = Directory.GetFiles (folderPath1945);
        string[] files1985 = Directory.GetFiles (folderPath1985);
        string[] files2019 = Directory.GetFiles (folderPath2019);

        for (int cnt = 0; cnt < files1900.Length; cnt++) {
            using (WWW www = new WWW (files1900[cnt])) {
                yield return www;
                textures1900.Add (www.texture);
            }
        }

        for (int cnt = 0; cnt < files1945.Length; cnt++) {
            using (WWW www = new WWW (files1945[cnt])) {
                yield return www;
                //www.texture.filterMode = FilterMode.Point;
                textures1945.Add (www.texture);
            }
        }

        for (int cnt = 0; cnt < files1985.Length; cnt++) {
            using (WWW www = new WWW (files1985[cnt])) {
                yield return www;
                //www.texture.filterMode = FilterMode.Point;
                textures1985.Add (www.texture);
            }
        }

        for (int cnt = 0; cnt < files2019.Length; cnt++) {
            using (WWW www = new WWW (files2019[cnt])) {
                yield return www;
                //www.texture.filterMode = FilterMode.Point;
                textures2019.Add (www.texture);
            }
        }

    yield return new WaitForEndOfFrame ();
        StartCoroutine (CreateSprites ());
    }

    IEnumerator CreateSprites () {
        yield return new WaitForEndOfFrame ();
        for (int cnt = 0; cnt < 4; cnt++) {
            Game303SequenceView.instance.buildingSequences.Add (new SpriteSequence ());
        }
        for (int cnt = 0; cnt < textures1900.Count; cnt++) {
            var texture = textures1900[cnt];
            texture.filterMode = FilterMode.Point;

            Sprite sprite = Sprite.Create (textures1900[cnt],
                new Rect (0.0f, 0.0f, textures1900[cnt].width, textures1900[cnt].height),
                new Vector2 (0.5f, 0.5f), 1);
            Game303SequenceView.instance.buildingSequences[0].sprites.Add (sprite);
        }
        for (int cnt = 0; cnt < textures1945.Count; cnt++) {
            var texture = textures1945[cnt];
            texture.filterMode = FilterMode.Point;

            Sprite sprite = Sprite.Create (textures1945[cnt],
                new Rect (0.0f, 0.0f, textures1945[cnt].width, textures1945[cnt].height),
                new Vector2 (0.5f, 0.5f), 1);
            Game303SequenceView.instance.buildingSequences[1].sprites.Add (sprite);
        }
        for (int cnt = 0; cnt < textures1985.Count; cnt++) {
            var texture = textures1985[cnt];
            texture.filterMode = FilterMode.Point;

            Sprite sprite = Sprite.Create (textures1985[cnt],
                new Rect (0.0f, 0.0f, textures1985[cnt].width, textures1985[cnt].height),
                new Vector2 (0.5f, 0.5f), 1);
            Game303SequenceView.instance.buildingSequences[2].sprites.Add (sprite);
        }
        for (int cnt = 0; cnt < textures2019.Count; cnt++) {
            var texture = textures2019[cnt];
            texture.filterMode = FilterMode.Point;

            Sprite sprite = Sprite.Create (textures2019[cnt],
                new Rect (0.0f, 0.0f, textures2019[cnt].width, textures2019[cnt].height),
                new Vector2 (0.5f, 0.5f), 1);
            Game303SequenceView.instance.buildingSequences[3].sprites.Add (sprite);

        }
        StartCoroutine (ClearTextures ());
    }

    IEnumerator ClearTextures () {
        yield return new WaitForEndOfFrame ();
        textures1900.Clear ();
        textures1945.Clear ();
        textures1985.Clear ();
        textures2019.Clear ();
        Resources.UnloadUnusedAssets ();
    }
}
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game303ConfigData : MonoBehaviour {

    public static Game303ConfigData instance;

    [Header ("Config")]
    public float sandEffectMoveTime;
    public float genericTransitionMoveTime;

    [Header ("Debug")]
    [SerializeField] private string[] configs;
    [SerializeField] private bool fileLoaded;
    [SerializeField] private bool dataLoaded;

    void Awake () {
        instance = this;
    }

    public void LoadConfig () {
        var dictionary = new Dictionary<string, string> ();

        try {
            configs = File.ReadAllLines ((string.Concat (Application.dataPath, "/../External/Config-303.csv")));
            for (int cnt = 0; cnt < configs.Length; cnt++) {
                if (configs[cnt].Contains (",")) {
                    string[] splitted = configs[cnt].Split (',');
                    dictionary.Add (splitted[0], splitted[1]);
                }
            }
            fileLoaded = true;
        }
        catch {
            Debug.LogError ("Fail To Load Config File!");
            fileLoaded = false;
        }

        if (fileLoaded) {
            try {
                sandEffectMoveTime = float.Parse (dictionary[nameof(sandEffectMoveTime)]);
                genericTransitionMoveTime = float.Parse(dictionary[nameof(genericTransitionMoveTime)]);
                dataLoaded = true;
            }
            catch {
                Debug.LogError ("Missing Data From Config File!");
                dataLoaded = false;
            }
        }
    }

}

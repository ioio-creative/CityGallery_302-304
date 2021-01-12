using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemConfig : MonoBehaviour
{
    //singleton
    public static SystemConfig Instance;
    
    [SerializeField]
    private string configJsonFile;


    public ConfigJson Config => config;
    [SerializeField]
    private ConfigJson config;


    private void Awake()
    {
        Instance = this;
        
        string configPath = UnityPaths.GetFullPathUnderStreamingAssets(configJsonFile);        
        config = ConfigJson.LoadJsonData(System.IO.File.ReadAllText(configPath));
    }
}

[Serializable]
public struct ConfigJson
{
    public bool debug;
    public string socketUrl;

    public static ConfigJson LoadJsonData(string jsonData) => JsonUtility.FromJson<ConfigJson>(jsonData);
}

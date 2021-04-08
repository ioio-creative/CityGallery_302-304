using SOVariables;
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

    [SerializeField]
    private Vector2 screenDimension;

    [SerializeField]
    private FloatVariable playerInRangeThresholdMax;
    [SerializeField]
    private FloatVariable playerEnterGameThreshold;
    [SerializeField]
    private FloatVariable playerLeaveGameThreshold;
    [SerializeField]
    private FloatVariable idleTimeout;


    private void Awake()
    {
        Instance = this;
        
        string configPath = UnityPaths.GetFullPathUnderStreamingAssets(configJsonFile);        
        config = ConfigJson.LoadJsonData(System.IO.File.ReadAllText(configPath));

        screenDimension = new Vector2(config.displayWidth, config.displayHeight);

        playerInRangeThresholdMax.InitializeValue(config.playerRangeThresholdMax);
        playerEnterGameThreshold.InitializeValue(config.playerRangeThresholdMin);
        
        playerLeaveGameThreshold.InitializeValue(config.idlePlayerRangeThreshold);
        idleTimeout.InitializeValue(config.idleTimeout);

        if (screenDimension.x > 0 && screenDimension.y > 0)
        {
            Display.main.SetParams((int)screenDimension.x, (int)screenDimension.y, 0, 0);
        }
    }
}

[Serializable]
public struct ConfigJson
{
    public bool debug;
    public string socketUrl;

    public int displayWidth;
    public int displayHeight;

    public float playerRangeThresholdMax;
    public float playerRangeThresholdMin;

    public float idlePlayerRangeThreshold;
    public float idleTimeout;

    public static ConfigJson LoadJsonData(string jsonData) => JsonUtility.FromJson<ConfigJson>(jsonData);
}

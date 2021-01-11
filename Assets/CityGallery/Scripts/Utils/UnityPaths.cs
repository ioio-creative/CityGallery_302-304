using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UnityPaths
{
    public static string GetFullPathUnderStreamingAssets(params string[] paths)
    {
        List<string> pathList = new List<string>(paths);
        pathList.Insert(0, Application.streamingAssetsPath);
        return Path.Combine(pathList.ToArray());
    }
}

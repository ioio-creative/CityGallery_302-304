using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{

    public static void GoToScene(MonoBehaviour mb, int sceneIdx)
    {
        Debug.Log("Load Scene called");
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (sceneIdx == SceneManager.GetSceneAt(i).buildIndex)
            {
                return;
            }
        }
        mb.StartCoroutine(WaitAndActiveLoadedScene(sceneIdx));       
    }

    private static IEnumerator WaitAndActiveLoadedScene(int sceneIdx)
    {
        var async = SceneManager.LoadSceneAsync(sceneIdx, LoadSceneMode.Additive);

        while (!async.isDone)
        {
            yield return null;
        }
        async.allowSceneActivation = true;
        yield break;
    }

    public static void UnloadScene(int sceneIdx)
    {
        Debug.Log("Unload Scene called");
        var async = SceneManager.UnloadSceneAsync(sceneIdx);
    }
}

/*************************************************
  * 名稱：SceneLoader
  * 作者：RyanHsu
  * 功能說明：轉場程式
  * ***********************************************/
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    static DispatchEvent dispatcher = DispatchEvent.GetInstance();

    public static event Action<float> OnProgressChanged;
    public static event Action OnSceneLoaded;

    static bool _canLoadScene = true;
    public static bool canLoadScene
    {
        get
        {
            return _canLoadScene;
        }
        set
        {
            _canLoadScene = value;
            //Time.timeScale = value ? 1f : 0.5f;
        }
    }

    static string activeSceneName;
    static Coroutine coroutine = null;
    static MonoManager mono = MonoManager.Instance;

    public static void StartSceneLoad(string targetSceneName)
    {
        if (!canLoadScene) return;
        canLoadScene = false;
        //
        if (coroutine != null)
        {
            mono.StopCoroutine(coroutine);
            coroutine = null;
            canLoadScene = true;
        }

        mono.StartCoroutine(LoadSceneCoroutine(targetSceneName));
    }

    private static IEnumerator LoadSceneCoroutine(string targetSceneName)
    {
        activeSceneName = SceneManager.GetActiveScene().name;

        // 加載加載場景
        yield return LoadSceneAsync("Loading", true);

        // 等待1秒
        yield return new WaitForSeconds(1f);

        // 卸載當前場景
        yield return UnloadSceneAsync(activeSceneName);

        // 加載目標場景
        yield return LoadSceneAsync(targetSceneName);

        dispatcher.OnLoadingFadeOut?.Invoke(1f);

        // 等待1秒
        yield return new WaitForSeconds(1f);

        // 卸載場景
        yield return UnloadSceneAsync("Loading");

        // 發送場景加載完成事件
        OnSceneLoaded?.Invoke();
        canLoadScene = true;
    }

    private static IEnumerator LoadSceneAsync(string sceneName, bool isLoadingScene = false)
    {
        if (SceneManager.GetActiveScene().name == sceneName)
        {
            yield break;
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            if (!isLoadingScene)
            {
                OnProgressChanged?.Invoke(progress);
                //Debug.Log(sceneName + "--asyncLoad.progress=" + progress);
            }

            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    private static IEnumerator UnloadSceneAsync(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName)) yield break;

        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(sceneName);

        while (!asyncUnload.isDone)
        {
            yield return null;
        }
    }
}
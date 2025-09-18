/*************************************************
  * 名稱：MonoManager
  * 作者：RyanHsu
  * 功能說明：
  * ***********************************************/
using UnityEngine;
using System.Collections;
using System;

/// <summary>讓純C#也能使用MonoBehaviour內的涵式，無需與Inspector交互</summary>
public class MonoManager : MonoBehaviour
{
    private static MonoManager instance;

    public static MonoManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("MonoManager");
                instance = go.AddComponent<MonoManager>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }
    
    private MonoManager() { }

    /// <summary>等待Call</summary>
    /// <param name="delay">單位(秒)</param>
    public static void DelayCall(float delay, Action method)
    {
        // 確保 Instance 是有效的 MonoBehaviour 實例
        if (Instance != null)
        {
            Instance.StartCoroutine(IDelayCall(delay, method));
        } else
        {
            Debug.LogWarning("Instance is null. Cannot start coroutine.");
        }
    }


    /// <summary>
    /// WhildLoop條件等待式，可自訂循環的時脈
    /// </summary>
    /// <param name="waitTime">每幾秒檢查一次條件</param>
    /// <param name="func">while條件</param>
    /// <param name="method">while條件滿足後執行Method</param>
    /// <returns></returns>
    public static void WhildLoop(float waitTime, Func<bool> func, Action method)
    {
        Instance.StartCoroutine(IWhildLoop(waitTime, func, method));
    }

    private void OnDestroy()
    {
        instance = null;
    }

    public static IEnumerator IDelayCall(float delay, Action method)
    {
        yield return new WaitForSeconds(delay);

        try
        {
            method.Invoke();
        } catch (Exception ex)
        {
            Debug.LogError($"Error invoking delayed action: {ex.Message}");
        }
    }

    public static IEnumerator IWhildLoop(float waitTime, Func<bool> func, Action method)
    {
        while (!func.Invoke())
        {
            yield return new WaitForSeconds(waitTime);
        }
        method.Invoke();
    }

}


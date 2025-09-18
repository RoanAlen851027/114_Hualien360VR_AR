/*************************************************
  * 名稱：LocalStorageManager
  * 作者：RyanHsu
  * 功能說明：操作WebGL中的LocalStorage與Unity連結
  * ***********************************************/
using UnityEngine;
using System.Runtime.InteropServices;
using System;

public class LocalStorageManager
{
    [DllImport("__Internal")] private static extern void SetLocalStorageItem(string key, string value);
    [DllImport("__Internal")] private static extern IntPtr GetLocalStorageItem(string key);
    [DllImport("__Internal")] private static extern void RemoveLocalStorageItem(string key);

    static Bindable<string> bind;

    public static void SetItem(string key, string value)
    {
        bind = Bindable<string>.GetData(key);

        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            SetLocalStorageItem(key, value);
        } else
        {
            Debug.LogWarning("LocalStorage is only available in WebGL build.");
        }
        bind.Set(value);
    }

    public static string GetItem(string key)
    {
        bind = Bindable<string>.GetData(key);

        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            IntPtr valuePtr = GetLocalStorageItem(key);
            string value = Marshal.PtrToStringUTF8(valuePtr);
            bind.Set(value);
        } else
        {
            Debug.LogWarning("LocalStorage is only available in WebGL build.");
            bind.Invoke();
        }

        return bind.Get();
    }

    public static void RemoveItem(string key)
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            RemoveLocalStorageItem(key);
        } else
        {
            Debug.LogWarning("LocalStorage is only available in WebGL build.");
        }
    }

}

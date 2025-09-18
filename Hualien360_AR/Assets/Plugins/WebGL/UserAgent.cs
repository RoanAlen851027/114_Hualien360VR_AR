using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

public class UserAgent
{
#if !UNITY_EDITOR && UNITY_WEBGL
    [DllImport("__Internal")]
    private static extern string GetUserAgent();
#endif
    public static bool IsMobile()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        switch (Application.platform)
        {
            case RuntimePlatform.WebGLPlayer:
                {
                    string userAgent = GetUserAgent();
                    string[] mobileKeywords = new string[] { "Android", "iPhone", "iPad", "Windows Phone", "Mobile"};
                    for (int i = 0; i < mobileKeywords.Length; i++)
                    {
                        if (userAgent.IndexOf(mobileKeywords[i]) != -1)
                        {
                            return true;
                        }
                    }
                    return false;
                }
            case RuntimePlatform.LinuxEditor:
            case RuntimePlatform.OSXEditor:
            case RuntimePlatform.WindowsEditor:
                {
                    return true;
                }
            default:
                return false;
        }
#else
        return true;
#endif
    }
}

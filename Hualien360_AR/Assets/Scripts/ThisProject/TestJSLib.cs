/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using System.Runtime.InteropServices;
using UnityEngine;

public class TestJSLib : MonoBehaviour
{
    // 宣告 extern 方法，對應 JSLib
#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void ShowAlert(string message);
#endif

    public void OnButtonClick()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        ShowAlert("Hello from Unity WebGL!");
#else
        Debug.Log("WebGL Alert only works in build!");
#endif
    }
}

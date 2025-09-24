/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using System.Runtime.InteropServices;
using UnityEngine;

public class MindARCameraController : MonoBehaviour
{
#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void StartMindARCamera();

    [DllImport("__Internal")]
    private static extern void PauseMindARCamera();
#endif

    public void ResumeCamera()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        StartMindARCamera();
#else
        Debug.Log("ResumeCamera called (non-WebGL)");
#endif
    }

    public void PauseCamera()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        PauseMindARCamera();
#else
        Debug.Log("PauseCamera called (non-WebGL)");
#endif
    }

    // 自動暫停 / 恢復
    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus) ResumeCamera();
        else PauseCamera();
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus) PauseCamera();
        else ResumeCamera();
    }
}


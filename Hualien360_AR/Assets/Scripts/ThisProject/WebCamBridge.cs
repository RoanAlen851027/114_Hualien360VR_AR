/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using System.Runtime.InteropServices;
using UnityEngine;

public class WebCamBridge : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void StartCamera();

    [DllImport("__Internal")]
    private static extern void PauseCamera();

    [DllImport("__Internal")]
    private static extern void ResumeCamera();

    [DllImport("__Internal")]
    private static extern void StopCamera();


    void Awake()
    {
#if UNITY_WEBGL
        Application.runInBackground = true; // 即使切換視窗，Unity 仍然更新
#endif
    }

    public void StartCamera_BTN() { StartCamera(); }
    public void PauseCamera_BTN() { PauseCamera(); }
    public void ResumeCamera_BTN() { ResumeCamera(); }
    public void StopCamera_BTN()
    {
        StopCamera();
    }
}

/*************************************************
  * 名稱：GAEventTracker
  * 作者：RyanHsu
  * 功能說明：
  * ***********************************************/
using System;
using System.Runtime.InteropServices;
using UnityEngine;

public static class GAEventTracker
{
    [DllImport("__Internal")] private static extern void SendGAEvent(string eventName, string eventParams);

    public static void TrackEvent(string eventName, string eventParams)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        try
        {
            SendGAEvent(eventName, eventParams);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Failed to send GA event: {e.Message}");
        }
#else
        Debug.Log($"[Simulated GA Event] Event Name: {eventName}, Params: {eventParams}");
#endif
    }
}

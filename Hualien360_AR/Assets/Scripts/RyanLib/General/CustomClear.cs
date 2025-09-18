/*************************************************
  * 名稱：CustomClear
  * 作者：RyanHsu
  * 功能說明：觸發glClear之用(CustomClear.jslib)
  * ***********************************************/
using UnityEngine;
#if !UNITY_EDITOR && UNITY_WEBGL
using System.Runtime.InteropServices;
#endif

public class CustomClear : MonoBehaviour
{
#if !UNITY_EDITOR && UNITY_WEBGL
    [DllImport("__Internal")] private static extern void glClear(int mask);

    void Start()
    {
        glClear(0x00004000 | 0x00000100);
    }

#endif
}
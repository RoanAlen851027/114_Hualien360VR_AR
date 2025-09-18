/*************************************************
  * 名稱：CameraAspectController
  * 作者：RyanHsu
  * 功能說明：
  * ***********************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAspectController : MonoBehaviour
{
    public Vector2 vectorAspect;
    float targetAspect = 0;// 目標長寬比

    void Start()
    {
        targetAspect = vectorAspect.x / vectorAspect.y;
        Camera.main.aspect = targetAspect;
    }

    void Update()
    {

        if (Camera.main.aspect != targetAspect)
        {
            Camera.main.aspect = targetAspect;
        }
    }
}

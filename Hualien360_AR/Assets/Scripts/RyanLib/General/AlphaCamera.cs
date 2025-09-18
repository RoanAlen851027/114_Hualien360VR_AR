/*************************************************
  * 名稱：AlphaCamera
  * 作者：RyanHsu
  * 功能說明：應透明背景需求，自動調整Camera設定值
  * ***********************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

//禁止多載         [DisallowMultipleComponent]
//#if UNITY_EDITOR
//執行階段執行     [ExecuteAlways]
//#endif
[RequireComponent(typeof(Camera))]
public class AlphaCamera : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
        mainCamera.clearFlags = CameraClearFlags.Depth;
        mainCamera.backgroundColor = new Color(0f, 0f, 0f, 0f);
        if (TryGetComponent(out UniversalAdditionalCameraData universalAdditionalCameraData))
        {
            universalAdditionalCameraData.renderPostProcessing = false;
        }
    }

#if UNITY_EDITOR

    void OnValidate()
    {
        Awake();
    }

#endif

}
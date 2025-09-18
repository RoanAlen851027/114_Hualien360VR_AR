using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteAlways]
public class FaceToCamera : MonoBehaviour
{
    void OnEnable() => FaceTo();

    void Update() => FaceTo();
    
    void FaceTo()
    {
        if (!gameObject.activeInHierarchy) return;
        var camera = Camera.main;
        if (camera == default)
            return;

        Vector3 targetPosition = camera.transform.position;
        var lookPos = transform.position - targetPosition;
        transform.rotation = Quaternion.LookRotation(lookPos, camera.transform.up);
    }
}
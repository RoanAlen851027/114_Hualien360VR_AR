/*************************************************
  * 名稱：GizmosIcon
  * 作者：RyanHsu
  * 功能說明：
  * ***********************************************/
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[DisallowMultipleComponent]
public class GizmosWireSphere : MonoBehaviour
{
    public Vector3 offset;
    public float radius = 0.03f;
    public Color color = Color.white;
    public Color color2 = Color.white;

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        DrawGizmos();
    }
    void OnDrawGizmosSelected()
    {
        DrawGizmos();
    }
    void DrawGizmos()
    {
        SceneView sceneView = SceneView.lastActiveSceneView;
        Camera camera = sceneView.camera;

        Vector3 radiusPoint = Vector3.Lerp(transform.position, camera.transform.position, radius);
        float dis = Vector3.Distance(transform.position, radiusPoint);
        if (dis > radius) dis = radius;
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position + offset, dis);
        Gizmos.color = color2;
        Gizmos.DrawWireSphere(transform.position + offset, dis);
    }
#endif

}
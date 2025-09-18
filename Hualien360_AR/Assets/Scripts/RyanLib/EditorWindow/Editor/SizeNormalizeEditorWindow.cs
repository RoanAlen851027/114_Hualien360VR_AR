/*************************************************
  * 名稱：SizeNormalizeEditorWindow
  * 作者：RyanHsu
  * 功能說明：
  * ***********************************************/
using UnityEditor;
using UnityEngine;

public class SizeNormalizeEditorWindow : EditorWindow
{
    private float lastScale = 1f;

    [MenuItem("Tools/CustomTools/Size Normalize Windows")]
    public static void ShowWindow()
    {
        GetWindow<SizeNormalizeEditorWindow>("Size Normalize Windows");
    }

    private void OnGUI()
    {
        GUILayout.Label("Size Normalize", EditorStyles.boldLabel);

        GUILayout.Space(10f);

        if (GUILayout.Button("Selected Item Size Normalize"))
        {
            ApplySizeToSelectedGameObject();
        }
    }

    private void ApplySizeToSelectedGameObject()
    {
        foreach (var obj in Selection.objects)
        {
            if (obj is GameObject gameObject)
            {
                Transform tf = gameObject.transform;
                Resize(tf, lastScale);
            }
        }
    }

    private void Resize(Transform tf, float scale)
    {
        Vector3 normalizeScale = new Vector3(
                1 / tf.localScale.x,
                1 / tf.localScale.y,
                1 / tf.localScale.z
            );


        tf.localScale = Vector3.one;

        foreach (Transform child in tf)
        {
            Vector3 adjustedScale = new Vector3(
                child.localScale.x * 1f/normalizeScale.x,
                child.localScale.y * 1f/normalizeScale.y,
                child.localScale.z * 1f/normalizeScale.z
            );

            child.localScale = adjustedScale;
        }
    }


}

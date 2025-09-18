/*************************************************
  * 名稱：NewMonoBehaviourScript
  * 作者：RyanHsu
  * 功能說明：
  * ***********************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#if UNITY_EDITOR
//[ExecuteAlways]
#endif
//[DisallowMultipleComponent]
//[RequireComponent(typeof CLASS)]
public class NewMonoBehaviourScript : MonoBehaviour
{
    //DispatchEvent dispatcher = DispatchEvent.GetInstance();
    //[Header("Setting")]
    //[Header("Depend")]

    void Awake()  {}
    void Start()  {}
    void Update() {}

#if UNITY_EDITOR
    //void OnValidate() { }
    //void OnDrawGizmos() { }
    //void OnDrawGizmosSelected() { }

/*
[UnityEditor.CustomEditor(typeof(NewMonoBehaviourScript))]
class NewMonoBehaviourScriptEditor : UnityEditor.Editor
{
    private NewMonoBehaviourScript Target => (NewMonoBehaviourScript)target;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        UnityEditor.EditorGUILayout.Space();
        UnityEditor.EditorGUI.BeginDisabledGroup(!Application.isPlaying || !Target.isActiveAndEnabled);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Start"))
        {
            Target.Start();
        }
        GUILayout.EndHorizontal();
        UnityEditor.EditorGUI.EndDisabledGroup();
    }
}
*/

#endif

}

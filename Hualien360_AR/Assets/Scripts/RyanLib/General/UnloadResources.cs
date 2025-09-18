/*************************************************
  * 名稱：UnloadResources
  * 作者：RyanHsu
  * 功能說明：釋放資源
  * ***********************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class UnloadResources : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 1.0f;
    }
    static bool GCLock;//避免太快重覆GC
    static void DoGCLock()
    {
        GCLock = true;
        MonoManager.DelayCall(1, () => { GCLock = false; });
    }

    [SerializeField] bool AutoGC;

    void OnDestroy()
    {
        if (AutoGC) ClearAllGC();
    }

    public void ClearAllGC()
    {
        if (!GCLock)
        {
            ClearUnusedAssets();
            ForceGarbageCollection();
            DoGCLock();
        } else
        {
            Debug.LogWarning("GCLock");
        }
    }

    public void ClearUnusedAssets()
    {
        Resources.UnloadUnusedAssets();
    }

    public void ForceGarbageCollection()
    {
        System.GC.Collect();
    }

#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(UnloadResources))]
    class UnloadResourcesEditor : UnityEditor.Editor
    {
        private UnloadResources Target => (UnloadResources)target;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            UnityEditor.EditorGUILayout.Space();
            UnityEditor.EditorGUI.BeginDisabledGroup(!Application.isPlaying || !Target.isActiveAndEnabled);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("ClearUnusedAssets"))
            {
                Target.ClearUnusedAssets();
            }
            if (GUILayout.Button("ForceGarbageCollection"))
            {
                Target.ForceGarbageCollection();
            }
            GUILayout.EndHorizontal();
            UnityEditor.EditorGUI.EndDisabledGroup();
        }
    }
#endif

}
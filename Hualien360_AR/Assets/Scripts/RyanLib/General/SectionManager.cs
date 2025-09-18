/*************************************************
  * 名稱：SectionManager
  * 作者：RyanHsu
  * 功能說明：做為訊息說明欄駐列系統的Manager管控中心
  * ***********************************************/
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SectionManager : MonoBehaviour
{
    [Header("Setting")]
    public bool activeOnEnable = false;
    public SectionNode[] sectionNodes;

    [HideInInspector] public UnityEvent onTotalStart;
    [HideInInspector] public UnityEvent onTotalCompleted;

    public int sectionIndex = 0;
    Coroutine coroutine = null;

    void OnEnable()
    {
        if (activeOnEnable) SectionStart();
    }


    [ContextMenu("TestNode")]
    public void TestNodeTest() { TestNode(3); }

    public void TestNode(int Node)
    {
        SectionStop();
        sectionIndex = Node;
        SectionStart();
    }

    void OnDisable()
    {
        SectionStop();
    }

    [ContextMenu("SectionStart")]
    public void SectionStart()
    {
        if (coroutine != null) return;

        sectionIndex = 0;
        onTotalStart?.Invoke();
        coroutine = StartCoroutine(NodeRun());
    }

    [ContextMenu("SectionStop")]
    public void SectionStop()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            sectionNodes.ForEach(m => { if (m != null && m.gameObject.scene != null) m.CallOnCompleted(); });
            sectionNodes.ForEach(m => { if (m != null && m.gameObject.scene != null) m.BroadcastMessage("FadeOut", SendMessageOptions.DontRequireReceiver); });
            coroutine = null;
        }
    }

    IEnumerator NodeRun()
    {
        //do
        //{
        //    sectionNodes[sectionIndex].gameObject.SetActive(true);
        //    sectionNodes[sectionIndex].BroadcastMessage("FadeIn", SendMessageOptions.DontRequireReceiver);
        //    yield return sectionNodes[sectionIndex].init();

        //    sectionNodes[sectionIndex].BroadcastMessage("FadeOut", SendMessageOptions.DontRequireReceiver);
        //    ++sectionIndex;
        //} while (sectionIndex < sectionNodes.Length);

        for (; sectionIndex < sectionNodes.Length; ++sectionIndex)
        {
            sectionNodes[sectionIndex].gameObject.SetActive(true);
            sectionNodes[sectionIndex].BroadcastMessage("FadeIn", SendMessageOptions.DontRequireReceiver);
            yield return sectionNodes[sectionIndex].init();

            sectionNodes[sectionIndex].BroadcastMessage("FadeOut", SendMessageOptions.DontRequireReceiver);
        }

        onTotalCompleted?.Invoke();
        coroutine = null;
    }

    public void SKEP()
    {
        SectionStop();
        this.Invoke(() => { onTotalCompleted?.Invoke(); }, 1f);
    }

#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(SectionManager))]
    class SectionManagerEditor : UnityEditor.Editor
    {
        private bool onFoldout = false;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            UnityEditor.SerializedProperty onTotalStart = serializedObject.FindProperty("onTotalStart");
            UnityEditor.SerializedProperty onTotalCompleted = serializedObject.FindProperty("onTotalCompleted");

            onFoldout = UnityEditor.EditorGUILayout.Foldout(onFoldout, "UnityEvents");
            if (onFoldout)
            {
                UnityEditor.EditorGUILayout.Space();
                UnityEditor.EditorGUILayout.PropertyField(onTotalStart, true); // 顯示 UnityEvent 的完整結構
                UnityEditor.EditorGUILayout.PropertyField(onTotalCompleted, true); // 顯示 UnityEvent 的完整結構
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif

}

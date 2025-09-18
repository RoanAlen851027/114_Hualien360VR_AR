/*************************************************
  * 名稱：DialogueContent
  * 作者：RyanHsu
  * 功能說明：
  * ***********************************************/
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DialogueContent : MonoBehaviour, IPointerClickHandler
{
    public bool activeEnable;
    public bool clickAnyWhere;
    public bool scriptControlOnly = false;
    [TextArea(2, 5)] public List<string> dialogues;
    public UnityEvent onStartDialogue;
    public UnityEvent<int> onEnterDialogue;
    public UnityEvent<int> onNextDialogue;
    public UnityEvent onCompleteDialogue;

    [Header("依賴項")]
    public TMP_Text txt_filed;
    public void ChangeDialogueTxtTarget(TMP_Text target) => txt_filed = target;
    [Header("可選，依照dialog index，將指定tf的subindex物件激活"), ContextMenuItem("自動建立DialogItems內容", "CreatDialogItems")]
    public Transform targetRoot;

    Coroutine coroutine;
    int dialogueIndex = 0;
    bool UpdateLock = true;

    private void Awake()
    {
        UpdateLock = true;
    }

    void Update()
    {
        if (UpdateLock || scriptControlOnly) return;

        if (clickAnyWhere)
        {
            if (Input.GetMouseButtonUp(0))
            {
                OnClick();
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData = null)
    {
        if (clickAnyWhere || scriptControlOnly) return;

        OnClick();
    }

    public void OnClick()
    {
        if (txt_filed.TryGetComponent(out Typewriter typewriter))
        {
            if (typewriter.State != TypewriterState.Completed)
            {
                typewriter.CompleteOutput();
            } else
            {
                OnNext();
            }
        } else
        {
            OnNext();
        }
    }

    public void OnNext() => onNextDialogue?.Invoke(dialogueIndex);

    private void OnEnable()
    {
        if (activeEnable) StartDialoguePush();
    }

    void OnDisable()
    {
        StopDialoguePush();
    }

    public void StartDialoguePush()
    {
        if (coroutine != null) StopDialoguePush();
        coroutine = StartCoroutine(DialoguePush());
    }

    public void StopDialoguePush()
    {
        if (coroutine != null) StopCoroutine(coroutine);
    }

    IEnumerator DialoguePush()
    {
        dialogueIndex = 0;
        onStartDialogue?.Invoke();
        if (targetRoot != null) targetRoot.gameObject.SetActive(true);

        foreach (string dialogue in dialogues)
        {

            if (!string.IsNullOrEmpty(dialogue))
            {
                txt_filed.text = dialogue;
                if (txt_filed.TryGetComponent(out Typewriter typewriter))
                {
                    typewriter.OutputText(dialogue);
                }
                if (txt_filed.TryGetComponent(out TypingEffect typingEffect))
                {
                    typingEffect.StartTypingEffectOnly(dialogue);
                }
            }
            UpdateLock = false;
            onEnterDialogue?.Invoke(dialogueIndex);

            if (targetRoot != null)
            {
                int index = Mathf.Clamp(dialogueIndex, 0, targetRoot.transform.childCount - 1);
                targetRoot.gameObject.Radio(index);
            }

            yield return new WaitForAction<int>(onNextDialogue);
            dialogueIndex += 1;
            UpdateLock = true;
        }

        onCompleteDialogue?.Invoke();
        coroutine = null;
    }

    //ContentMenu
    void CreatDialogItems()
    {
        if (targetRoot != null)
        {
            targetRoot.RemoveAllChild();
            //
            for (int i = 0; i < dialogues.Count; i++)
            {
                GameObject newItem = new GameObject($"dialog_{i}");
                newItem.transform.SetParent(targetRoot);
                RectTransform rect = newItem.AddComponent<RectTransform>();
                rect.anchorMin = Vector2.zero;
                rect.anchorMax = Vector2.one;
                rect.offsetMin = Vector2.zero;
                rect.offsetMax = Vector2.zero;
                newItem.SetActive(false);
            }
        }
    }


    public void SKIP()
    {
        StopDialoguePush();
        coroutine = null;
        onCompleteDialogue?.Invoke();
    }

    //=================================================================================

#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(DialogueContent))]
    class DialogueContentEditor : UnityEditor.Editor
    {
        private DialogueContent Target => (DialogueContent)target;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            UnityEditor.EditorGUILayout.Space();
            UnityEditor.EditorGUI.BeginDisabledGroup(!Application.isPlaying || !Target.isActiveAndEnabled);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("NextDialogue"))
            {
                Target.OnPointerClick();
            }
            if (GUILayout.Button("SKIP"))
            {
                Target.SKIP();
            }
            GUILayout.EndHorizontal();
            UnityEditor.EditorGUI.EndDisabledGroup();
        }
    }
#endif

}
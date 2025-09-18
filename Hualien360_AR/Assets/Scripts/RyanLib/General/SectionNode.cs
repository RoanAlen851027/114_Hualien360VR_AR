/*************************************************
  * 名稱：SectionNode
  * 作者：RyanHsu
  * 功能說明：做為訊息說明欄駐列系統的PartNode
  * ***********************************************/
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

interface ISection
{
    public IEnumerator init();
    public void CallOnCompleted();
}

/// <summary>
/// 做為訊息說明欄駐列系統的PartNode
/// </summary>
public class SectionNode : MonoBehaviour, ISection
{
    [Header("如果大於0，會自動Delay n秒後，執行Completed")]
    public float delayCallCompleted = 0f;
    [Header("結束Section會自我關閉")]
    public bool inactive = true;

    [Header("UnityEvens")][SerializeField] bool showFoldout;
    [DynamicHide("showFoldout")] public UnityEvent onStart;
    [DynamicHide("showFoldout")] public UnityEvent onFadeOut;
    [DynamicHide("showFoldout")] public UnityEvent onCompleted;

    [Header("(可選)")]
    public SectionNodeLink[] sectionNodeLinks;

    void Start()
    {
        if (TryGetComponent(out CanvasGroup group))
        {
            group.enabled = true;
        }
    }

    /// <summary> 
    /// 由父層呼叫的駐列協程 
    /// 將自身active=true後，等待onCompleted
    /// 完成onCompleted後，將自身active=false;
    /// </summary>
    public IEnumerator init()
    {
        SetActive(true);
        if (delayCallCompleted > 0f) Invoke("CallOnCompleted", delayCallCompleted);
        InvokeStart();
        yield return new WaitForAction(onCompleted);
        if (inactive) SetActive(false);
    }

    [ContextMenu("CallOnCompleted")]
    public void CallOnCompleted()
    {
        Broadcast("FadeOut");
        InvokeFadeOut();
        Invoke("InvokeComplete", 1f);
    }

    void SetActive(bool io)
    {
        gameObject.SetActive(io);
        if (sectionNodeLinks.Length > 0) sectionNodeLinks.ForEach(link =>
            link.gameObject.SetActive(io)
        );
    }

    void Broadcast(string id)
    {
        if (enabled == false || gameObject?.scene == null) return;

        BroadcastMessage(id, SendMessageOptions.DontRequireReceiver);
        if (sectionNodeLinks.Length > 0) sectionNodeLinks.ForEach(link =>
            link.gameObject.BroadcastMessage(id, SendMessageOptions.DontRequireReceiver)
        );
    }

    void InvokeStart()
    {
        onStart?.Invoke();
        if (sectionNodeLinks.Length > 0) sectionNodeLinks.ForEach(link =>
            link.onStart?.Invoke()
        );
    }

    void InvokeFadeOut()
    {
        onFadeOut?.Invoke();
        if (sectionNodeLinks.Length > 0) sectionNodeLinks.ForEach(link =>
            link.onFadeOut?.Invoke()
        );
    }

    void InvokeComplete()
    {
        onCompleted?.Invoke();
        if (sectionNodeLinks.Length > 0) sectionNodeLinks.ForEach(link =>
            link.onCompleted?.Invoke()
        );
    }

}
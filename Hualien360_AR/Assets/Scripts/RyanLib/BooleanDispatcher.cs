/*************************************************
  * 名稱：BooleanDispatcher
  * 作者：RyanHsu
  * 功能說明：
  * ***********************************************/
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
[ExecuteAlways]
#endif
public class BooleanDispatcher : MonoBehaviour
{
    DispatchEvent dispatcher = DispatchEvent.GetInstance();
    [Header("數值說明(無實際用途)")]
    [TextArea(1, 3)] public string header;

    [Header("Setting")]
    public bool io;
    //
    [Header("UnityEvens")][SerializeField] bool showFoldout;
    [DynamicHide("showFoldout")] public UnityEvent OnTrue;
    [DynamicHide("showFoldout")] public UnityEvent OnFalse;
    [DynamicHide("showFoldout")] public UnityEvent<bool> OnBool;
    [DynamicHide("showFoldout")] public UnityEvent<bool> OnRevert;

    DirtyBool dirty = new DirtyBool(true);

    void OnEnable()
    {
        DirtyBool dirty = new DirtyBool(!io);
        dispatcher.OnEventScriptObject.AddListener(OnEventScriptObject);
    }

    void OnDisable() => dispatcher.OnEventScriptObject.RemoveListener(OnEventScriptObject);

    void OnEventScriptObject(string id, object evt)
    {
        if (id == gameObject.name && evt is bool io)
        {
            this.io = io;
        }
    }

    #region 給SnedMessenge使用
    public void booleanTrue()
    {
        io = true;
        Dirty();
        OnTrue?.Invoke();
    }
    public void booleanFalse()
    {
        io = false;
        Dirty();
        OnFalse?.Invoke();
    }
    public void booleanAuto()
    {
        io = !io;
        Dirty();
        if (io) OnTrue?.Invoke();
        else OnFalse?.Invoke();
    }
    #endregion

    public void Dirty() => dirty.defaultValue = !io;

    void Update()
    {
        if (enabled == false) return;

        if (dirty.isDirty(io))
        {
            if (io)
            {
                if (OnTrue != null) OnTrue.Invoke();
            } else
            {
                if (OnFalse != null) OnFalse.Invoke();
            }
            if (OnBool != null) OnBool.Invoke(io);
            if (OnRevert != null) OnRevert.Invoke(!io);
        }
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        Update();
    }
#endif

}

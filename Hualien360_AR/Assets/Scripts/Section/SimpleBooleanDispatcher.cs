/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/

using UnityEngine;
using UnityEngine.Events;

public class SimpleBooleanDispatcher : MonoBehaviour
{
    [Header("當前狀態")]
    [SerializeField] private bool value;

    [Header("事件回呼")]
    public UnityEvent OnTrue;
    public UnityEvent OnFalse;
    public UnityEvent<bool> OnValueChanged;

    private bool lastValue;

    void OnEnable()
    {
        // 啟動時立即同步一次
        InvokeEvents(value);
        lastValue = value;
    }

    /// <summary>切換布林值</summary>
    public void Toggle()
    {
        SetValue(!value);
    }

    /// <summary>設定布林值</summary>
    public void SetValue(bool newValue)
    {
        if (value == newValue) return;

        value = newValue;
        InvokeEvents(value);
        lastValue = value;
    }

    private void InvokeEvents(bool v)
    {
        if (v) OnTrue?.Invoke();
        else OnFalse?.Invoke();
        OnValueChanged?.Invoke(v);
    }
}
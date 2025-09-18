/*************************************************
  * 名稱：WaitForAction
  * 作者：RyanHsu
  * 功能說明：與StartCoroutine搭配使用，在Yield Return中
  * 等待一個Action被觸發。
  * ***********************************************/
using System;
using UnityEngine;
using UnityEngine.Events;

public class WaitForAction<T> : CustomYieldInstruction, IDisposable
{
    private readonly UnityEvent<T> _onActionCompleted;
    private bool _isActionCompleted;

    public WaitForAction(UnityEvent<T> onActionCompleted)
    {
        _onActionCompleted = onActionCompleted;
        _isActionCompleted = false;

        _onActionCompleted.AddListener(OnActionCompleted);
    }

    private void OnActionCompleted(T n)
    {
        Dispose();
    }

    /// <summary> 銷毀時觸發 </summary>
    public void Dispose()
    {
        _isActionCompleted = true;
        _onActionCompleted.RemoveListener(OnActionCompleted);
    }

    public override bool keepWaiting => !_isActionCompleted;
}

public class WaitForAction : CustomYieldInstruction
{
    private readonly UnityEvent _onActionCompleted;
    private bool _isActionCompleted;

    public WaitForAction(UnityEvent onActionCompleted)
    {
        _onActionCompleted = onActionCompleted;
        _isActionCompleted = false;

        _onActionCompleted.AddListener(OnActionCompleted);
    }

    private void OnActionCompleted()
    {
        _isActionCompleted = true;
        _onActionCompleted.RemoveListener(OnActionCompleted);
    }

    public override bool keepWaiting => !_isActionCompleted;
}

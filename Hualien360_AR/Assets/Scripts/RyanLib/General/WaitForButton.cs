/*************************************************
  * 名稱：WaitForButton
  * 作者：RyanHsu
  * 功能說明：與StartCoroutine搭配使用，在Yield Return中
  * 等待一個Button的onClick被觸發。
  * ***********************************************/
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WaitForButton : CustomYieldInstruction
{
    private readonly UnityEvent _onActionCompleted;
    private bool _isActionCompleted;

    public WaitForButton(Button button)
    {
        _onActionCompleted = button.onClick;
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

/*************************************************
  * 名稱：WaitForBool
  * 作者：RyanHsu
  * 功能說明：與StartCoroutine搭配使用，在Yield Return中
  * 等待一個Action被觸發。
  * ***********************************************/
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class WaitForBool : CustomYieldInstruction
{
    private bool _isBool;

    public WaitForBool(bool isBool)
    {
        _isBool = isBool;
    }

    public override bool keepWaiting => _isBool;
}

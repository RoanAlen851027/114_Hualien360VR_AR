/********************************
---------------------------------
著作者：RoanAlen
用途：按鈕管理者
---------------------------------
*********************************/
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;
using DG.Tweening;
public class ButtonMoveManager : MonoBehaviour
{

    public List<MoveButtonAvoidLastPoint> moveButtons;
    public PersonalityTestManager personalityTestManager;

    private void Start()
    {
        for (int i = 0; i < moveButtons.Count; i++)
        {
            moveButtons[i].moveButton.onClick.AddListener(MoveBTN);
        }
    }
    public void OnTestEnd()
    {
        for (int i = 0; i < moveButtons.Count; i++)
        {
            moveButtons[i].CancelInvoke("EnableButton"); // 取消呼叫 EnableButton
            moveButtons[i].buttonRectTransform.DOKill(); // 停止所有 DOTween 動畫
            moveButtons[i].moveButton.interactable = false; // 立即關閉按鈕
        }
    }
    //public void DisableMoveButtons()
    //{
    //    for (int i = 0; i < moveButtons.Count; i++)
    //    {
    //        moveButtons[i].moveButton.interactable = false;
    //    }
    //    Debug.Log("心理測驗已結束，按鈕已禁用");
    //}

    public void MoveBTN()
    {
        for (int i = 0; i < moveButtons.Count; i++)
        {
            moveButtons[i].MoveButton();
        }
    }

#if UNITY_EDITOR

    private void OnValidate()
    {
        if (moveButtons.Count == 0)
        {
            moveButtons = GetComponentsInChildren<MoveButtonAvoidLastPoint>().ToList();
        }
        if (personalityTestManager == null)
        {
            personalityTestManager = GetComponentInChildren<PersonalityTestManager>();
        }
    }

#endif
}

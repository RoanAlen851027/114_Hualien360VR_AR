/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BackgroundMover : MonoBehaviour
{
    public RectTransform background1; // 第一個背景
    public RectTransform background2; // 第二個背景

    private int currentIndex = 0; // 記錄當前移動的索引
    public float[] bg1Positions; // 第一個背景的目標 X 值
    public float[] bg2Positions; // 第二個背景的目標 X 值

    //public float Z = 800; // 自定義數值
    //public float X = 1600; // 自定義數值
    //public float Y = 2400; // 自定義數值

    public float moveDuration = 5f; // 移動動畫的時間
    public float resetDuration = 0f; // 回到原點的時間
    private Coroutine coroutine;
    void Start()
    {
        // 初始化第二個背景的移動點
        //bg2Positions = new float[] { 0, Z, X, Y };
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            MoveBackgrounds();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            ResetBackgrounds();
        }
    }

    public void MoveBackgrounds()
    {
        if (currentIndex < bg1Positions.Length)
        {
            // 移動第一個背景
            background1.DOAnchorPosX(bg1Positions[currentIndex], moveDuration).SetEase(Ease.Linear);

            // 移動第二個背景
            background2.DOAnchorPosX(bg2Positions[currentIndex], moveDuration).SetEase(Ease.Linear);

            currentIndex++; // 更新索引
        }
    }

    public void WaitToClosed()
    {
        coroutine=StartCoroutine(WaitToClose());
    }

    IEnumerator WaitToClose()
    {
        yield return new WaitForSeconds(1f);
        coroutine = null;

    }
    public void ResetBackgrounds()
    {
        // 取消所有 Tween 動畫，避免倒帶
        background1.DOKill();
        background2.DOKill();

        // 直接將背景位置設為 0（瞬間回到原點）
        background1.anchoredPosition = new Vector2(0, background1.anchoredPosition.y);
        background2.anchoredPosition = new Vector2(0, background2.anchoredPosition.y);

        // 重置索引
        currentIndex = 0;
    }
}


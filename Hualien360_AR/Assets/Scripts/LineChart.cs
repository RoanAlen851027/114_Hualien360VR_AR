/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using UnityEngine;

public class LineChart : MonoBehaviour
{
    private LineRenderer line;
    [SerializeField] private Transform[] points; // 需要顯示的點
    private Transform[] pointsReversed; // 反向的點陣列



    [SerializeField, Range(.75f, 5)]
    private float drawTime=1;
    private int currentIndex = 0; // 當前顯示的點的索引
    private float timer = 0f; // 計時器，用來控制逐步顯示
    private Vector3 previousPosition; // 上一個已顯示的點
    public bool shouldRedraw = false; // 控制是否需要重新繪製線條
    public bool reverseDrawing = false; // 控制是否逆向繪製
    public bool animationLine = false;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 0; // 一開始不顯示任何點
        previousPosition = points[0].position; // 初始設定為第一個點的位置

        // 創建反向的點陣列
        pointsReversed = new Transform[points.Length];
        for (int i = 0; i < points.Length; i++)
        {
            pointsReversed[i] = points[points.Length - 1 - i];
        }
    }

    public void ReDrawLine()
    {
        shouldRedraw = true;
    }

    private void Update()
    {
        if (shouldRedraw)
        {
            ResetDrawing();
        }

        // 判斷是否要跑動畫
        if (animationLine)
        {
            if (reverseDrawing)
            {
                ReverseDrawing();
            }
            else
            {
                NormalDrawing();
            }
        }
        else
        {
            ShowFullLine();
        }
    }

    private void ShowFullLine()
    {
        Transform[] targetPoints = reverseDrawing ? pointsReversed : points;

        line.positionCount = targetPoints.Length; // 設定點數量
        for (int i = 0; i < targetPoints.Length; i++)
        {
            line.SetPosition(i, targetPoints[i].position); // 直接設定點的位置
        }
    }


    // 正向繪製
    private void NormalDrawing()
    {
        if (currentIndex < points.Length)
        {
            // 每次更新，會依照時間插值讓當前點的繪製進行平滑過渡
            timer += Time.deltaTime* drawTime;
            // 使用 Lerp 進行平滑過渡，讓新點平滑地過渡到下一個點
            Vector3 targetPosition = points[currentIndex].position;
            Vector3 currentPosition = Vector3.Lerp(previousPosition, targetPosition, timer);

            // 確保 positionCount 足夠
            if (line.positionCount <= currentIndex)
            {
                line.positionCount = currentIndex + 1; // 增加顯示的點數
            }

            // 設定 LineRenderer 的位置
            line.SetPosition(currentIndex, currentPosition); // 設定當前點的位置

            // 當過渡完成時，更新位置
            if (timer >= 1f)
            {
                previousPosition = targetPosition; // 更新上一個顯示點的位置
                currentIndex++; // 增加顯示的點索引
                timer = 0f; // 重置計時器
            }
        }
        else
        {
            // 當線條繪製完成後，保持最後的線條並不斷更新
            UpdateLinePositions();
        }
    }

    // 逆向繪製
    private void ReverseDrawing()
    {
        if (currentIndex < pointsReversed.Length)
        {
            // 每次更新，會依照時間插值讓當前點的繪製進行平滑過渡
            timer += Time.deltaTime * drawTime;
   
            // 使用 Lerp 進行平滑過渡，讓新點平滑地過渡到上一個點
            Vector3 targetPosition = pointsReversed[currentIndex].position;
            Vector3 currentPosition = Vector3.Lerp(previousPosition, targetPosition, timer);

            // 確保 positionCount 足夠
            if (line.positionCount <= currentIndex)
            {
                line.positionCount = currentIndex + 1; // 增加顯示的點數
            }

            // 設定 LineRenderer 的位置
            line.SetPosition(currentIndex, currentPosition); // 設定當前點的位置

            // 當過渡完成時，更新位置
            if (timer >= 1f)
            {
                previousPosition = targetPosition; // 更新上一個顯示點的位置
                currentIndex++; // 增加顯示的點索引
                timer = 0f; // 重置計時器
            }
        }
        else
        {
            // 當線條逆向繪製完成後，保持最後的線條並不斷更新
            UpdateLinePositions();
        }
    }

    // 持續更新已繪製的點
    private void UpdateLinePositions()
    {
        // 根據 reverseDrawing 的布林值來選擇要更新的陣列
        Transform[] targetPoints = reverseDrawing ? pointsReversed : points;

        for (int i = 0; i < targetPoints.Length; i++)
        {
            // 每次更新，持續更新所有點的最後位置
            line.SetPosition(i, targetPoints[i].position);
        }
    }

    // 重置畫線過程
    public void TriggerRedraw()
    {
        shouldRedraw = true; // 設置為需要重繪
    }

    // 重置畫線的變數
    private void ResetDrawing()
    {
        shouldRedraw = false; // 重置重繪標誌
        currentIndex = 0; // 重置顯示點索引
        timer = 0f; // 重置計時器
        line.positionCount = 0; // 清除現有的線條
        previousPosition = points[0].position; // 重設第一個點的位置
    }
}

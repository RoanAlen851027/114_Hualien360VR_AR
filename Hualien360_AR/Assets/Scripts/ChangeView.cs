/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ChangeView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [Header("提示")]
    public GameObject tutorialObject;
    [Space(10), Header("按下按鈕切換頁面")]
    public Button LeftArrow;
    public Button RightArrow;
    [Space(10)]
    public GameObject[] all;  // 動態數量的面板
    public int StartIndex = 0;
    public Transform pos_left_2;
    public Transform pos_left;
    public Transform pos_Mid;
    public Transform pos_right;
    public Transform pos_right_2;
    int length = 0;
    public float speed = 1f;
    [HideInInspector]
    public bool IsTweening = false;

    int Mid_Index = 0;
    int left_Index;
    int left_Index_2;
    int right_Index;
    int right_Index_2;


    private bool hiddenPanelUnlocked = false;
    public GameObject HidenPoint;

    [Header("是否啟用強制觀看")]
    private bool[] viewedPanels; // 紀錄哪些面板已看過
    public bool requireAllViewed = false; // 是否啟用強制觀看機制
    public bool IsAllSee=false;
    private Coroutine coroutine;

    [Header("事件")]
    public UnityEvent unityEvent;
    private void Start()
    {
   
        length = all.Length;
        Mid_Index = StartIndex;
        viewedPanels = new bool[length]; // 初始化為 false

        init();
    }

    private Vector2 startPos;
    public float swipeThreshold = 50f; // 判定滑動的最小距離
    public void OnPointerDown(PointerEventData eventData)
    {
        if (IsTweening) return;
        startPos = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (IsTweening) return;

        Vector2 endPos = eventData.position;
        Vector2 delta = endPos - startPos;

        if (delta.magnitude < swipeThreshold)
            return;

        if (Mathf.Abs(delta.y) > Mathf.Abs(delta.x))
        {
            // 垂直滑動
            if (delta.y > 0)
                TurnRight(); // 向上滑
            else
                TurnLeft();  // 向下滑
        }
        else
        {
            // 水平滑動（保留未來使用）
            if (delta.x > 0)
                Debug.Log("向右滑：預留功能");
            else
                Debug.Log("向左滑：預留功能");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            AddHiddenPanel();
            //UnlockHiddenPanel();
            //Mid_Index = 8;
            //init(); // 重新配置 carousel 面板位置

        }
    }


    public void AddHiddenPanel()
    {
        UnlockHiddenPanel();
        Mid_Index = 8;
        init(); // 重新配置 carousel 面板位置
        UpdatePanelSelection();
    }
    //void Update()
    //{
    //    if (IsTweening) return;

    //    // 按下滑動開始
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        startPos = Input.mousePosition;
    //        isDragging = true;
    //    }

    //    // 滑動結束
    //    if (Input.GetMouseButtonUp(0) && isDragging)
    //    {
    //        Vector2 endPos = Input.mousePosition;
    //        Vector2 delta = endPos - startPos;

    //        // 防誤觸閾值
    //        if (delta.magnitude > 50f)
    //        {
    //            // 判斷主要滑動方向
    //            if (Mathf.Abs(delta.y) > Mathf.Abs(delta.x))
    //            {
    //                // 垂直滑動
    //                if (delta.y > 0)
    //                    TurnRight();   // 向上滑 → 看前一個
    //                else
    //                    TurnLeft();    // 向下滑 → 看下一個
    //            }
    //            else
    //            {
    //                // 水平滑動（目前不做事，預留）
    //                if (delta.x > 0)
    //                {
    //                    Debug.Log("右滑：未來可切換角色或頁面");
    //                }
    //                else
    //                {
    //                    Debug.Log("左滑：未來可切換角色或頁面");
    //                }
    //            }
    //        }

    //        isDragging = false;
    //    }
    //}


    public void UnlockHiddenPanel()
    {
        if (hiddenPanelUnlocked || HidenPoint == null)
            return;

        hiddenPanelUnlocked = true;

        Debug.Log("手動解鎖隱藏面板：" + HidenPoint.name);

        HidenPoint.SetActive(true);

        // 建立新的 all 陣列 +1 長度
        GameObject[] newAll = new GameObject[all.Length + 1];
        for (int i = 0; i < all.Length; i++)
        {
            newAll[i] = all[i];
        }
        newAll[all.Length] = HidenPoint;
        all = newAll;

        // 建立新的 viewedPanels 陣列
        bool[] newViewed = new bool[viewedPanels.Length + 1];
        for (int i = 0; i < viewedPanels.Length; i++)
        {
            newViewed[i] = viewedPanels[i];
        }
        newViewed[viewedPanels.Length] = false;
        viewedPanels = newViewed;

        length = all.Length;
    }


    private void OnRestartBTN()
    {
        IsTweening = false;
        if (all == null || all.Length == 0)
        {
            Debug.LogError("錯誤！'all' 陣列為空，無法重新初始化！");
            return;
        }
        init();
    }

    public void init()
    {
        for (int i = 0; i < length; i++)
        {
            all[i].transform.localScale = Vector3.zero;
            all[i].gameObject.SetActive(false);
        }

        all[Mid_Index].transform.position = pos_Mid.position;
        all[Mid_Index].transform.localScale = pos_Mid.localScale;
        all[Mid_Index].gameObject.SetActive(true);

        left_Index = (Mid_Index - 1 + length) % length;
        all[left_Index].transform.position = pos_left.position;
        all[left_Index].transform.localScale = pos_left.localScale;
        all[left_Index].gameObject.SetActive(true);

        right_Index = (Mid_Index + 1) % length;
        all[right_Index].transform.position = pos_right.position;
        all[right_Index].transform.localScale = pos_right.localScale;
        all[right_Index].gameObject.SetActive(true);

        all[right_Index].transform.SetAsLastSibling();
        all[left_Index].transform.SetAsLastSibling();
        all[Mid_Index].transform.SetAsLastSibling();

        // 標記當前面板為已觀看
        viewedPanels[Mid_Index] = true;
    }

    public void TurnLeft()
    {
        tutorialObject.SetActive(false);

        if (IsTweening)
            return;

        coroutine= StartCoroutine(ITurnLeft());
    }


    IEnumerator ITurnLeft()
    {
        IsTweening = true;
        right_Index_2 = (right_Index + 1) % length;
        all[right_Index_2].transform.position = pos_right_2.position;
        all[right_Index_2].transform.localScale = pos_right_2.localScale;
        all[right_Index_2].gameObject.SetActive(true);

        all[right_Index_2].transform.SetAsLastSibling();
        all[right_Index].transform.SetAsLastSibling();
        all[Mid_Index].transform.SetAsLastSibling();

        all[right_Index_2].transform.DOMove(pos_right.position, speed);
        all[right_Index_2].transform.DOScale(pos_right.localScale, speed);
        all[right_Index].transform.DOMove(pos_Mid.position, speed);
        all[right_Index].transform.DOScale(pos_Mid.localScale, speed);
        all[Mid_Index].transform.DOMove(pos_left.position, speed);
        all[Mid_Index].transform.DOScale(pos_left.localScale, speed);
        all[left_Index].transform.DOMove(pos_left_2.position, speed);
        all[left_Index].transform.DOScale(pos_left_2.localScale, speed);

        yield return new WaitForSeconds(speed);
        all[left_Index].gameObject.SetActive(false);

        // 更新索引
        Mid_Index = (Mid_Index + 1) % length;
        left_Index = (Mid_Index - 1 + length) % length;
        right_Index = (Mid_Index + 1) % length;

        // **標記新進入中間的面板為已觀看**
        viewedPanels[Mid_Index] = true;

        // **檢查是否所有面板都已觀看**
        if (!viewedPanels.Contains(false))
        {
            Debug.Log("全部面板已看過");
        }
        if (AllPanelsViewed())
        {
            Debug.Log("全部面板已看過");
        }

        IsTweening = false;
        UpdatePanelSelection();
        // 印出當前中間面板的名稱
        Debug.Log("目前中間的面板是：" + all[Mid_Index].name);
    }

    public void TurnRight()
    {
        tutorialObject.SetActive(false);

        if (IsTweening)
            return;

        coroutine= StartCoroutine(ITurnRight());
    }


    IEnumerator ITurnRight()
    {
        IsTweening = true;
        left_Index_2 = (left_Index - 1 + length) % length;
        all[left_Index_2].transform.position = pos_left_2.position;
        all[left_Index_2].transform.localScale = pos_left_2.localScale;
        all[left_Index_2].gameObject.SetActive(true);

        all[left_Index_2].transform.SetAsLastSibling();
        all[left_Index].transform.SetAsLastSibling();
        all[Mid_Index].transform.SetAsLastSibling();

        all[left_Index_2].transform.DOMove(pos_left.position, speed);
        all[left_Index_2].transform.DOScale(pos_left.localScale, speed);
        all[left_Index].transform.DOMove(pos_Mid.position, speed);
        all[left_Index].transform.DOScale(pos_Mid.localScale, speed);
        all[Mid_Index].transform.DOMove(pos_right.position, speed);
        all[Mid_Index].transform.DOScale(pos_right.localScale, speed);
        all[right_Index].transform.DOMove(pos_right_2.position, speed);
        all[right_Index].transform.DOScale(pos_right_2.localScale, speed);

        yield return new WaitForSeconds(speed);
        all[right_Index].gameObject.SetActive(false);

        // 更新索引
        Mid_Index = (Mid_Index - 1 + length) % length;
        left_Index = (Mid_Index - 1 + length) % length;
        right_Index = (Mid_Index + 1) % length;

        // **標記新進入中間的面板為已觀看**
        viewedPanels[Mid_Index] = true;

        // **檢查是否所有面板都已觀看**
        if (!viewedPanels.Contains(false))
        {
            Debug.Log("全部面板已看過");
        }
        if (AllPanelsViewed())
        {
            Debug.Log("全部面板已看過");
        }

        IsTweening = false;
        UpdatePanelSelection();
        // 印出當前中間面板的名稱
        Debug.Log("目前中間的面板是：" + all[Mid_Index].name);
    }
    private void OnEnable()
    {
        IsTweening = false; // 確保重新啟用時，按鈕可以正常運作
    }

    // 檢查是否所有面板都已看過
    private bool AllPanelsViewed()
    {
        foreach (bool viewed in viewedPanels)
        {
            if (!viewed) return false;
        }

        Debug.Log("所有面板都已經看過了！");
        IsAllSee = true;

        if (unityEvent != null)
        {
            unityEvent?.Invoke();
        }
        return true;
    }

    //中間選擇亮起來 中間的線條亮起來
    public void UpdatePanelSelection()
    {
        for (int i = 0; i < all.Length; i++)
        {
            var handler = all[i].GetComponent<PanelSelectionHandler>();
            if (handler == null) continue;

            if (i == Mid_Index)
                handler.OnSelect();
            else
                handler.OnDeselect();
        }
    }

}

/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using UnityEngine;
using UnityEngine.UI;

public class HorizontalScrollTrigger : MonoBehaviour
{
    public ScrollRect scrollRect;
    private bool triggered = false;
    public GameObject ScrolContent;

    public CanvasFadeOut canvasFade;
    void Update()
    {
        float pos = scrollRect.horizontalNormalizedPosition;

        // 當在 0.8 ~ 0.7 之間時觸發
        if (!triggered && pos <= 0.8f && pos >= 0.7f)
        {
            triggered = true;
            Debug.Log("觸發事件！當前水平位置: " + pos);
            // 這裡執行你的事件
            ScrolContent.SetActive(false);
            canvasFade.FadeOut();
        }

        if (triggered==false)
        {
            ScrolContent.SetActive(true);
        }

        // 重置觸發條件 (避免只觸發一次就永遠不能觸發)
        if (pos < 0.6f)
        {
            triggered = false;
        }
    }
}

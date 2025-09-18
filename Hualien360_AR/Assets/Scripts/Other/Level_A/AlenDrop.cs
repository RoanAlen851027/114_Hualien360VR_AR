using UnityEngine;
/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class AlenDrop : MonoBehaviour, IDropHandler
{

    public string AnswerName;
    private AlenDrag currentDragItem; // 記錄當前拖進來的物件


    public int CurrentAnswer= 0;
    public Image Drop_FillAmount;
    public Image targetImage;
    public TextMeshProUGUI CurrentAnswer_Text;

    public ShakeEffect shakeEffect;

    public AudioPlayScriptable Correct_Sfx;
    public AudioPlayScriptable Error_Sfx;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            currentDragItem = eventData.pointerDrag.GetComponent<AlenDrag>();

            if (currentDragItem != null)
            {
                if (currentDragItem.TheQuestionAnswer == AnswerName)
                {
                    Debug.Log("答案正確！");
                    currentDragItem.transform.SetParent(transform); // 讓答案歸屬於這個 Drop 物件
                    currentDragItem.transform.localPosition = new Vector3(-3f, -10f, 0f);
                    currentDragItem.GetCurrect();
                    FindFirstObjectByType<DragDropQuestionManager>().LoadNextQuestion(); // 載入下一題
                    CurrentAnswer++;
                    PlayScaleEffect();
                    CurrentAnswer_Text.text = $"<size=60>{CurrentAnswer}</size>/3";
                    CurrentFillAmount(CurrentAnswer);
                    Correct_Sfx.Play();
                    //currentDragItem.ResetPosition(); // 回到原本位置
                }
                else
                {
                    Debug.Log("答案錯誤！回到原位");
                    currentDragItem.ResetPosition(); // 回到原本位置
                    shakeEffect.TriggerShake();
                    Error_Sfx.Play();
                }
            }
        }
    }

    public void CurrentFillAmount(int current)
    {
        float targetFill = 0f;

        switch (current)
        {
            case 0: targetFill = 0f; break;
            case 1: targetFill = 0.33f; break;
            case 2: targetFill = 0.66f; break;
            case 3: targetFill = 1f; break;
        }

        // 使用 DOTween 讓 FillAmount 平滑變化
        if (Drop_FillAmount != null)
        {
            DOTween.To(() => Drop_FillAmount.fillAmount, x => Drop_FillAmount.fillAmount = x, targetFill, 0.5f)
                .SetEase(Ease.OutQuad);
        }
        else
        {
            Debug.LogError("Drop_FillAmount 未正確設置！");
        }
    }

    public void PlayScaleEffect()
    {
        // 重置 Scale，避免連續點擊後動畫錯亂
        targetImage.rectTransform.localScale = Vector3.one;

        // 執行 DoTween 動畫：放大到 1.15，然後縮回 1
        targetImage.rectTransform.DOScale(1.15f, 0.3f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                targetImage.rectTransform.DOScale(1f, 0.1f).SetEase(Ease.InQuad);
            });
    }



#if UNITY_EDITOR

    private void OnValidate()
    {
        if (targetImage == null) 
        {
            targetImage = this.gameObject.transform.GetChild(0).GetComponent<Image>();
        }

        if (shakeEffect == null)
        {
            shakeEffect = this.gameObject.transform.GetChild(0).GetComponent<ShakeEffect>();
        }
    }
#endif
}

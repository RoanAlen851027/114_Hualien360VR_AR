/*************************************************
  * 名稱：LoadingFadeManager
  * 作者：RyanHsu
  * 功能說明：
  * ***********************************************/
using DG.Tweening;
using UnityEngine;

public class LoadingFadeManager : MonoBehaviour, IDispatchEvent
{
    DispatchEvent dispatcher = DispatchEvent.GetInstance();
    public CanvasGroup group = null;

    private void Awake()
    {
        dispatcher.OnLoadingFadeOut.AddListener(FaidOut);
    }

    private void OnDestroy()
    {
        dispatcher.OnLoadingFadeOut.RemoveListener(FaidOut);
    }

    void OnEnable()
    {
        FaidIn();
    }

    public void FaidIn(float d = 1f)
    {
        group.DOFade(1f, 0.5f * d);
        transform.DOScale(1f, d)
            .From(0f)
            .SetEase(Ease.OutBounce);
        SingletonEventSystem.Enable(false);//關閉Event事件
    }

    public void FaidOut(float d = 1f)
    {
        group.DOFade(0f, 0.5f * d);
        transform.DOScale(0f, d)
            .From(1f).SetEase(Ease.OutBounce)
            .OnComplete(() =>
            {
                SingletonEventSystem.Enable(true);//恢復Event事件
            });
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (group == null)
        {
            group = GetComponentInParent<CanvasGroup>();
        }
    }
#endif

}
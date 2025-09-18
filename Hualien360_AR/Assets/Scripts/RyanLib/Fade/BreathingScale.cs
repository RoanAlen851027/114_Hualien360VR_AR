using DG.Tweening;
using UnityEngine;

public class BreathingScale : MonoBehaviour
{
    /// <summary> x:min y:max </summary>
    public Vector2 intensity = new Vector2(0f, 1f);
    [Range(0.1f, 1f)] public float duration = 0.5f;

    /// <summary> 啟動/關閉 呼吸燈 </summary>
    public Bindable<bool> turnOn = new Bindable<bool>();
    Sequence sequence = null;

    void OnEnable()
    {
        turnOn.OnChanged += OnChangeTurn;
        OnChangeTurn(turnOn.Get());
    }

    void OnDisable()
    {
        OnChangeTurn(false);
        turnOn.OnChanged -= OnChangeTurn;
#if UNITY_EDITOR
        OnDestroy();
#endif
    }

    public void Reflash()
    {
        Turn(false);
        Turn(true);
    }

    public void Turn(bool io) => turnOn.Set(io);

    void OnChangeTurn(bool io)
    {
        if (io)
        {
            if (sequence != null && sequence.IsPlaying())
            {
                sequence.Kill();
            }
            transform.DOScale(intensity.x, 0);
            sequence = DOTween.Sequence();
            sequence.Append(transform.DOScale(new Vector3().SetValue(intensity.y), duration).SetEase(Ease.OutQuad));
            sequence.Append(transform.DOScale(new Vector3().SetValue(intensity.x), duration).SetEase(Ease.InQuad));
            sequence.SetLoops(-1);
            sequence.OnKill(() => transform.localScale = new Vector3().SetValue(intensity.x));
        } else
        {
            if (sequence != null && sequence.IsPlaying())
            {
                sequence.Kill();
            }
        }
    }

    private void OnDestroy()
    {
        if (sequence != null && sequence.IsPlaying())
        {
            sequence.Kill();
        }
    }

}

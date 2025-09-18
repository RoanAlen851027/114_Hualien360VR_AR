using DG.Tweening;
using UnityEngine;

public class BreathingShake : MonoBehaviour
{
    [Header("Setting")]
    /// <summary> x:min y:max </summary>
    public float delay = 1f;
    public float strength = 1f;
    public int vibrato = 10;
    public float randomness = 90f;
    public Ease ease = Ease.OutQuad;
    [Range(0.1f, 1f)] public float duration = 0.5f;
    /// <summary> 啟動/關閉 呼吸燈 </summary>
    public Bindable<bool> turnOn = new Bindable<bool>();
    public void TurnOn(bool io) => turnOn.Set(io);

    public Vector3 revertScale = Vector3.one;

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
    }

    void OnChangeTurn(bool io)
    {
        if (io)
        {
            sequence = DOTween.Sequence();
            sequence.Append(transform.DOShakeScale(duration, strength, vibrato, randomness).SetEase(ease).SetDelay(delay));
            sequence.SetLoops(-1);
        } else
        {
            sequence.Kill();
            RevertScale();
        }
    }

    void RevertScale()
    {
        transform.localScale = revertScale;
    }

    private void OnDestroy()
    {
        if (sequence != null && sequence.IsPlaying())
        {
            sequence.Kill();
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (!Application.isPlaying)
        {
            revertScale = transform.localScale;
        }
    }
#endif
}

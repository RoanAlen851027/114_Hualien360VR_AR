using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Image))]
public class BreathingLight : MonoBehaviour
{
    Image imgLight = null;
    /// <summary> x:min y:max </summary>
    public Vector2 intensity = new Vector2(0f, 1f);
    [Range(0.1f, 1f)] public float duration = 0.5f;

    /// <summary> 啟動/關閉 呼吸燈 </summary>
    //public bool turnOn = false;
    public Bindable<bool> turnOn = new Bindable<bool>();
    DirtyBool dirty = new DirtyBool(false);
    Sequence sequence = null;

    void Awake()
    {
        imgLight = GetComponent<Image>();
    }

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
            imgLight.DOFade(intensity.x, 0);
            sequence = DOTween.Sequence();
            sequence.Append(imgLight.DOFade(intensity.y, duration).SetEase(Ease.OutQuad));
            sequence.Append(imgLight.DOFade(intensity.x, duration).SetEase(Ease.InQuad));
            sequence.SetLoops(-1);
            sequence.OnKill(() => imgLight.CrossFadeAlpha(intensity.x,0,true));
        } else
        {
            sequence.Kill();
        }
    }

    private void OnDestroy()
    {
        if (sequence != null && sequence.IsPlaying())
        {
            sequence.Kill();
        }
    }

    [ContextMenu("RandomDuration")] void RandomDuration() => duration = Random.Range(0.1f, 1f);
    [ContextMenu("RandomScale(0.1f-1f)")] void RandomScale() => transform.localScale = new Vector3().SetValue(Random.Range(0.1f, 1f));
    [ContextMenu("RandomScale(0.1f-2f)")] void RandomScale2() => transform.localScale = new Vector3().SetValue(Random.Range(0.1f, 2f));

}

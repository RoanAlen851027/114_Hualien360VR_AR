using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class DoShake : MonoBehaviour
{
    [Header("³]©w¶µ")]
    public float duration = 0.3f;
    public float strength = 0.2f;
    public int vibrate = 1;
    public float randomness = 10;

    public Vector2 localScale = default;
    Tweener tweener = null;

    void OnEnable()
    {
        if (localScale == default) localScale = transform.localScale;
    }

    void OnDisable()
    {
        try
        {
            tweener.Kill(true);
        } catch (Exception e)
        {
            Debug.LogException(e);
        }
    }


    public void Shake()
    {
        if (tweener != null && tweener.IsActive()) { tweener.Kill(true); }
        tweener = transform.DOShakeScale(duration, strength, vibrate, randomness).OnComplete(() => { transform.localScale = localScale; });
    }

}

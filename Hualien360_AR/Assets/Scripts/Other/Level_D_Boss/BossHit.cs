using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
public class BossHit : MonoBehaviour
{

    public Animator boss_Ani;
    public UnityEvent hit_Event;

    public int BossHP=100;
    public Image boss_hp_Bar;

    public void ResetBossHP()
    {
        BossHP = 100;
        float targetValue = Mathf.InverseLerp(0f, 100f, BossHP);
        boss_hp_Bar.DOFillAmount(targetValue, 1f).SetEase(Ease.OutCubic);
    }

    public void SubBossHP()
    {
        BossHP -= 20;
        float targetValue = Mathf.InverseLerp(0f, 100f, BossHP);
        boss_hp_Bar.DOFillAmount(targetValue, 1f).SetEase(Ease.OutCubic);
    }

    public void BossGetHit()
    {
        boss_Ani.SetTrigger("BossHit");
    }
    
    public void HitEvent()
    {
        hit_Event?.Invoke();
        SubBossHP();

    }
}

using UnityEngine;
/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/

using DG.Tweening;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEditor;
public class CharaterEquiment : MonoBehaviour
{
    public List<SpriteRenderer> CharaterBody;

    public List<SpriteRenderer> CharaterHead;

    public List<SpriteRenderer> CharaterWeapon;


    public GameObject CharaterObj;
    public Transform orangin_Tf;
    void Start()
    {
        //foreach (var bodyPart in CharaterBody)
        //{
        //    if (bodyPart != null)
        //    {
        //        Color color = bodyPart.color;
        //        color.a = 0;
        //        bodyPart.color = color;
        //    }
        //}
        //foreach (var headPart in CharaterHead)
        //{
        //    if (headPart != null)
        //    {
        //        Color color = headPart.color;
        //        color.a = 0;
        //        headPart.color = color;
        //    }
        //}
    }

    public void Reset_Tf()
    {
        CharaterObj.transform.localPosition = new Vector3(0, -200, 0);
    }

    public void FadeInCharacterBody(float duration = 1f)
    {
        foreach (var bodyPart in CharaterBody)
        {
            if (bodyPart != null)
            {
                DOTween.ToAlpha(
                    () => bodyPart.color,
                    x => bodyPart.color = x,
                    1f, // 目標 Alpha 值
                    duration
                ).SetTarget(bodyPart);
            }
        }
    }

    public void Get__Body()
    {
        foreach (var bodyPart in CharaterBody)
        {
            if (bodyPart != null)
            {
                Color color = bodyPart.color;
                color.a = 1;
                bodyPart.color = color;
            }
            Debug.LogError($"我有點顏色瞧瞧{bodyPart.color.a}");
        }
    }

    public void Get__Head()
    {
        foreach (var headPart in CharaterHead)
        {
            if (headPart != null)
            {
                Color color = headPart.color;
                color.a = 1;
                headPart.color = color;
            }
        }
    }


    public void FadeInCharacterHead(float duration = 1f)
    {
        foreach (var headPart in CharaterHead)
        {
            if (headPart != null)
            {
                DOTween.ToAlpha(
                    () => headPart.color,
                    x => headPart.color = x,
                    1f, // 目標 Alpha 值
                    duration
                ).SetTarget(headPart);
            }
        }
    }


    public void Get__Weapon()
    {
        foreach (var WeaponPart in CharaterWeapon)
        {
            if (WeaponPart != null)
            {
                Color color = WeaponPart.color;
                color.a = 1;
                WeaponPart.color = color;
            }
        }
    }


    public void FadeInCharacterWeapon(float duration = 1f)
    {
        foreach (var WeaponPart in CharaterWeapon)
        {
            if (WeaponPart != null)
            {
                DOTween.ToAlpha(
                    () => WeaponPart.color,
                    x => WeaponPart.color = x,
                    1f, // 目標 Alpha 值
                    duration
                ).SetTarget(WeaponPart);
            }
        }
    }
}

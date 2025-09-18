/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System.Collections.Generic;
using RoanAlen.Extension;
using UnityEngine.Events;


public class Section8_Control : SectionBase
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayS8();
        }
    }

    public void PlayS8()
    {
        // 如果前一次Coroutine還在跑，先停止
        if (introCoroutine != null)
        {
            StopCoroutine(introCoroutine);
        }
        Section_Ani.SetBool("OnPlay", true);
        // 停止目前語音播放
        audioSource.Stop();

        introCoroutine = StartCoroutine(PlayIntroCoroutine());


    }

    IEnumerator PlayIntroCoroutine()
    {
        audioSource.clip = introClip;
        audioSource.Play();

        // 等待語音播完
        yield return new WaitForSeconds(introClip.length);

        introCoroutine = null;
    }

    public void ContentText(int content)
    {
        content_Text.text = content_count[content];
    }


    public void StopAll()
    {
        // 中斷播放並重置狀態
        if (introCoroutine != null)
        {
            StopCoroutine(introCoroutine);
            introCoroutine = null;
        }
        Section_Ani.SetBool("OnPlay", false);

        audioSource.Stop();


        // 可加動畫重置等
        // Section2_Ani.Play("IdleState");
    }

    public GameObject EndObj;

    public void End()
    {
        EndObj.SetActive(true);
    }


    public void StartObj()
    {
        EndObj.SetActive(false);
    }

    public void PlayStepClip(int currentStep)
    {
        // 中斷語音直接播放下一段
        audioSource.Stop();
        audioSource.clip = stepClips[currentStep];
        audioSource.Play();
    }

    [Header("結束事件")]
    public UnityEvent Ani_Event;

    public void Ani_Event_End()
    {
        Ani_Event?.Invoke();
    }
}

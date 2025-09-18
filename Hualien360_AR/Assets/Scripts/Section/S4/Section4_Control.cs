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

public class Section4_Control : SectionBase
{
    public DT_FadeController dt_Fade;
    public DT_FadeController dt_AnotherSee_Fade;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayS4();
        }
    }

    public void GetOrange()
    {
        Section_Ani.SetBool("GoOrange", true);
    }

    public void GetGreen()
    {
        Section_Ani.SetBool("GoGreen", true);
    }

    public void ShowCanClick()
    {
        dt_Fade.RoanAlen_DoFadeIn();
    }

    public void AnotherSeeShow()
    {
        dt_AnotherSee_Fade.RoanAlen_DoFadeIn();
    }

    public void PlayS4()
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

        Section_Ani.SetBool("GoOrange", false);
        Section_Ani.SetBool("GoGreen", false);


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


    [Header("結束事件")]
    public UnityEvent Ani_Event;

    public void Ani_Event_End()
    {
        Ani_Event?.Invoke();
    }
}

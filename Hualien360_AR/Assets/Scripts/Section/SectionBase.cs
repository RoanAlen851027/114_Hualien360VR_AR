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
using UnityEngine.Events;


public class SectionBase : MonoBehaviour 
{
    [Header("語音")]
    public AudioSource audioSource;
    public AudioSource sfx_Source;
    public AudioClip introClip; //主音效

    public AudioClip[] stepClips;  // 階段音效
    public AudioClip[] sfx_Clip;  // 效果音效

    [Header("動畫")]
    public Animator Section_Ani;


    public Coroutine introCoroutine;

    [Header("內文")]
    public TextMeshProUGUI content_Text;
    public List<string> content_count;



}

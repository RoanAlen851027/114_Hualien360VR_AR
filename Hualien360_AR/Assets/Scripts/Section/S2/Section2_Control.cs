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
public class Section2_Control : MonoBehaviour
{
    [Header("語音")]
    public AudioSource audioSource;
    public AudioSource sfxSource;

    public AudioClip introClip;
    public AudioClip[] stepClips;  // 5段語音

    public AudioClip walkClip;
    [Header("動畫")]
    public Animator Section2_Ani;

    public int currentStep = 0;
    public bool canClick = false;

    private Coroutine introCoroutine;

    [Header("內文")]
    public TextMeshProUGUI content_Text;
    public List<string> content_count;

    public GameObject GoNextText;
    public DT_FadeController fadeController;

    [Header("結束事件")]
    public UnityEvent Ani_Event;

    public void Ani_Event_End()
    {
        Ani_Event?.Invoke();
    }
    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayS2();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            OnFootClicked();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            fadeController.RoanAlen_DoBreathingFade();
        }
    }

    public void ShowGoNext()
    {
        fadeController.RoanAlen_DoBreathingFade();
        GoNextText.SetActive(true);
    }

    public void CloseGoNext()
    {
        GoNextText.SetActive(false);
    }
    public void ContentText(int content)
    {
        content_Text.text = content_count[content];
    }


    public void PlayS2()
    {
        // 如果前一次Coroutine還在跑，先停止
        if (introCoroutine != null)
        {
            StopCoroutine(introCoroutine);
        }

        // 停止目前語音播放
        audioSource.Stop();
        Section2_Ani.SetBool("OnPlay",true);
        currentStep = 0;
        canClick = false;

        introCoroutine = StartCoroutine(PlayIntroCoroutine());
    }

    IEnumerator PlayIntroCoroutine()
    {
        audioSource.clip = introClip;
        audioSource.Play();

        // 等待語音播完
        yield return new WaitForSeconds(introClip.length);

        canClick = true;
        introCoroutine = null;
    }

    public void WalkSound()
    {
        sfxSource.clip = walkClip;
        sfxSource.Play();
    }

    public void StopAll()
    {
        // 中斷播放並重置狀態
        if (introCoroutine != null)
        {
            StopCoroutine(introCoroutine);
            introCoroutine = null;
        }

        audioSource.Stop();
        currentStep = 0;
        canClick = false;
        // 重置 Animator
        Section2_Ani.Rebind();
        Section2_Ani.Update(0f);
        GoNextText.SetActive(false);

        Section2_Ani.SetBool("OnPlay", false);
        Section2_Ani.SetBool("S2_2", false);
        Section2_Ani.SetBool("S2_3", false);
        Section2_Ani.SetBool("S2_4", false);
        Section2_Ani.SetBool("S2_5", false);
        Section2_Ani.SetBool("S2_6", false);
        // 可加動畫重置等
        // Section2_Ani.Play("IdleState");
    }

    public void OnFootClicked()
    {
        if (!canClick || currentStep >= stepClips.Length)
            return;

        // 中斷語音直接播放下一段
        audioSource.Stop();
        audioSource.clip = stepClips[currentStep];
        audioSource.Play();

        // 播放動畫 (依需求解開註解)
        // Section2_Ani.Play("MigrationStep" + currentStep);

        currentStep++;

        switch (currentStep)
        {
            case 1:
                Section2_Ani.SetBool("S2_2", true);
                Debug.Log("點擊後第一段");
                break;
            case 2:
                Debug.Log("點擊後第二段");
                Section2_Ani.SetBool("S2_3", true);
                break;
            case 3:
                Debug.Log("點擊後第三段");
                Section2_Ani.SetBool("S2_4", true); 
                break;
            case 4:
                Debug.Log("點擊後第四段");
                Section2_Ani.SetBool("S2_5", true);
                break;
            case 5:
                Debug.Log("點擊後第五段");
                Section2_Ani.SetBool("S2_6", true);
                break;
            default:
                break;
        }

        if (currentStep >= stepClips.Length)
        {
            canClick = false;
            Debug.Log("完整遷徙路線完成！");
        }
    }

    public void OnReturnKeyPressed()
    {
        Debug.Log("返回鍵被按下，停止Section2流程");

        // 停止所有播放與重置狀態
        StopAll();

        // 這裡你可以加跳轉場景或關閉UI的程式碼
        // 例如：
        // SceneManager.LoadScene("MainMenu");
        // 或
        // this.gameObject.SetActive(false);
    }


    public void CanClick()
    {
        canClick = true;
        GoNextText.SetActive(true);
        fadeController.RoanAlen_DoBreathingFade();

    }

    public void N_Click()
    {
        canClick = false;
        GoNextText.SetActive(false);
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
}


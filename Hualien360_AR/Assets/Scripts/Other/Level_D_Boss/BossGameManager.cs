using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using static UnityEngine.Rendering.BoolParameter;
/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
public class BossGameManager : MonoBehaviour
{
    [Header("啟用狀態")]
    public bool IsPlay;
    public float Timer=20;
    public GameObject SubTimer;
    public TextMeshProUGUI timer;
    public Image timer_Img;
    private float currentFill = 1f; // 用來追蹤目前 fillAmount
    private Tween fillTween;

    public BossClickGameControl Board_A;
    public BossClickGameControl Board_B;

    public GemValue gemValue;
    private Coroutine coroutine;
    public UnityEvent Correct_Event;
    public UnityEvent Worng_Event;

    private bool isJudging = false;

    public bool IsFailed;

    public int Correct_Count;


    public List<CanvasGroup> Boss_Eye_CanvasGroup;
    public Image EnergyBar_Img;
    public List<Sprite> energy_Bar;
    public Image EnergyGem;
    public List<Sprite> EnergyGem_Icon;


    public int Set_Count;
    public UnityEvent Fill_Event;
    public List<GameObject> FireBalls;
    public List<MoveFadeBoth_Tf> moveFadeBoth_Tfs;
    public List<MoveFadeBoth_Tf> board_MoveFade;

    public TextMeshProUGUI Boss_Talk_Text;

    [Header("挑戰失敗")]
    public MoveFadeBoth_Tf moveFade_Failed_Panel;
    public UnityEvent Failed_Event;
    private void Start()
    {
        EnergyBar_Img.DOFillAmount(Correct_Count, 3f).SetEase(Ease.OutCubic);
    }
    private void Update()
    {
        Attack_Judgment();

        if (IsPlay == true)
        {
            Timer -= Time.deltaTime;
            //float targetValue = Mathf.InverseLerp(0f, 20f, Timer);
            //timer_Img.fillAmount = Mathf.Clamp01(targetValue);

            // ❗用 Tween 來平滑過渡 fillAmount

            if (Timer < 0)
            {

                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
                    coroutine = null;
                }
                ResetSelect();
                Correct_Count = 0;
                float targetValue = Mathf.InverseLerp(0f, 3f, Correct_Count);
                EnergyBar_Img.DOFillAmount(targetValue, 1f).SetEase(Ease.OutCubic);
                Timer = 0;
                IsFailed = true;
                IsPlay = false;
                timer.text = "00";
                board_MoveFade[0].FadeOut();
                board_MoveFade[1].FadeOut();
                Debug.LogError("You Lose!");
                EnergyGem.sprite = EnergyGem_Icon[5];
                moveFade_Failed_Panel.FadeIn();
                FadeOutAllEyes();
                Failed_Event?.Invoke();
            }
            else
            {
                IsFailed = false;
                int displayTime = Mathf.CeilToInt(Timer); // 向上取整，確保不會提前顯示 00
                timer.text = displayTime.ToString("00");
                UpdateTimerBarSmoothly();
            }
        }
    }
    private void UpdateTimerBarSmoothly()
    {
        float targetValue = Mathf.InverseLerp(0f, 20f, Timer);
        if (fillTween != null && fillTween.IsActive()) fillTween.Kill();
        fillTween = timer_Img.DOFillAmount(Mathf.Clamp01(targetValue), 0.5f).SetEase(Ease.OutCubic);
    }
    public void StartPlayGame()
    {
        IsPlay = true;
        Timer = 20;
        UpdateTimerBarSmoothly();
    }

    public void TryAgain_Game()
    {
        ReTimer();
        Set_Count=0;
    }

    public void ReTimer()
    {
        IsPlay = false;
        Timer = 20;
        UpdateTimerBarSmoothly();
    }

    public void Attack_Judgment()
    {
        //這邊先判斷都有選擇
        if (!isJudging && Board_A.OnSelect && Board_B.OnSelect)
        {
            coroutine = StartCoroutine(WaitToJudgment());
        }
    }

    public void FadeOutAllEyes()
    {
        for (int i = 0; i < Boss_Eye_CanvasGroup.Count; i++)
        {
            FadeOutBossEye(i);
        }
    }
    private void FadeOutBossEye(int index)
    {
        if (index >= 0 && index < Boss_Eye_CanvasGroup.Count)
        {
            CanvasGroup cg = Boss_Eye_CanvasGroup[index];
            cg.DOFade(0f, 0.5f).SetEase(Ease.OutQuad);
            cg.interactable = false;
            cg.blocksRaycasts = false;
        }
    }
    public void ShowBossEye(int index)
    {
        if (index >= 0 && index < Boss_Eye_CanvasGroup.Count)
        {
            CanvasGroup cg = Boss_Eye_CanvasGroup[index];
            cg.DOFade(1f, 0.5f).SetEase(Ease.OutQuad);
            cg.interactable = true;
            cg.blocksRaycasts = true;
        }
    }
    public void SetPlayStatus(int status)
    {

        switch (status)
        {
            case 1:
                gemValue = GemValue.CP;
                EnergyGem.sprite = EnergyGem_Icon[0];
                EnergyBar_Img.sprite = energy_Bar[0];
                Boss_Talk_Text.text = "請蒐集榮耀藍鑽石，激勵魔王承擔起責任。";
                break;
            case 2:
                gemValue = GemValue.NP;
                EnergyGem.sprite = EnergyGem_Icon[1];
                EnergyBar_Img.sprite = energy_Bar[1];
                Boss_Talk_Text.text = "請蒐集關懷紅瑪瑙，給予魔王溫暖與關愛。";
                break;
            case 3:
                gemValue = GemValue.A;
                EnergyGem.sprite = EnergyGem_Icon[2];
                EnergyBar_Img.sprite = energy_Bar[2];
                Boss_Talk_Text.text = "請蒐集智慧綠翡翠，讓魔王可以冷靜思考。";
                break;
            case 4:
                gemValue = GemValue.FC;
                EnergyGem.sprite = EnergyGem_Icon[3];
                EnergyBar_Img.sprite = energy_Bar[3];
                Boss_Talk_Text.text = "請蒐集活力黃水晶，讓魔王能夠感受快樂。";
                break;
            case 5:
                gemValue = GemValue.AC;
                EnergyGem.sprite = EnergyGem_Icon[4];
                EnergyBar_Img.sprite = energy_Bar[4];
                Boss_Talk_Text.text = "請蒐集調和紫晶石，提高魔王的合作能力。";
                break;
            default:
                gemValue = GemValue.None;
                break;
        }
    }

    public IEnumerator WaitToJudgment()
    {
        
        isJudging = true;

        if (IsFailed)
        {
            ResetSelect();
            isJudging = false;
            if (IsFailed == true)
            {
                Correct_Count = 0;
                float targetValue = Mathf.InverseLerp(0f, 3f, Correct_Count);
                EnergyBar_Img.DOFillAmount(targetValue, 1f).SetEase(Ease.OutCubic);
            }
            yield break;
        }
        yield return new WaitForSeconds(0.1f);

        if (Board_A.gemValue == gemValue && Board_B.gemValue == gemValue)
        {
            if (Board_A.gemCount == Board_B.gemCount)
            {
                Debug.LogError("竟然選對ㄌ!");
                Board_A.Correct();
                Board_B.Correct();
                ResetSelect();
                Correct_Count++;
                float targetValue = Mathf.InverseLerp(0f, 3f, Correct_Count);
                EnergyBar_Img.DOFillAmount(targetValue, 1f).SetEase(Ease.OutCubic);
                if (Correct_Count >= 3)
                {
                    ReTimer();
                    timer.text = Timer.ToString("00");
                    Correct_Event?.Invoke();

                    yield return new WaitForSeconds(1f);
                    Correct_Count = 0;
                    ClearBar();
                    Debug.Log("換下一隻");
                }
   
            }
            else
            {
                if (Board_A.OnSelect == true && Board_B.OnSelect == true)
                {
                    Debug.LogError("屬性相同，但數量不同");
                    Timer -= 5;
                    ResetSelect();
                    SubTimer.SetActive(true);
                    UpdateTimerBarSmoothly();
                    Worng_Event?.Invoke();
                }
                if (Board_B.OnSelect == false)
                {
                    Board_B.ResetSelect();
                    coroutine = null;
                    isJudging = false;
                }

                if (Board_A.OnSelect == false)
                {
                    Board_A.ResetSelect();
                    coroutine = null;
                    isJudging = false;
                }
            }
        }
        else
        {
            if (Board_A.OnSelect==true && Board_B.OnSelect == true)
            {
                Debug.LogError("都沒中，可憐~");
                Timer -= 5;
                SubTimer.SetActive(true);
                ResetSelect();
                UpdateTimerBarSmoothly();
                Worng_Event?.Invoke();

            }

            if (Board_B.OnSelect == false)
            {
                Board_B.ResetSelect();
                coroutine = null;
                isJudging = false;
            }

            if (Board_A.OnSelect == false)
            {
                Board_A.ResetSelect();
                coroutine = null;
                isJudging = false;
            }
        }

       
    }

    private void ClearBar()
    {
        EnergyGem.sprite = EnergyGem_Icon[5];
        FireBalls[Set_Count].SetActive(true);
        EnergyBar_Img.DOFillAmount(0, 1f).SetEase(Ease.OutCubic);
        board_MoveFade[0].FadeOut();
        board_MoveFade[1].FadeOut();
        Set_Count++;
    }

    public void ShowPanel(int count)
    {
        Correct_Count = 0;
        float targetValue = Mathf.InverseLerp(0f, 3f, Correct_Count);
        EnergyBar_Img.DOFillAmount(targetValue, 1f).SetEase(Ease.OutCubic);
        moveFadeBoth_Tfs[count].FadeIn();
        // 先全部淡出
        for (int i = 0; i < Boss_Eye_CanvasGroup.Count; i++)
        {
            Boss_Eye_CanvasGroup[i].DOFade(0f, 0.3f).SetEase(Ease.OutQuad);
            Boss_Eye_CanvasGroup[i].interactable = false;
            Boss_Eye_CanvasGroup[i].blocksRaycasts = false;
        }
        //開眼睛
        ShowBossEye(count);



        //switch (count)
        //{
        //    case 0:
        //        ShowBossEye(0);
        //        break;
        //    case 1:
        //        ShowBossEye(1);
        //        break;
        //    case 2:
        //        ShowBossEye(2);
        //        break;
        //    case 3:
        //        ShowBossEye(3);
        //        break;
        //    case 4:
        //        ShowBossEye(4);
        //        break;
        //}
    }

    private void ResetSelect()
    {
        Board_A.ResetSelect();
        Board_B.ResetSelect();
        coroutine = null;
        isJudging = false;
    }

    
}

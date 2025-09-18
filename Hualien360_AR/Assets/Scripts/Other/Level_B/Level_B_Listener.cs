using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using TMPro;
using System.Collections.Generic;
/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
public class Level_B_Listener : MonoBehaviour
{
    public TalkToPACscore talkToPACscore;
    private Coroutine coroutine;
    private Coroutine coroutine_2;

    public GameObject Level_B_EquimentPanel;
    public UnityEvent Level_A_Event;
    public UnityEvent Level_B_Event;
    [Space(10)]
    public GameObject StarObj;
    public GameObject LevelUP_Obj;
    public ScaleFadeOut LevelUp_Effect;
    public GameObject Charater_Obj;

    [Header("第二關結算畫面")]
    public MoveFadeBoth_Tf moveFade;
    public GameObject State_2_EndPanel;
    public List<TextMeshProUGUI> Show_Add_Score;
    private void Start()
    {
        coroutine = StartCoroutine(waitToStart());
    }

    private void OnEnable()
    {
        coroutine = StartCoroutine(waitToStart());//測試用
    }


    IEnumerator waitToStart()
    {
        yield return new WaitForSeconds(0.25f);
        talkToPACscore.Get_State_2_Equiment();

    }

    public void ShowEnd()
    {
        coroutine_2 = StartCoroutine(waitToShowPanel());

        for (int i = 0; i < Show_Add_Score.Count; i++)
        {
            Show_Add_Score[i].text = $"+{talkToPACscore.pAC_Score.Wait_Add_score[i]}";
        }
    }

    IEnumerator waitToShowPanel()
    {
        Charater_Obj.SetActive(true);

        LevelUP_Obj.SetActive(true);
        yield return new WaitForSeconds(4f);
        LevelUp_Effect.FadeOut();
        yield return new WaitForSeconds(1f);
        LevelUP_Obj.SetActive(false);
        moveFade.enabled = true;
        State_2_EndPanel.SetActive(true);

    }
    public void GetEquiment()
    {

        //EquimentPanel.SetActive(true);
        Level_B_EquimentPanel.SetActive(true);
        Level_B_Event.Invoke();
        Level_A_Event?.Invoke();
        Debug.Log("開啟第二關裝備");
    }

    public void Level_B_End()
    {
        State_2_EndPanel.SetActive(false);
        Charater_Obj.SetActive(false);
        talkToPACscore.CloseEquimentPanel();
    }
}

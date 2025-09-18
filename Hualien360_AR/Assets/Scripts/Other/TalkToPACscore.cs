using TMPro;
using UnityEngine;
/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using UnityEngine.UI;
public class TalkToPACscore : MonoBehaviour
{
    public PAC_Score pAC_Score;
    [SerializeField]
    private InputField _InputField;

    [SerializeField]
    private GameObject tutorial_Manager;
    [SerializeField]
    private GameObject tutorial_Talk;
    [SerializeField]
    private GameObject LobbyGameObject;

    [Header("各關卡裝備")]
    public GameObject Equiment_Panel;
    public Level_A_Listener Level_A;
    public Level_B_Listener Level_B;
    public Level_C_Listener Level_C;

    private void Awake()
    {
      
        if (pAC_Score == null)
        {
            pAC_Score = FindFirstObjectByType<PAC_Score>();
            Debug.LogError("PAC_Score 找到！請保存於物件中。");

        }
        else
        {
            Debug.LogError("PAC_Score 物件未找到！請確保它存在於 DontDestroyOnLoad 的物件中。");

        }

    }

    public void GetLvB_WaitScore()
    {
        pAC_Score.GetLvB_WaitAddScore();
    }
    

    public void SetUserName(string Name)
    {
        if (_InputField != null)
        {
            pAC_Score.UserName = _InputField.text;
        }
        else
        {
            pAC_Score.UserName = Name;
        }
    }

    public void Get_State_1_Equiment()
    {
        if (pAC_Score.State_1_GetEquiment == false && pAC_Score.Level_state_1 == Level_State.Finish)
        {
            pAC_Score.State_1_GetEquiment = true;
            Equiment_Panel.SetActive(true);
            Level_A.GetEquiment();
            Debug.Log("結束取得身體裝備");
        }
    }

    public void Get_State_2_Equiment()
    {
        if (pAC_Score.State_2_GetEquiment == false && pAC_Score.Level_state_2 == Level_State.Finish)
        {
            pAC_Score.State_2_GetEquiment = true;
            Equiment_Panel.SetActive(true);
            Level_B.GetEquiment();
            Debug.Log("取得面罩");
        }
    }


    public void Get_State_3_Equiment()
    {
        if (pAC_Score.State_3_GetEquiment == false && pAC_Score.Level_state_3 == Level_State.Finish)
        {
            pAC_Score.State_3_GetEquiment = true;
            Equiment_Panel.SetActive(true);
            Level_C.GetEquiment();
            Debug.Log("取得武器");
        }
    }


    public void CloseEquimentPanel()
    {
        Equiment_Panel.SetActive(false);
    }


    private void LobbyControl( bool tutorialManager,bool tutorialTalk, bool lobby)
    {
        tutorial_Manager.SetActive(tutorialManager);
        tutorial_Talk.SetActive(tutorialTalk);
        LobbyGameObject.SetActive(lobby);
    }
    public void SetLobbyState(int State)
    {
        switch (State)
        {
            case 0:
                pAC_Score.LobbyContent = Lobby_State.None;
                break;
            case 1:
                pAC_Score.LobbyContent = Lobby_State.Introduction;
                pAC_Score.LobbyFirstTime();
                LobbyControl(true,false, false);
                break;
            case 2:
                pAC_Score.LobbyContent = Lobby_State.NoIntroduction;
                LobbyControl(false,false, true);
                break;
            case 3:
                pAC_Score.LobbyContent = Lobby_State.Finish;
                break;
            default:
                break;
        }
    }

    public void SendAddScore()
    {
        pAC_Score.Send_AddScore();
    }

    public void ClearAddScore()
    {
        pAC_Score.Clear_AddScore();
    }

    public void All_End()
    {
        pAC_Score.All_End=true;
    }

}

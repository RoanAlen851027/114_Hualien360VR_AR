using UnityEngine;
/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
public class Level_B_Complete : MonoBehaviour
{
    public TalkToPACscore pACscore;

    public void MissionComplete()
    {
        pACscore.pAC_Score.Level_state_2 = Level_State.Finish;
    }
}

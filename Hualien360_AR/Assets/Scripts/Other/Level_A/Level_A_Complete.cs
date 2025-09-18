using UnityEngine;
/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
public class Level_A_Complete : MonoBehaviour
{
    public TalkToPACscore pACscore;

    public void MissionComplete()
    {
        pACscore.pAC_Score.Level_state_1 = Level_State.Finish;


        for (int i = 0; i < pACscore.pAC_Score.Wait_Add_score.Count; i++)
        {
            pACscore.pAC_Score.Wait_Add_score[i] = 15;
            pACscore.pAC_Score.Reslut_score[i] += 15;
            if (pACscore.pAC_Score.Reslut_score[i]>=80)
            {
                pACscore.pAC_Score.Reslut_score[i] = 80;
            }

        }
    }
}

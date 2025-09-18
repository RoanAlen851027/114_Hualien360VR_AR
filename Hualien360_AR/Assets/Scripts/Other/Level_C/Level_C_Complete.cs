using UnityEngine;
/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
public class Level_C_Complete : MonoBehaviour
{
    public TalkToPACscore pACscore;

    public void MissionComplete()
    {
        for (int i = 0; i < pACscore.pAC_Score.Reslut_score.Count; i++)
        {
            pACscore.pAC_Score.Reslut_score[i] += 20;

            if (pACscore.pAC_Score.Reslut_score[i] >=80)
            {
                pACscore.pAC_Score.Reslut_score[i] = 80;
            }
            pACscore.pAC_Score.Wait_Add_score[i] = 20;
        }

        pACscore.pAC_Score.Level_state_3 = Level_State.Finish;
    }
}

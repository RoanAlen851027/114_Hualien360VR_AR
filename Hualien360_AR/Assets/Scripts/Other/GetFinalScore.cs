using UnityEngine;
/********************************
---------------------------------
著作者：RoanAlen
用途：直接取得結束分數
---------------------------------
*********************************/
public class GetFinalScore : MonoBehaviour
{
    private void Start()
    {
        if (PAC_Score.instance != null)
        {
            PAC_Score.instance.GetReultScore();
        }
    }

}

using UnityEngine;
/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;
public class Level_B_Backer : MonoBehaviour
{
    public int Back_Count;
    [SerializeField]
    private Button Backer_BTN;

    [SerializeField]
    private UnityEvent GoToLobbyEvent;
    [SerializeField]
    private UnityEvent GoToChoiceEvent;

    public List<GameObject> playGround;

    public void Backer_Control()
    {
       
        if (Back_Count == 0)
        {
            GoToLobbyEvent?.Invoke();
        }
        if (Back_Count == 1)
        {
            GoToChoiceEvent?.Invoke();
            for (int i = 0; i < playGround.Count; i++)
            {
                playGround[i].SetActive(false);
            }
        }
    }
    public void Set_BackCount(int back_Count)
    {
        Back_Count=back_Count;
    }
}

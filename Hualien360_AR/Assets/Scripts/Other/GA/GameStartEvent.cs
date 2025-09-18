using UnityEngine;
/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
public class GameStartEvent : MonoBehaviour
{
    public GASender GA_GameStart;

    void Start()
    {
        GA_GameStart.SendGA();
    }

}

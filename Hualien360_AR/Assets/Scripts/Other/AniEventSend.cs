using UnityEngine;
/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using UnityEngine.Events;
public class AniEventSend : MonoBehaviour
{
    public UnityEvent AniEvent;

    public void Ani_Event()
    {
        AniEvent?.Invoke();
    }
}

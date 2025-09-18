/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;
public class ContentViewController : MonoBehaviour
{
    public UnityEvent CVC_Event;

    public List<MoveFadeBoth_Tf> moveFadeBoth_Tfs;
    public List<TypingEffect> typingEffects;

    public void CloseContent()
    {
        for (int i = 0; i < moveFadeBoth_Tfs.Count; i++)
        {
            moveFadeBoth_Tfs[i].FadeOut();
        }
        for (int j = 0; j < typingEffects.Count; j++)
        {
            typingEffects[j].ClearText();
        }
    }
 
}

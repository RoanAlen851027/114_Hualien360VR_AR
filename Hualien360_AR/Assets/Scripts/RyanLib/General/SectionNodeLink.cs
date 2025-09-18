/*************************************************
  * 名稱：SectionNodeLink
  * 作者：RyanHsu
  * 功能說明：SectionNode的訊號延申
  * ***********************************************/
using UnityEngine;
using UnityEngine.Events;

public class SectionNodeLink : MonoBehaviour
{
    public UnityEvent onStart;
    public UnityEvent onFadeOut;
    public UnityEvent onCompleted;

    public void FadeOut()
    {
        if (onFadeOut != null) onFadeOut.Invoke();
    }
}

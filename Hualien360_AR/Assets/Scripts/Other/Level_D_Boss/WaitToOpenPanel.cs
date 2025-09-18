using UnityEngine;
/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using System.Collections;
using UnityEngine.Events;
using TMPro;
public class WaitToOpenPanel : MonoBehaviour
{

    public UnityEvent WaitToEvent;
    private Coroutine coroutine;
    public TextMeshProUGUI Content;

    public void WaitToOpen()
    {
        coroutine = StartCoroutine(WaitInvokeEvent());
    }

    private IEnumerator WaitInvokeEvent()
    {
        Content.text = "";
        yield return new WaitForSeconds(1f);
        WaitToEvent?.Invoke();
    }
}

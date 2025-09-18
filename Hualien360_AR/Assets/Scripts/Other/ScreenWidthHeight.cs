using UnityEngine;
/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using UnityEngine.Events;
public class ScreenWidthHeight : MonoBehaviour
{

    public bool OnPortrait_Invoke;
    public bool OnLandscape_Invoke;

    public UnityEvent OnPortrait_Event;
    public UnityEvent OnLandscape_Event;
    private void Update()
    {
        if (Screen.width > Screen.height)
        {
            OnPortrait_Invoke = false;
            OnLandscape();
        }
        else
        {
            OnLandscape_Invoke = false;
            OnPortraitMode();
        }
    }

    private void OnLandscape()
    {
        if (OnLandscape_Invoke == false)
        {
            OnLandscape_Invoke = true;
            OnLandscape_Event?.Invoke();
        }
    }

    private void OnPortraitMode()
    {
        if (OnPortrait_Invoke == false)
        {
            OnPortrait_Invoke = true;
            OnPortrait_Event?.Invoke();
        }
    }
    public void DebugOnPortrait()
    {
        Debug.LogError("我現在直的 執行喔");
    }
}

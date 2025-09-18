/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class RedirectButton : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void RedirectToHappinessvillage();

    public Button yourButton;

    private void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        yourButton.onClick.AddListener(() => {
            RedirectToHappinessvillage();
        });
#endif
    }
}

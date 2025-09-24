/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using UnityEngine;
using System;
using System.Collections.Generic;

public class ScanShowHide : MonoBehaviour
{

    public bool ShowHideScan;

    public List<GameObject> Control_Object;

    public GameObject ShowAniObject;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ShowScanAni();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            HideScanAni_ShowDragon();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            HideScanAni_ShowBird();
        }
    }
    public void ShowScanAni()
    {
        ShowHideScan = true;
        Control_Object[0].SetActive(true);
        Control_Object[1].SetActive(false);
        Control_Object[2].SetActive(false);
        ShowAniObject.SetActive(false);
    }

    public void HideScanAni_ShowDragon()
    {
        ShowHideScan = false;
        Control_Object[0].SetActive(false);
        Control_Object[1].SetActive(true);
        Control_Object[2].SetActive(false);
        ShowAniObject.SetActive(true);


    }

    public void HideScanAni_ShowBird()
    {
        ShowHideScan = false;
        Control_Object[0].SetActive(false);
        Control_Object[1].SetActive(false);
        Control_Object[2].SetActive(true);
        ShowAniObject.SetActive(true);

    }
}

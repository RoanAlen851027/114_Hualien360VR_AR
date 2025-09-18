/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

public class ScannerControl : MonoBehaviour
{

    public List<GameObject> ScannerObject;

    public GameObject HideA;
    public GameObject HideB;
    public GameObject HideC;   
    public GameObject HideD;

    public Coroutine coroutine;

    public List<bool> All_Ani;
    public List<GameObject> collect_Object;

    [Header("結尾")]
    public int HiddenCount;
    public UnityEvent Show_HiddenPoint;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            AddHideObjects();
        }
    }

    public void GetCollect(int step)
    {
        if (step < 0 || step >= All_Ani.Count) return; // 防呆

        All_Ani[step] = true;
        collect_Object[step].SetActive(true);

        if (All_Ani.All(x => x))  // ✅ 全部 true 才成立
        {
            AddHideObjects();
            HiddenCount++;
            if (HiddenCount==1)
            {
                Show_HiddenPoint?.Invoke();
            }

        }
    }

    public void OpenScanObj()
    {

        if (coroutine==null)
        {
            coroutine = StartCoroutine(OpenScanViewObj());
        }
    }

    IEnumerator OpenScanViewObj()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < ScannerObject.Count; i++)
        {
            ScannerObject[i].SetActive(true);
        }
        coroutine = null;
    }


    public void CloseScanObj()
    {
        for (int i = 0; i < ScannerObject.Count; i++)
        {
            ScannerObject[i].SetActive(false);
        }
    }

    public void AddHideObjects()
    {
        if (HideA != null && !ScannerObject.Contains(HideA))
            ScannerObject.Add(HideA);

        if (HideB != null && !ScannerObject.Contains(HideB))
            ScannerObject.Add(HideB);

        if (HideC != null && !ScannerObject.Contains(HideC))
            ScannerObject.Add(HideC);

        if (HideD != null && !ScannerObject.Contains(HideD))
            ScannerObject.Add(HideD);
    }


}

/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using UnityEngine;

public class JSReceiver : MonoBehaviour
{
    public GameObject appleObj;
    public GameObject bananaObj;
    public GameObject cherryObj;
    public GameObject dragonfruitObj;
    public GameObject eggplantObj;
    public GameObject figObj;
    public GameObject grapeObj;
    public GameObject honeydewObj;

    public void ReceiveFromJS(string qrName)
    {
        Debug.Log("收到來自 JS 的 QR Code 名稱：" + qrName);

        // 先全部隱藏
        HideAll();

        // 根據 QR 名稱顯示對應物件
        switch (qrName)
        {
            case "QRCode_Apple":
                appleObj.SetActive(true);
                break;
            case "QRCode_Banana":
                bananaObj.SetActive(true);
                break;
            case "QRCode_Cherry":
                cherryObj.SetActive(true);
                break;
            case "QRCode_Dragonfruit":
                dragonfruitObj.SetActive(true);
                break;
            case "QRCode_Eggplant":
                eggplantObj.SetActive(true);
                break;
            case "QRCode_Fig":
                figObj.SetActive(true);
                break;
            case "QRCode_Grape":
                grapeObj.SetActive(true);
                break;
            case "QRCode_Honeydew":
                honeydewObj.SetActive(true);
                break;
            default:
                Debug.LogWarning("未定義的 QR 名稱：" + qrName);
                break;
        }
    }

    private void HideAll()
    {
        appleObj.SetActive(false);
        bananaObj.SetActive(false);
        cherryObj.SetActive(false);
        dragonfruitObj.SetActive(false);
        eggplantObj.SetActive(false);
        figObj.SetActive(false);
        grapeObj.SetActive(false);
        honeydewObj.SetActive(false);
    }
}


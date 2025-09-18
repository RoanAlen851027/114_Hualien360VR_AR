using UnityEngine;
/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using UnityEngine.UI;
public class CharaterChoice : MonoBehaviour
{

    public CharaterProfession charater_Select;

    private void Update()
    {
        if (this.gameObject.transform.position.x==0 && this.gameObject.transform.position.z==0)
        {
            SelectCharater();
        }
    }

    public void SelectCharater()
    {
        if (PAC_Score.instance != null)
        {
           PAC_Score.instance.charaterProfession = charater_Select;
        }
        else
        {
            Debug.LogError("PAC_Score instance 不存在，無法設定 charaterProfession！");
        }
    }
}

/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using UnityEngine;
using TMPro;
using System.Collections.Generic;
public class CharaterSelected : MonoBehaviour
{
    public CharaterProfession charaterSeleted;

    public List<GameObject> charaterSkin; //角色造型
    public List<GameObject> charaterHelmet; //角色帽子
    public List<GameObject> charaterWeapon; //角色武器

    [Header("介紹會用到的地方")]

    public List<GameObject> CharaterProfessionText;
    public List <GameObject> CharaterContentBG;
    public List <GameObject> CharaterEquimentBG;
    // 0 角色職業   1 角色介紹標題  2 角色介紹  3 角色裝備標題 4 角色裝備介紹
    //public TextMeshProUGUI charaterInfo; // 角色資訊
    

    //public TextMeshProUGUI charaterProfession;//角色職業
    //public TextMeshProUGUI charaterContentTitle; //角色介紹標題
    //public TextMeshProUGUI charaterContent; //角色介紹
    //public TextMeshProUGUI charaterEquimentTitle; // 角色裝備標題
    //public TextMeshProUGUI charaterEquimentContent; // 角色裝備介紹



    //在介紹的時候 把對應的角色打開來
    public void OnIntroduction()
    {
        if (PAC_Score.instance != null)
        {
            switch (PAC_Score.instance.charaterProfession)
            {
                case CharaterProfession.None:
                    break;
                case CharaterProfession.Conductor:
    
                    Introdution(0);
                    break;
                case CharaterProfession.Picketer:
                    Introdution(1);
                    break;
                case CharaterProfession.Enginer:
                    Introdution(2);
                    break;
                case CharaterProfession.Walker:
                    Introdution(3);
                    break;
                case CharaterProfession.Creater:
                    Introdution(4);
                    break;
                case CharaterProfession.Doctor:
                    Introdution(5);
                    break;
                case CharaterProfession.Strategist:
                    Introdution(6);
                    break;
            }
        }
        else
        {
            Debug.Log("沒有找到 PAC_Score");
        }
    }

    /// <summary>
    /// 角色介紹
    /// </summary>
    /// <param name="index"></param>
    private void Introdution(int index )
    {
        charaterSkin[index].gameObject.SetActive(true);
        charaterHelmet[index].gameObject.SetActive(true);
        CharaterProfessionText[index].gameObject.SetActive(true);
        CharaterContentBG[index].gameObject.SetActive(true);
        CharaterEquimentBG[index].gameObject.SetActive(true);

    }


    //開始遊玩的時候預設
    public void Finished_1_State_GetEquiment()
    {
        if (PAC_Score.instance != null)
        {
            switch (PAC_Score.instance.charaterProfession)
            {
                case CharaterProfession.Conductor:
                    charaterSkin[0].SetActive(true);
                    break;
                case CharaterProfession.Picketer:
                    charaterSkin[1].SetActive(true);
                    break;
                case CharaterProfession.Enginer:
                    charaterSkin[2].SetActive(true);
                    break;
                case CharaterProfession.Walker:
                    charaterSkin[3].SetActive(true);
                    break;
                case CharaterProfession.Creater:
                    charaterSkin[4].SetActive(true);
                    break;
                case CharaterProfession.Doctor:
                    charaterSkin[5].SetActive(true);
                    break;
                case CharaterProfession.Strategist:
                    charaterSkin[6].SetActive(true);
                    break;

            }
        }
    }


    //開始遊玩的時候預設
    public void Finished_2_State_GetEquiment()
    {
        if (PAC_Score.instance != null)
        {
            switch (PAC_Score.instance.charaterProfession)
            {
                case CharaterProfession.Conductor:
                    charaterHelmet[0].SetActive(true);
                    break;
                case CharaterProfession.Picketer:
                    charaterHelmet[1].SetActive(true);
                    break;
                case CharaterProfession.Enginer:
                    charaterHelmet[2].SetActive(true);
                    break;
                case CharaterProfession.Walker:
                    charaterHelmet[3].SetActive(true);
                    break;
                case CharaterProfession.Creater:
                    charaterHelmet[4].SetActive(true);
                    break;
                case CharaterProfession.Doctor:
                    charaterHelmet[5].SetActive(true);
                    break;
                case CharaterProfession.Strategist:
                    charaterHelmet[6].SetActive(true);
                    break;

            }
        }
    }


    //開始遊玩的時候預設
    public void Finished_3_State_GetEquiment()
    {
        if (PAC_Score.instance != null)
        {
            switch (PAC_Score.instance.charaterProfession)
            {
                case CharaterProfession.Conductor:
                    charaterWeapon[0].SetActive(true);
                    break;
                case CharaterProfession.Picketer:
                    charaterWeapon[1].SetActive(true);
                    break;
                case CharaterProfession.Enginer:
                    charaterWeapon[2].SetActive(true);
                    break;
                case CharaterProfession.Walker:
                    charaterWeapon[3].SetActive(true);
                    break;
                case CharaterProfession.Creater:
                    charaterWeapon[4].SetActive(true);
                    break;
                case CharaterProfession.Doctor:
                    charaterWeapon[5].SetActive(true);
                    break;
                case CharaterProfession.Strategist:
                    charaterWeapon[6].SetActive(true);
                    break;

            }
        }
    }
}

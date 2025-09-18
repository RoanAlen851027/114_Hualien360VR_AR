using UnityEngine;
/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
public class CharaterSelectEvent : MonoBehaviour
{
    public GASender GA_Charater_mt;
    public GASender GA_Charater_v;
    public GASender GA_Charater_m;
    public GASender GA_Charater_none;
    public GASender GA_Charater_n;
    public GASender GA_Charater_nb;
    public GASender GA_Charater_w;


    public void GA_CharaterSelect()
    {
        switch (PAC_Score.instance.charaterProfession)
        {
   
            case CharaterProfession.Conductor:
                //指揮官 山形
                GA_Charater_mt.SendGA();
                break;
            case CharaterProfession.Picketer:
                //糾察官 V型
                GA_Charater_v.SendGA();
                break;
            case CharaterProfession.Enginer:
                //工程師 M型
                GA_Charater_m.SendGA();
                break;
            case CharaterProfession.Walker:
                //漫遊者 漫遊
                GA_Charater_none.SendGA();
                break;
            case CharaterProfession.Creater:
                //創造家 反N型
                GA_Charater_nb.SendGA();
                break;
            case CharaterProfession.Doctor:
                //治療官 N型
                GA_Charater_n.SendGA();
                break;
            case CharaterProfession.Strategist:
                //策略家 W型
                GA_Charater_w.SendGA();
                break;
        }
    }

}

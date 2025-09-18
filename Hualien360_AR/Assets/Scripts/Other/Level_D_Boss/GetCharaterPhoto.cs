using System.Collections.Generic;
using UnityEngine;
/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using UnityEngine.UI;
public class GetCharaterPhoto : MonoBehaviour
{
    public TalkToPACscore pACscore;
    public Image playerPhoto;

    public List<Sprite> charaterPhotos;

    private void Start()
    {
        switch (pACscore.pAC_Score.charaterProfession)
        {
            case CharaterProfession.Conductor:
                //山
                playerPhoto.sprite = charaterPhotos[0];
                break;
            case CharaterProfession.Picketer:
                //V
                playerPhoto.sprite = charaterPhotos[1];
                break;
            case CharaterProfession.Enginer:
                //M
                playerPhoto.sprite = charaterPhotos[2];
                break;

            case CharaterProfession.Walker:
                //未知
                playerPhoto.sprite = charaterPhotos[3];
                break;
            case CharaterProfession.Creater:
                //反N
                playerPhoto.sprite = charaterPhotos[4];
                break;
            case CharaterProfession.Doctor:
                //N
                playerPhoto.sprite = charaterPhotos[5];
                break;
            case CharaterProfession.Strategist:
                //W
                playerPhoto.sprite = charaterPhotos[6];
                break;
            default:
                break;
        }
    }

}

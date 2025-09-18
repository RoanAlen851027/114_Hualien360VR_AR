using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
public class BossAttributeTile : MonoBehaviour
{
    [Header("寶石屬性")]
    public GemValue gemValue;

    public int row;
    public int col;
    public string attribute;
    public bool IsLink;
    public BossClickGameControl bossClickGameControl;
    public BreathingScale breathingScale;

    public ScaleFadeIn scaleFade_in;
    public ScaleFadeOut scaleFade_out;
    public Image Origin_BTN_Img;
    public List<Sprite> Change_BTN_Img;

    public GameObject OutLineObject;

    public Button button;




    private void Start()
    {
        //if (Btn_Img.TryGetComponent(out Image image))
        //{

        //}
    }

    public void OnClick()
    {
        bossClickGameControl.CheckConnectedAttribute(row, col);
    }

    public void SetAttribute(string attr, int r, int c)
    {
        attribute = attr;
        row = r;
        col = c;
        IsLink = false; // 初始時，設為 false
        bossClickGameControl.ResetSelect();
        breathingScale.Turn(IsLink);

        //CP NP A FC AC

        switch (attr)
        {
            case "CP":
                Origin_BTN_Img.sprite = Change_BTN_Img[0];
                gemValue = GemValue.CP;
                break;
            case "NP":
                Origin_BTN_Img.sprite = Change_BTN_Img[1];
                gemValue = GemValue.NP;
                break;
            case "A":
                Origin_BTN_Img.sprite = Change_BTN_Img[2];
                gemValue = GemValue.A;
                break;
            case "FC":
                Origin_BTN_Img.sprite = Change_BTN_Img[3];
                gemValue = GemValue.FC;
                break;
            case "AC":
                Origin_BTN_Img.sprite = Change_BTN_Img[4];
                gemValue = GemValue.AC;
                break;
        }


        //GetComponentInChildren<Text>().text = attr;
        // 在這裡加入 log 來檢查屬性是否正確設置
        //Debug.Log($"Setting attribute {attribute} at position ({row}, {col})");
    }

    public void IsSelect()
    {
        breathingScale.Turn(IsLink);
        OutLineObject.SetActive(IsLink);


    }

    public void IsCorrect()
    {
        breathingScale.Turn(false);
        scaleFade_out.FadeOut();
        OutLineObject.SetActive(false);
        button.enabled = false;

    }

    public void IsFadeIn()
    {
        scaleFade_in.FadeIn();
        OutLineObject.SetActive(false);
        button.enabled = true;


    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (Origin_BTN_Img == null)
        {
            Origin_BTN_Img = this.gameObject.transform.GetChild(2).gameObject.GetComponent<Image>();
        }
        if (scaleFade_out == null)
        {
            scaleFade_out = GetComponent<ScaleFadeOut>();
        }

        if (scaleFade_in == null)
        {
            scaleFade_in = GetComponent<ScaleFadeIn>();
        }

        if (breathingScale == null)
        {
            breathingScale = this.gameObject.transform.GetChild(2).gameObject.GetComponent<BreathingScale>();
        }

        if (OutLineObject == null)
        {
            OutLineObject = this.gameObject.transform.GetChild(1).gameObject;
        }

        if (button == null)
        {
            button = this.gameObject.GetComponent<Button>();   
        }
    }

#endif

}

/*************************************************
  * 名稱：LandingManager
  * 作者：RyanHsu
  * 功能說明：
  * ***********************************************/
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


#if UNITY_EDITOR
//[ExecuteAlways]
#endif
//[DisallowMultipleComponent]
//[RequireComponent(typeof CLASS)]
public class LandingManager : LandingBase
{
    //DispatchEvent dispatcher = DispatchEvent.GetInstance();
    [Header("Depend")]
    public TMP_InputField nameText;


    void Start()
    {
        if (AudioManager.Statuses == enum_AudioStatuses.none) bgm.BGM();
        this.Invoke(() => sectionManager.SectionStart(), 0.5f);
    }

    public void CheckName()
    {
        //if (db.userName != "") nameText.text = db.userName;
    }

#if UNITY_EDITOR
    //void OnValidate() { }
    //void OnDrawGizmos() { }
    //void OnDrawGizmosSelected() { }
#endif

}


public abstract class LandingBase : MonoBehaviour
{
    [Header("Setting")]
    public GameObject[] sectionGos;

    [Header("Depend")]
    public SectionManager sectionManager;
    public AudioPlayScriptable bgm;
}
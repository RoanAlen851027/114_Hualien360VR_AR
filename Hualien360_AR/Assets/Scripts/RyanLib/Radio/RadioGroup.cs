/*************************************************
  * 名稱：RadioGroup
  * 作者：RyanHsu
  * 功能說明：
  * ***********************************************/
using UnityEngine;
using System.Collections.Generic;


[ExecuteAlways]
public class RadioGroup : MonoBehaviour
{
    [DisplayOnly] public List<GameObjectRadio> radios = new List<GameObjectRadio>();

    // 註冊 Radio 到 Group
    public void RegisterRadio(GameObjectRadio radio)
    {
        if (!radios.Contains(radio))
        {
            radios.Add(radio);
        }
    }

    private void OnEnable()
    {
        CleanNullEntries();
    }

    /// <summary>
    /// 當某個Radio被選中時處理
    /// </summary>
    public void OnRadioSelected(GameObjectRadio selectedRadio)
    {
        foreach (GameObjectRadio radio in radios)
        {
            if (radio != selectedRadio)
            {
                radio.SetActive(false); // 關閉其他的Radio
            }
        }
    }

    /// <summary>
    /// 隱藏全部
    /// </summary>
    public void HideAll()
    {
        radios.ForEach(m => m.SetActive(false));
    }

    /// <summary>
    /// 清理 null 項目
    /// </summary>
    private void CleanNullEntries()
    {
        radios.RemoveAll(radio => radio == null);
    }

    private void OnValidate()
    {
        CleanNullEntries();
    }
}

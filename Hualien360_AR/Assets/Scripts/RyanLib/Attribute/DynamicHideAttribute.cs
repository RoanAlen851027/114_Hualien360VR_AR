/*************************************************
  * 名稱：DynamicHideAttribute
  * 作者：RyanHsu
  * 功能說明：從 Inspector 中以反射bool類別控制HideInInspector
  * ***********************************************/

using UnityEngine;

public class DynamicHideAttribute : PropertyAttribute
{
    public string dynamicSourceField;       // 檢查的變數名稱
    public bool IsExternalSource;           // 是否是外部變數
    public bool HideInInspector;            // 是否隱藏

    public DynamicHideAttribute(string dynamicSourceField, bool isExternalSource = false, bool hideInInspector = true)
    {
        this.dynamicSourceField = dynamicSourceField;
        this.IsExternalSource = isExternalSource;
        this.HideInInspector = hideInInspector;
    }
}

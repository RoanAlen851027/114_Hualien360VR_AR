/*************************************************
  * 名稱：Header
  * 作者：RyanHsu
  * 功能說明：
  * ***********************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[DisallowMultipleComponent]
public class Header : MonoBehaviour
{
    public bool m_allwaysShow = true;
    public string m_Header;
    public int m_FontSize = 18;
    public Vector3 m_Offset;
    public Color32 m_Color = Color.black;

    public void SetHeader(string str) => m_Header = str;

#if UNITY_EDITOR
    //void OnValidate() { }
    void OnDrawGizmos()
    {
        if (!m_allwaysShow) return;
        DrawGizmos();
    }
    void OnDrawGizmosSelected()
    {
        if (m_allwaysShow) return;
        DrawGizmos();
    }
    void DrawGizmos()
    {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = m_Color;
        style.fontSize = m_FontSize;
        Handles.Label(transform.position + m_Offset, m_Header, style);
    }

#endif

}
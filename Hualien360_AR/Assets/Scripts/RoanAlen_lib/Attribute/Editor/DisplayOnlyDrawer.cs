/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace RoanAlen.Attribute
{
    [CustomPropertyDrawer(typeof(DisplayOnlyAttribute))]
    public class DisplayOnlyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false; // 不可編輯
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;  // 還原編輯狀態，避免影響其他欄位
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }
    }
}
#endif

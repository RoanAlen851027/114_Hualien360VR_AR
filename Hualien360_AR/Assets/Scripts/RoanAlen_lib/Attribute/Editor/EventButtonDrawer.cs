/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using RoanAlen.Attribute;

[CustomPropertyDrawer(typeof(EventButtonAttribute))]
public class EventButtonDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EventButtonAttribute buttonAttr = (EventButtonAttribute)attribute;
        string buttonLabel = buttonAttr.ButtonLabel;

        EditorGUI.BeginProperty(position, label, property);

        // 畫按鈕
        if (GUI.Button(position, buttonLabel))
        {
            property.boolValue = !property.boolValue;
        }

        EditorGUI.EndProperty();
    }
}
#endif

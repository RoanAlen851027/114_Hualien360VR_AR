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

[CustomPropertyDrawer(typeof(BoolToggleButton_Attribute))]
public class BoolToggleButtonAttribute_Drawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        BoolToggleButton_Attribute toggleButton = (BoolToggleButton_Attribute)attribute;

        if (property.propertyType != SerializedPropertyType.Boolean)
        {
            EditorGUI.LabelField(position, label.text, "僅能套用在 bool 屬性");
            return;
        }

        string buttonLabel = property.boolValue ? toggleButton.TrueLabel : toggleButton.FalseLabel;

        if (GUI.Button(position, buttonLabel))
        {
            property.boolValue = !property.boolValue;
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight;
    }
}
#endif

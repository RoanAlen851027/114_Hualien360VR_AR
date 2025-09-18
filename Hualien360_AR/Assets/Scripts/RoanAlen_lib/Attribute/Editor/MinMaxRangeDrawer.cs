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
    [CustomPropertyDrawer(typeof(MinMaxRangeAttribute))]
    public class MinMaxRangeDrawer : PropertyDrawer
    {  
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            MinMaxRangeAttribute range = (MinMaxRangeAttribute)attribute;

            SerializedProperty minProp = property.FindPropertyRelative("min");
            SerializedProperty maxProp = property.FindPropertyRelative("max");

            EditorGUI.BeginProperty(position, label, property);

            // 保存原本縮排
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // Label 寬度
            float labelWidth = EditorGUIUtility.labelWidth;
            float fieldWidth = 50f; // 左右輸入框寬度
            float spacing = 5f;

            // 計算滑塊區域的寬度
            float sliderWidth = position.width - labelWidth - fieldWidth - spacing * 3;

            // 分區
            Rect labelRect = new Rect(position.x, position.y, labelWidth, position.height);
            Rect leftField = new Rect(labelRect.xMax-50, position.y, fieldWidth, position.height); // 左側額外的輸入框
            Rect sliderRect = new Rect(leftField.xMax + spacing, position.y, sliderWidth, position.height); // 滑桿區域
            Rect rightField = new Rect(sliderRect.xMax+5 + spacing, position.y, fieldWidth, position.height); // 右側輸入框

            EditorGUI.LabelField(labelRect, label);

            if (minProp.propertyType == SerializedPropertyType.Float && maxProp.propertyType == SerializedPropertyType.Float)
            {
                float minVal = minProp.floatValue;
                float maxVal = maxProp.floatValue;

                // 左邊的額外輸入框
                minVal = EditorGUI.FloatField(leftField, minVal);

                // 顯示滑桿
                EditorGUI.MinMaxSlider(sliderRect, ref minVal, ref maxVal, range.Min, range.Max);

                // 右邊的輸入框
                maxVal = EditorGUI.FloatField(rightField, maxVal);

                // 保持精度
                minVal = Mathf.Round(minVal * 100f) / 100f;
                maxVal = Mathf.Round(maxVal * 100f) / 100f;

                minProp.floatValue = Mathf.Clamp(minVal, range.Min, range.Max);
                maxProp.floatValue = Mathf.Clamp(maxVal, range.Min, range.Max);
            }
            else if (minProp.propertyType == SerializedPropertyType.Integer && maxProp.propertyType == SerializedPropertyType.Integer)
            {
                float minVal = minProp.intValue;
                float maxVal = maxProp.intValue;

                // 左邊的額外輸入框
                minVal = EditorGUI.IntField(leftField, (int)minVal);

                // 顯示滑桿
                EditorGUI.MinMaxSlider(sliderRect, ref minVal, ref maxVal, range.Min, range.Max);

                // 右邊的輸入框
                maxVal = EditorGUI.IntField(rightField, (int)maxVal);

                minProp.intValue = Mathf.Clamp(Mathf.RoundToInt(minVal), Mathf.RoundToInt(range.Min), Mathf.RoundToInt(range.Max));
                maxProp.intValue = Mathf.Clamp(Mathf.RoundToInt(maxVal), Mathf.RoundToInt(range.Min), Mathf.RoundToInt(range.Max));
            }
            else
            {
                EditorGUI.LabelField(position, "Unsupported type for MinMaxRange");
            }

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}
#endif

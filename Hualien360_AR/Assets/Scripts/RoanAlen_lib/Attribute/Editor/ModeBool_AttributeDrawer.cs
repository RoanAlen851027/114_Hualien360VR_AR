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
    [CustomPropertyDrawer(typeof(ModeBoolAttribute))]
    public class ModeBoolAttribute_Drawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ModeBoolAttribute modeAttr = (ModeBoolAttribute)attribute;
            var labels = modeAttr.Labels;

            if (property.propertyType != SerializedPropertyType.Generic)
            {
                EditorGUI.LabelField(position, label.text, "ModeBool only supports struct with bool fields.");
                return;
            }

            property.serializedObject.Update();

            Rect labelRect = new Rect(position.x, position.y, EditorGUIUtility.labelWidth, position.height);
            EditorGUI.LabelField(labelRect, label);

            int count = labels.Length;
            if (count == 0)
            {
                EditorGUI.LabelField(position, label.text, "ModeBool Labels is empty.");
                return;
            }

            float btnWidth = (position.width - EditorGUIUtility.labelWidth) / count;
            float x = position.x + EditorGUIUtility.labelWidth;
            float y = position.y;
            float height = position.height;

            SerializedProperty[] boolFields = new SerializedProperty[count];
            for (int i = 0; i < count; i++)
            {
                boolFields[i] = property.FindPropertyRelative(labels[i]);
                if (boolFields[i] == null)
                {
                    EditorGUI.LabelField(position, label.text, $"Field {labels[i]} not found.");
                    return;
                }
            }

            // 找出目前哪個是 true (選中的)
            int selectedIndex = -1;
            for (int i = 0; i < count; i++)
            {
                if (boolFields[i].boolValue)
                {
                    selectedIndex = i;
                    break;
                }
            }

            for (int i = 0; i < count; i++)
            {
                bool isSelected = (i == selectedIndex);

                Rect btnRect = new Rect(x + i * btnWidth, y, btnWidth - 4, height);

                // 用內建的 Toggle Button 樣式
                bool newVal = GUI.Toggle(btnRect, isSelected, labels[i], "Button");

                if (newVal != isSelected)
                {
                    if (newVal)
                    {
                        // 點擊成 true，其他全部設 false
                        for (int j = 0; j < count; j++)
                            boolFields[j].boolValue = (j == i);
                    }
                    else
                    {
                        // 點擊取消選擇 (變 false)
                        boolFields[i].boolValue = false;
                    }
                }
            }

            property.serializedObject.ApplyModifiedProperties();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}
#endif

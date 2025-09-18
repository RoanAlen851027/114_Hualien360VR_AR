using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(DynamicHideAttribute))]
public class DynamicHidePropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        DynamicHideAttribute condHAtt = (DynamicHideAttribute)attribute;

        // 獲取目標物件
        Object targetObject = property.serializedObject.targetObject;
        bool shouldBeHidden = false;

        if (condHAtt.IsExternalSource)
        {
            // 使用反射從外部獲取變數值
            var externalField = targetObject.GetType().GetField(condHAtt.dynamicSourceField);
            if (externalField != null)
            {
                shouldBeHidden = !(bool)externalField.GetValue(targetObject);
            } else
            {
                Debug.LogError($"[DynamicHide] 無法找到外部變數 '{condHAtt.dynamicSourceField}'");
            }
        } else
        {
            // 使用序列化屬性獲取當前物件內部變數值
            SerializedProperty sourcePropertyValue = property.serializedObject.FindProperty(condHAtt.dynamicSourceField);
            if (sourcePropertyValue != null)
            {
                shouldBeHidden = !sourcePropertyValue.boolValue;
            } else
            {
                Debug.LogError($"[DynamicHide] 無法找到內部變數 '{condHAtt.dynamicSourceField}'");
            }
        }

        // 判斷是否顯示
        if (!shouldBeHidden || !condHAtt.HideInInspector)
        {
            EditorGUI.PropertyField(position, property, label, true);
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        DynamicHideAttribute condHAtt = (DynamicHideAttribute)attribute;

        // 獲取目標物件
        Object targetObject = property.serializedObject.targetObject;
        bool shouldBeHidden = false;

        if (condHAtt.IsExternalSource)
        {
            // 使用反射從外部獲取變數值
            var externalField = targetObject.GetType().GetField(condHAtt.dynamicSourceField);
            if (externalField != null)
            {
                shouldBeHidden = !(bool)externalField.GetValue(targetObject);
            }
        } else
        {
            // 使用序列化屬性獲取當前物件內部變數值
            SerializedProperty sourcePropertyValue = property.serializedObject.FindProperty(condHAtt.dynamicSourceField);
            if (sourcePropertyValue != null)
            {
                shouldBeHidden = !sourcePropertyValue.boolValue;
            }
        }

        // 隱藏時高度設為 0
        if (shouldBeHidden && condHAtt.HideInInspector)
        {
            return 0;
        }

        return EditorGUI.GetPropertyHeight(property, label, true);
    }
}

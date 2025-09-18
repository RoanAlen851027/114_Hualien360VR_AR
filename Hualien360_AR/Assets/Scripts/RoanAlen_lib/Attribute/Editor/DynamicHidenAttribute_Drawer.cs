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
    [CustomPropertyDrawer(typeof(DynamicHidenAttribute))]
    public class DynamicHidenAttribute_Drawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!ShouldShow(property)) return 0;
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!ShouldShow(property)) return;
            EditorGUI.PropertyField(position, property, label, true);
        }

        private bool ShouldShow(SerializedProperty property)
        {
            DynamicHidenAttribute dynamic = (DynamicHidenAttribute)attribute;

            string conditionPath = property.propertyPath.Replace(property.name, dynamic.ConditionFieldName);
            SerializedProperty conditionProp = property.serializedObject.FindProperty(conditionPath);

            if (conditionProp == null)
            {
                Debug.LogWarning($"找不到欄位 {dynamic.ConditionFieldName}");
                return true;
            }

            return conditionProp.boolValue;
        }
    }
}
#endif

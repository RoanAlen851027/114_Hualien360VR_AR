using UnityEditor;
using UnityEngine;
using System;
using System.Linq;
using System.Reflection;

[CustomPropertyDrawer(typeof(Bindable<>), true)]
public class BindablePropertyDrawer : PropertyDrawer
{
    private RangeAttribute _rangeAttribute;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty valueProperty = property.FindPropertyRelative("value");
        if (valueProperty == null)
        {
            EditorGUI.LabelField(position, label.text, "Unsupported type");
            return;
        }

        // Initialize RangeAttribute
        var fieldInfo = GetFieldInfo(property);
        if (fieldInfo != null)
        {
            _rangeAttribute = fieldInfo.GetCustomAttributes(typeof(RangeAttribute), true)
                .FirstOrDefault() as RangeAttribute;
        }

        EditorGUI.BeginProperty(position, label, property);
        EditorGUI.BeginChangeCheck();

        // Draw range slider if applicable
        if (_rangeAttribute != null)
        {
            if (valueProperty.propertyType == SerializedPropertyType.Integer)
            {
                EditorGUI.IntSlider(position, valueProperty, (int)_rangeAttribute.min, (int)_rangeAttribute.max, label);
            } else if (valueProperty.propertyType == SerializedPropertyType.Float)
            {
                EditorGUI.Slider(position, valueProperty, _rangeAttribute.min, _rangeAttribute.max, label);
            } else
            {
                EditorGUI.PropertyField(position, valueProperty, label, true);
            }
        } else
        {
            EditorGUI.PropertyField(position, valueProperty, label, true);
        }

        if (EditorGUI.EndChangeCheck())
        {
            property.serializedObject.ApplyModifiedProperties();
            InvokeBindable(property);
        }

        EditorGUI.EndProperty();
    }

    private FieldInfo GetFieldInfo(SerializedProperty property)
    {
        var targetObject = property.serializedObject.targetObject;
        var targetType = targetObject.GetType();
        var fieldPath = property.propertyPath.Split('.').Last(); // Get the field name from the property path
        return targetType.GetField(fieldPath, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
    }

    private void InvokeBindable(SerializedProperty property)
    {
        var targetObject = property.serializedObject.targetObject;
        var fieldInfo = GetFieldInfo(property);
        if (fieldInfo != null)
        {
            var bindable = fieldInfo.GetValue(targetObject) as Invokeable;
            bindable?.Invoke();
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        SerializedProperty valueProperty = property.FindPropertyRelative("value");
        return valueProperty != null ? EditorGUI.GetPropertyHeight(valueProperty, label, true) : EditorGUIUtility.singleLineHeight;
    }
}

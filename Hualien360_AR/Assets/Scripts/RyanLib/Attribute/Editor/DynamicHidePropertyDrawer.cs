using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(DynamicHideAttribute))]
public class DynamicHidePropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        DynamicHideAttribute condHAtt = (DynamicHideAttribute)attribute;

        // ����ؼЪ���
        Object targetObject = property.serializedObject.targetObject;
        bool shouldBeHidden = false;

        if (condHAtt.IsExternalSource)
        {
            // �ϥΤϮg�q�~������ܼƭ�
            var externalField = targetObject.GetType().GetField(condHAtt.dynamicSourceField);
            if (externalField != null)
            {
                shouldBeHidden = !(bool)externalField.GetValue(targetObject);
            } else
            {
                Debug.LogError($"[DynamicHide] �L�k���~���ܼ� '{condHAtt.dynamicSourceField}'");
            }
        } else
        {
            // �ϥΧǦC���ݩ������e���󤺳��ܼƭ�
            SerializedProperty sourcePropertyValue = property.serializedObject.FindProperty(condHAtt.dynamicSourceField);
            if (sourcePropertyValue != null)
            {
                shouldBeHidden = !sourcePropertyValue.boolValue;
            } else
            {
                Debug.LogError($"[DynamicHide] �L�k��줺���ܼ� '{condHAtt.dynamicSourceField}'");
            }
        }

        // �P�_�O�_���
        if (!shouldBeHidden || !condHAtt.HideInInspector)
        {
            EditorGUI.PropertyField(position, property, label, true);
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        DynamicHideAttribute condHAtt = (DynamicHideAttribute)attribute;

        // ����ؼЪ���
        Object targetObject = property.serializedObject.targetObject;
        bool shouldBeHidden = false;

        if (condHAtt.IsExternalSource)
        {
            // �ϥΤϮg�q�~������ܼƭ�
            var externalField = targetObject.GetType().GetField(condHAtt.dynamicSourceField);
            if (externalField != null)
            {
                shouldBeHidden = !(bool)externalField.GetValue(targetObject);
            }
        } else
        {
            // �ϥΧǦC���ݩ������e���󤺳��ܼƭ�
            SerializedProperty sourcePropertyValue = property.serializedObject.FindProperty(condHAtt.dynamicSourceField);
            if (sourcePropertyValue != null)
            {
                shouldBeHidden = !sourcePropertyValue.boolValue;
            }
        }

        // ���îɰ��׳]�� 0
        if (shouldBeHidden && condHAtt.HideInInspector)
        {
            return 0;
        }

        return EditorGUI.GetPropertyHeight(property, label, true);
    }
}

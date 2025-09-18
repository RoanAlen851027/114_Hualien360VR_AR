/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System;

namespace RoanAlen.Attribute
{
    [CustomPropertyDrawer(typeof(RandomButtonAttribute))]
    public class RandomButtonAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            RandomButtonAttribute random = (RandomButtonAttribute)attribute;

            float buttonWidth = 60f;
            Rect fieldRect = new Rect(position.x, position.y, position.width - buttonWidth - 5f, position.height);
            Rect buttonRect = new Rect(position.x + position.width - buttonWidth, position.y, buttonWidth, position.height);

            EditorGUI.PropertyField(fieldRect, property, label);

            if (GUI.Button(buttonRect, "隨機"))
            {
                if (property.propertyType == SerializedPropertyType.Integer)
                {
                    property.intValue = UnityEngine.Random.Range(Mathf.RoundToInt(random.Min), Mathf.RoundToInt(random.Max + 1));
                }
                else if (property.propertyType == SerializedPropertyType.Float)
                {
                    property.floatValue = UnityEngine.Random.Range(random.Min, random.Max);
                }
                else
                {
                    Debug.LogWarning("RandomButton 只能用在 int 或 float 上");
                }
            }
        }
    }
}
#endif

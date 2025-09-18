/*************************************************
  * 名稱：TagEnumDrawer
  * 作者：RyanHsu
  * 功能說明：
  * ***********************************************/
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(TagEnumAttribute))]
public class TagEnumDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType == SerializedPropertyType.String)
        {
            EditorGUI.BeginProperty(position, label, property);

            string[] tags = UnityEditorInternal.InternalEditorUtility.tags;
            int index = Mathf.Max(0, System.Array.IndexOf(tags, property.stringValue));
            index = EditorGUI.Popup(position, label.text, index, tags);
            property.stringValue = tags[index];

            EditorGUI.EndProperty();
        } else
        {
            EditorGUI.PropertyField(position, property, label);
        }
    }
}
/*************************************************
  * 名稱：AssetFileNameDrawer
  * 作者：RyanHsu
  * 功能說明：從 Inspector 中拖放資源並將其轉換為資源的文件名
  * ***********************************************/
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(AssetFileNameAttribute))]
public class AssetFileNameDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType == SerializedPropertyType.String)
        {
            // 顯示為 ObjectField
            EditorGUI.BeginProperty(position, label, property);

            // 繪製 ObjectField
            EditorGUI.ObjectField(position, property.objectReferenceValue, typeof(Object), false);

            // 取得資源路徑
            Object obj = property.objectReferenceValue;
            if (obj != null)
            {
                string assetPath = AssetDatabase.GetAssetPath(obj);
                string fileName = System.IO.Path.GetFileName(assetPath);
                property.stringValue = fileName;
            } else
            {
                property.stringValue = "";
            }

            EditorGUI.EndProperty();
        } else
        {
            EditorGUI.LabelField(position, label.text, "Use AssetFileName with String type.");
        }
    }
}

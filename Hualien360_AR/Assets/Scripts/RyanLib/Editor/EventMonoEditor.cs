using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Diagnostics;

[CustomEditor(typeof(EventMono))]
public class EventMonoEditor : Editor
{
    private SerializedProperty triggers;
    private EventMono.EventMonoType selectedEventID; // 用來保存 Add Menu 的選中值
    private GUIStyle largeFontStyle; // 定義字體樣式

    private void OnEnable()
    {
        triggers = serializedObject.FindProperty("m_Delegates");
    }

    public override void OnInspectorGUI()
    {
        // 初始化字體樣式
        largeFontStyle = new GUIStyle(EditorStyles.label)
        {
            fontSize = 14, // 放大字體
            fontStyle = FontStyle.Bold // 粗體可選
        };
        serializedObject.Update();

        GUILayout.Label("Event Mono - Trigger Events", EditorStyles.boldLabel);

        // 遍歷所有 Trigger 事件
        for (int i = 0; i < triggers.arraySize; i++)
        {
            SerializedProperty entry = triggers.GetArrayElementAtIndex(i);
            SerializedProperty eventID = entry.FindPropertyRelative("eventID");
            SerializedProperty callback = entry.FindPropertyRelative("callback");

            // 顯示每個 Trigger 的視覺框
            EditorGUILayout.BeginVertical("box");

            // 顯示刪除按鈕在右上角
            EditorGUILayout.BeginHorizontal();

            // 顯示 EventID 的名稱作為 Label
            GUILayout.Label($"【{((EventMono.EventMonoType)eventID.enumValueIndex).ToString()}】", largeFontStyle);

            GUILayout.FlexibleSpace();
            if (GUILayout.Button("-", GUILayout.Width(20)))
            {
                // 刪除當前的 Trigger 並立即刷新編輯器
                triggers.DeleteArrayElementAtIndex(i);
                serializedObject.ApplyModifiedProperties(); // 更新編輯器顯示
                return; // 退出方法，防止在刪除後還繼續處理下面的項目
            }
            EditorGUILayout.EndHorizontal();

            // 顯示 Callback 欄位
            EditorGUILayout.PropertyField(callback);

            EditorGUILayout.EndVertical();

            // 每個 Trigger 項之間的間距
            GUILayout.Space(5);
        }

        // 顯示 Add New Trigger 和 EventID 選單
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        // 收集已使用的 EventID
        HashSet<EventMono.EventMonoType> usedEventIDs = new HashSet<EventMono.EventMonoType>();
        for (int i = 0; i < triggers.arraySize; i++)
        {
            SerializedProperty entry = triggers.GetArrayElementAtIndex(i);
            SerializedProperty eventID = entry.FindPropertyRelative("eventID");
            usedEventIDs.Add((EventMono.EventMonoType)eventID.enumValueIndex);
        }

        // 構建剩餘可用的選項
        List<EventMono.EventMonoType> availableEventIDs = new List<EventMono.EventMonoType>();
        foreach (EventMono.EventMonoType eventID in System.Enum.GetValues(typeof(EventMono.EventMonoType)))
        {
            if (!usedEventIDs.Contains(eventID))
            {
                availableEventIDs.Add(eventID);
            }
        }

        // 如果還有剩餘選項，顯示 EnumPopup；否則顯示提示
        if (availableEventIDs.Count > 0)
        {
            int selectedIndex = availableEventIDs.IndexOf(selectedEventID);
            selectedIndex = EditorGUILayout.Popup(
                selectedIndex >= 0 ? selectedIndex : 0,
                availableEventIDs.ConvertAll(e => e.ToString()).ToArray(),
                GUILayout.Width(80)
            );

            selectedEventID = availableEventIDs[selectedIndex];
        } else
        {
            GUILayout.Label("No available Event IDs", EditorStyles.helpBox);
        }

        // Add New Trigger 按鈕
        if (GUILayout.Button("Add New Trigger", GUILayout.Width(150)))
        {
            if (availableEventIDs.Count > 0)
            {
                // 新增一個 Trigger 項目
                int newIndex = triggers.arraySize;
                triggers.InsertArrayElementAtIndex(newIndex);

                // 設置新建 Trigger 的 EventID
                SerializedProperty newEntry = triggers.GetArrayElementAtIndex(newIndex);
                SerializedProperty newEventID = newEntry.FindPropertyRelative("eventID");
                newEventID.enumValueIndex = (int)selectedEventID;

                serializedObject.ApplyModifiedProperties(); // 更新編輯器顯示
            }
        }

        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        // 應用修改
        serializedObject.ApplyModifiedProperties();
    }
}

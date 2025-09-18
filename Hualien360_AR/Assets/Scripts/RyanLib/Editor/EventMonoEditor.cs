using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Diagnostics;

[CustomEditor(typeof(EventMono))]
public class EventMonoEditor : Editor
{
    private SerializedProperty triggers;
    private EventMono.EventMonoType selectedEventID; // �ΨӫO�s Add Menu ���襤��
    private GUIStyle largeFontStyle; // �w�q�r��˦�

    private void OnEnable()
    {
        triggers = serializedObject.FindProperty("m_Delegates");
    }

    public override void OnInspectorGUI()
    {
        // ��l�Ʀr��˦�
        largeFontStyle = new GUIStyle(EditorStyles.label)
        {
            fontSize = 14, // ��j�r��
            fontStyle = FontStyle.Bold // ����i��
        };
        serializedObject.Update();

        GUILayout.Label("Event Mono - Trigger Events", EditorStyles.boldLabel);

        // �M���Ҧ� Trigger �ƥ�
        for (int i = 0; i < triggers.arraySize; i++)
        {
            SerializedProperty entry = triggers.GetArrayElementAtIndex(i);
            SerializedProperty eventID = entry.FindPropertyRelative("eventID");
            SerializedProperty callback = entry.FindPropertyRelative("callback");

            // ��ܨC�� Trigger ����ı��
            EditorGUILayout.BeginVertical("box");

            // ��ܧR�����s�b�k�W��
            EditorGUILayout.BeginHorizontal();

            // ��� EventID ���W�٧@�� Label
            GUILayout.Label($"�i{((EventMono.EventMonoType)eventID.enumValueIndex).ToString()}�j", largeFontStyle);

            GUILayout.FlexibleSpace();
            if (GUILayout.Button("-", GUILayout.Width(20)))
            {
                // �R����e�� Trigger �åߧY��s�s�边
                triggers.DeleteArrayElementAtIndex(i);
                serializedObject.ApplyModifiedProperties(); // ��s�s�边���
                return; // �h�X��k�A����b�R�������~��B�z�U��������
            }
            EditorGUILayout.EndHorizontal();

            // ��� Callback ���
            EditorGUILayout.PropertyField(callback);

            EditorGUILayout.EndVertical();

            // �C�� Trigger �����������Z
            GUILayout.Space(5);
        }

        // ��� Add New Trigger �M EventID ���
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        // �����w�ϥΪ� EventID
        HashSet<EventMono.EventMonoType> usedEventIDs = new HashSet<EventMono.EventMonoType>();
        for (int i = 0; i < triggers.arraySize; i++)
        {
            SerializedProperty entry = triggers.GetArrayElementAtIndex(i);
            SerializedProperty eventID = entry.FindPropertyRelative("eventID");
            usedEventIDs.Add((EventMono.EventMonoType)eventID.enumValueIndex);
        }

        // �c�سѾl�i�Ϊ��ﶵ
        List<EventMono.EventMonoType> availableEventIDs = new List<EventMono.EventMonoType>();
        foreach (EventMono.EventMonoType eventID in System.Enum.GetValues(typeof(EventMono.EventMonoType)))
        {
            if (!usedEventIDs.Contains(eventID))
            {
                availableEventIDs.Add(eventID);
            }
        }

        // �p�G�٦��Ѿl�ﶵ�A��� EnumPopup�F�_�h��ܴ���
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

        // Add New Trigger ���s
        if (GUILayout.Button("Add New Trigger", GUILayout.Width(150)))
        {
            if (availableEventIDs.Count > 0)
            {
                // �s�W�@�� Trigger ����
                int newIndex = triggers.arraySize;
                triggers.InsertArrayElementAtIndex(newIndex);

                // �]�m�s�� Trigger �� EventID
                SerializedProperty newEntry = triggers.GetArrayElementAtIndex(newIndex);
                SerializedProperty newEventID = newEntry.FindPropertyRelative("eventID");
                newEventID.enumValueIndex = (int)selectedEventID;

                serializedObject.ApplyModifiedProperties(); // ��s�s�边���
            }
        }

        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        // ���έק�
        serializedObject.ApplyModifiedProperties();
    }
}

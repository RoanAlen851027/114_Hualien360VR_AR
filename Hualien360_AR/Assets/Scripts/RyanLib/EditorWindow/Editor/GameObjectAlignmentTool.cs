using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace RyanLab
{

    public class GameObjectAlignmentTool : EditorWindow
    {
        private enum Axis { X, Y, Z }
        private Axis scrollAxis = Axis.X;
        private int historySize = 30;
        private Stack<List<Vector3>> historyStack = new Stack<List<Vector3>>();
        private Stack<List<Vector3>> redoStack = new Stack<List<Vector3>>();

        [MenuItem("Tools/CustomTools/GameObject Alignment Tool")]
        public static void ShowWindow()
        {
            GetWindow<GameObjectAlignmentTool>("GameObject Alignment Tool");
        }

        void OnGUI()
        {
            GUILayout.Label("Select alignment axis:");
            scrollAxis = (Axis)EditorGUILayout.EnumPopup(scrollAxis);

            GUILayout.Space(10);

            GUILayout.Label("Alignment options:");
            if (GUILayout.Button("Align Left"))
            {
                AlignSelectedGameObjects(Alignment.Left);
            }
            if (GUILayout.Button("Align Center"))
            {
                AlignSelectedGameObjects(Alignment.Center);
            }
            if (GUILayout.Button("Align Right"))
            {
                AlignSelectedGameObjects(Alignment.Right);
            }
            if (GUILayout.Button("Distribute"))
            {
                DistributeSelectedGameObjects();
            }

            GUILayout.Space(10);

            GUILayout.Label("Undo/Redo:");
            EditorGUI.BeginDisabledGroup(historyStack.Count == 0);
            if (GUILayout.Button("Undo"))
            {
                UndoAction();
            }
            EditorGUI.EndDisabledGroup();

            EditorGUI.BeginDisabledGroup(redoStack.Count == 0);
            if (GUILayout.Button("Redo"))
            {
                RedoAction();
            }
            EditorGUI.EndDisabledGroup();
        }

        private void AlignSelectedGameObjects(Alignment alignment)
        {
            GameObject[] selectedObjects = Selection.gameObjects;

            if (selectedObjects.Length < 2)
            {
                Debug.LogWarning("Please select at least two GameObjects to align.");
                return;
            }

            List<Vector3> positions = new List<Vector3>();

            foreach (GameObject obj in selectedObjects)
            {
                positions.Add(obj.transform.position);
            }

            RecordHistory(positions);

            float referencePosition = GetReferencePosition(selectedObjects, alignment);

            foreach (GameObject obj in selectedObjects)
            {
                Vector3 newPosition = obj.transform.position;
                float offset = referencePosition - newPosition[(int)scrollAxis];

                if (alignment == Alignment.Right)
                {
                    offset *= -1;
                }

                newPosition[(int)scrollAxis] += offset;
                obj.transform.position = newPosition;
            }
        }

        private void DistributeSelectedGameObjects()
        {
            GameObject[] selectedObjects = Selection.gameObjects;

            if (selectedObjects.Length < 3)
            {
                Debug.LogWarning("Please select at least three GameObjects to distribute.");
                return;
            }

            List<Vector3> positions = new List<Vector3>();

            foreach (GameObject obj in selectedObjects)
            {
                positions.Add(obj.transform.position);
            }

            RecordHistory(positions);

            List<GameObject> sortedObjects = new List<GameObject>(selectedObjects);
            sortedObjects.Sort((x, y) => x.transform.position[(int)scrollAxis].CompareTo(y.transform.position[(int)scrollAxis]));

            float minPosition = sortedObjects[0].transform.position[(int)scrollAxis];
            float maxPosition = sortedObjects[sortedObjects.Count - 1].transform.position[(int)scrollAxis];

            float spacing = (maxPosition - minPosition) / (selectedObjects.Length - 1);

            int index = 0;
            foreach (GameObject obj in sortedObjects)
            {
                Vector3 newPosition = obj.transform.position;
                if (index != 0 && index != sortedObjects.Count - 1)
                {
                    newPosition[(int)scrollAxis] = minPosition + spacing * index;
                    obj.transform.position = newPosition;
                }
                index++;
            }
        }

        private float GetReferencePosition(GameObject[] selectedObjects, Alignment alignment)
        {
            float referencePosition = 0f;

            if (alignment == Alignment.Left)
            {
                referencePosition = float.MaxValue;
                foreach (GameObject obj in selectedObjects)
                {
                    float position = obj.transform.position[(int)scrollAxis];
                    if (position < referencePosition)
                    {
                        referencePosition = position;
                    }
                }
            } else if (alignment == Alignment.Right)
            {
                referencePosition = float.MinValue;
                foreach (GameObject obj in selectedObjects)
                {
                    float position = obj.transform.position[(int)scrollAxis];
                    if (position > referencePosition)
                    {
                        referencePosition = position;
                    }
                }
            } else // Alignment.Center
            {
                float minPosition = float.MaxValue;
                float maxPosition = float.MinValue;

                foreach (GameObject obj in selectedObjects)
                {
                    float position = obj.transform.position[(int)scrollAxis];
                    if (position < minPosition)
                    {
                        minPosition = position;
                    }
                    if (position > maxPosition)
                    {
                        maxPosition = position;
                    }
                }

                referencePosition = (minPosition + maxPosition) / 2f;
            }

            return referencePosition;
        }

        private void RecordHistory(List<Vector3> positions)
        {
            historyStack.Push(new List<Vector3>(positions));

            if (historyStack.Count > historySize)
            {
                historyStack.Pop();
            }

            redoStack.Clear();
        }

        private void UndoAction()
        {
            if (historyStack.Count == 0) return;

            List<Vector3> positions = historyStack.Pop();
            redoStack.Push(new List<Vector3>(positions));

            GameObject[] selectedObjects = Selection.gameObjects;

            for (int i = 0; i < selectedObjects.Length; i++)
            {
                selectedObjects[i].transform.position = positions[i];
            }
        }

        private void RedoAction()
        {
            if (redoStack.Count == 0) return;

            List<Vector3> positions = redoStack.Pop();
            historyStack.Push(new List<Vector3>(positions));

            GameObject[] selectedObjects = Selection.gameObjects;

            for (int i = 0; i < selectedObjects.Length; i++)
            {
                selectedObjects[i].transform.position = positions[i];
            }
        }

        private enum Alignment { Left, Center, Right }
    }
}
/********************************
---------------------------------
著作者：RoanAlen
用途：Editor Window - 快速調整Image

% 表示 Ctrl（在 macOS 上是 Cmd）
# 表示 Shift
& 表示 Alt
_ 表示無修飾按鍵（單一按鍵）
---------------------------------
*********************************/
#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
namespace RoanAlen
{
    namespace Editor
    {
        public class EditorWindow_Image : EditorWindow
        {
            private float scale = 1f;
            private float lastScale = 1f;

            private float alpha = 1f;
            private float newAlpha = 1f;

            private Texture2D previewTexture;

            // 設定圖標的路徑
            private static readonly string iconPath = "Assets/_Script/Roan_Library/Editor/Icon/Icon_Image.png";

            [MenuItem("RoanAlen/Image Editor %&I")]
            public static void ShowWindow()
            {
                var window = GetWindow<EditorWindow_Image>("Image Editor");

                // 設置圖標
                Texture2D icon = AssetDatabase.LoadAssetAtPath<Texture2D>(iconPath);
                if (icon != null)
                {
                    window.titleContent = new GUIContent("Image Editor", icon);  // 設定標題和圖標
                }
                else
                {
                    Debug.LogWarning("Icon not found at path: " + iconPath);
                }
            }

            private void OnGUI()
            {
                bool hasImage = false;
                foreach (var obj in Selection.objects)
                {
                    if (obj is GameObject gameObject && gameObject.GetComponent<Image>() != null)
                    {
                        hasImage = true;
                        break;
                    }
                }


                GUI.enabled = hasImage;

                GUILayout.Label("Image Preview", EditorStyles.boldLabel);
                DrawSelectedImagePreview();

                GUILayout.Label("Scale Image", EditorStyles.boldLabel);
                DrawHorizontalSlider("Scale", ref scale, 0f, 10f);
                DrawHorizontalButton("Apply Scale to Image", ApplyScaleToSelectedImage);

                GUILayout.Space(10f);
                DrawAlphaSlider();

                GUILayout.Space(20);
                GUI.enabled = true;
                if (!hasImage)
                {
                    string message = "請選擇帶有 Image 元件的物件";
                    GUIStyle style = new GUIStyle(EditorStyles.helpBox);
                    style.alignment = TextAnchor.MiddleCenter;
                    style.fontSize = 14;
                    style.wordWrap = true;

                    float boxWidth = 300f;
                    float boxHeight = 60f;

                    float centerX = (position.width - boxWidth) / 2f;
                    float bottomY = position.height - boxHeight - 20f; // 距離底部留 20px 邊距
                                                                       // 叫你好好選辣
                    Texture warningIcon = EditorGUIUtility.IconContent("console.erroricon").image;
                    Rect boxRect = GUILayoutUtility.GetRect(boxWidth, boxHeight);
                    Rect iconRect = new Rect(centerX + 10, bottomY + (boxHeight - 32) / 2, 32, 32);
                    GUI.DrawTexture(iconRect, warningIcon);
                    Rect bottomRect = new Rect(centerX, bottomY, boxWidth, boxHeight);
                    GUI.Label(bottomRect, message, style);
                    return;


                }
                UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
                SceneView.RepaintAll();
                EditorApplication.QueuePlayerLoopUpdate();
                Repaint();
            }

            private void OnEnable()
            {
                Selection.selectionChanged += OnSelectionChanged;
            }

            private void OnDisable()
            {
                Selection.selectionChanged -= OnSelectionChanged;
            }

            private void OnSelectionChanged()
            {
                Repaint();
            }

            // 封裝的水平布局和按鈕
            private void DrawHorizontalButton(string label, Action onClick)
            {
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button(label, GUILayout.Width(150), GUILayout.Height(50)))
                {
                    onClick?.Invoke();
                }
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }

            // 封裝的 Alpha 滑桿
            private void DrawAlphaSlider()
            {
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUILayout.Label("Alpha", GUILayout.Width(50f)); // 控制名稱寬度，讓名稱更靠近滑桿

                // 手動設置 Slider 範圍
                Rect sliderRect = GUILayoutUtility.GetRect(0, 20f, GUILayout.Width(150f)); // 設置滑桿寬度
                newAlpha = EditorGUI.Slider(sliderRect, alpha, 0f, 1f); // 使用手動設定的滑桿範圍

                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();

                newAlpha = Mathf.Round(newAlpha * 100f) / 100f;
                if (Mathf.Abs(newAlpha - alpha) > Mathf.Epsilon)
                {
                    alpha = newAlpha;
                    ApplyAlphaToSelectedImage();
                    UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
                    SceneView.RepaintAll();
                    EditorApplication.QueuePlayerLoopUpdate();
                }
            }

            // 封裝的水平布局和滑桿
            private void DrawHorizontalSlider(string label, ref float value, float minValue, float maxValue)
            {
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUILayout.Label(label, GUILayout.Width(50f));
                // 手動設置 Slider 範圍
                Rect sliderRect = GUILayoutUtility.GetRect(0, 20f, GUILayout.Width(position.width - 60f)); // 設置滑桿寬度
                value = EditorGUI.Slider(sliderRect, value, minValue, maxValue);
                if (Mathf.Abs(scale - lastScale) > Mathf.Epsilon)
                {
                    ApplyScaleToSelectedImage();
                    lastScale = scale;
                }
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }

            private void ApplyScaleToSelectedImage()
            {
                foreach (var obj in Selection.objects)
                {
                    if (obj is GameObject gameObject)
                    {
                        Image image = gameObject.GetComponent<Image>();
                        if (image != null & image.sprite != null)
                        {
                            ResizeImage(image, scale);
                        }
                    }
                }
            }

            private void ResizeImage(Image image, float scale)
            {
                Sprite sprite = image.sprite;
                Vector2 nativeSize = new Vector2(sprite.rect.width, sprite.rect.height);
                RectTransform rect = image.GetComponent<RectTransform>();
                rect.sizeDelta = nativeSize * scale;
            }

            private void ApplyAlphaToSelectedImage()
            {
                foreach (var obj in Selection.objects)
                {
                    if (obj is GameObject gameObject)
                    {
                        Image image = gameObject.GetComponent<Image>();
                        if (image != null)
                        {
                            Color color = image.color;
                            color.a = alpha;
                            image.color = color;
                            Repaint();
                        }
                    }
                }
            }

            private void DrawSelectedImagePreview()
            {
                float previewBoxSize = 150f;
                float centerX = (position.width - previewBoxSize) / 2f;
                float previewTopY = GUILayoutUtility.GetRect(0, previewBoxSize).y;
                Rect previewArea = new Rect(centerX, previewTopY, previewBoxSize, previewBoxSize);

                // 畫背景框（固定顯示）
                EditorGUI.DrawRect(previewArea, new Color(60 / 255f, 60 / 255f, 60 / 255f, 1f));

                bool hasValidImage = false;

                foreach (var obj in Selection.objects)
                {
                    if (obj is GameObject gameObject)
                    {
                        Image image = gameObject.GetComponent<Image>();
                        if (image != null && image.sprite != null)
                        {
                            hasValidImage = true;

                            Sprite sprite = image.sprite;
                            Rect spriteRect = sprite.rect;
                            Texture2D tex = sprite.texture;

                            float spriteAspect = spriteRect.width / spriteRect.height;
                            float drawWidth = previewBoxSize;
                            float drawHeight = previewBoxSize;

                            if (spriteAspect > 1f)
                                drawHeight = previewBoxSize / spriteAspect;
                            else
                                drawWidth = previewBoxSize * spriteAspect;

                            float offsetX = (previewBoxSize - drawWidth) / 2f;
                            float offsetY = (previewBoxSize - drawHeight) / 2f;

                            Rect centeredRect = new Rect(
                                previewArea.x + offsetX,
                                previewArea.y + offsetY,
                                drawWidth,
                                drawHeight
                            );
                            GUI.DrawTexture(centeredRect, tex, ScaleMode.ScaleToFit, true, 0, new Color(1, 1, 1, alpha), 0, 0);
                        }
                    }
                }

                if (!hasValidImage)
                {
                    // 沒有圖片時顯示提示文字
                    GUIStyle style = new GUIStyle(EditorStyles.boldLabel);
                    style.alignment = TextAnchor.MiddleCenter;
                    style.normal.textColor = Color.white;
                    GUI.Label(previewArea, "No Image", style);
                }
            }
        }
    }
}
#endif
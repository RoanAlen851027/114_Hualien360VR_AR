/*************************************************
  * 名稱：ImageScaleEditorWindow
  * 作者：RyanHsu
  * 功能說明：方便對Image做等比列縮放WH
  * ***********************************************/
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class ImageScaleEditorWindow : EditorWindow
{
    private float scale = 1f;
    private float lastScale = 1f;

    [MenuItem("Tools/CustomTools/Image Scale Editor")]
    public static void ShowWindow()
    {
        GetWindow<ImageScaleEditorWindow>("Image Scale Editor");
    }

    private void OnGUI()
    {
        GUILayout.Label("Scale Images", EditorStyles.boldLabel);

        // 允許用戶調整縮放比例
        scale = EditorGUILayout.Slider("Scale", scale, 0f, 10f);

        // 如果 scale 值發生變化，則應用縮放
        if (Mathf.Abs(scale - lastScale) > Mathf.Epsilon)
        {
            ApplyScaleToSelectedImages();
            lastScale = scale;
        }

        GUILayout.Space(10f);

        // 按鈕來執行縮放
        if (GUILayout.Button("Apply Scale to Selected Images"))
        {
            ApplyScaleToSelectedImages();
        }

        GUILayout.Space(10f);

        // 按鈕來執行縮放
        if (GUILayout.Button("Apply RenName GameObject"))
        {
            ApplyReNameObj();
        }
    }

    private void ApplyScaleToSelectedImages()
    {
        foreach (var obj in Selection.objects)
        {
            if (obj is GameObject gameObject)
            {
                Image image = gameObject.GetComponent<Image>();
                if (image != null && image.sprite != null)
                {
                    ResizeImage(image, scale);
                }
            }
        }
    }

    private void ApplyReNameObj()
    {
        foreach (var obj in Selection.objects)
        {
            if (obj is GameObject gameObject)
            {
                Image image = gameObject.GetComponent<Image>();
                if (image != null && image.sprite != null)
                {
                    obj.name = image.sprite.name;
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

        Debug.Log($"Resized {image.gameObject.name} to scale {scale}");
    }

}

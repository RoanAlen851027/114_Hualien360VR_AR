using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
[RequireComponent(typeof(Image))]
public class ImageCarousel : MonoBehaviour
{
    public Image image;
    public Sprite[] sprites;
    [Range(0f, 100f)] public float speed;
    public float imageIndex = 0;
    DirtyInt dirty = new DirtyInt(0);

    void Update()
    {
        if (sprites.Length <= 0 || sprites[0] == null) return;
        //
        imageIndex += Time.deltaTime * speed;
        int index_int = Mathf.FloorToInt(imageIndex) % sprites.Length;
        if (dirty.isDirty(index_int))
        {
            if (!image.enabled) image.enabled = true;
            image.sprite = sprites[index_int];
        }
    }

    public void SetIndex(int index) => imageIndex = (index < 0) ? 0 : index;

#if UNITY_EDITOR
    void OnValidate()
    {
        if (image == null) image = GetComponent<Image>();
        if (imageIndex < 0) imageIndex = 0;
        if (sprites.Length > 0)
        {
            if (sprites[0] != null)
            {
                int index_int = Mathf.FloorToInt(imageIndex) % sprites.Length;
                image.sprite = sprites[index_int];
            } else
            {
                sprites[0] = image.sprite;
            }
        }
    }
#endif
}

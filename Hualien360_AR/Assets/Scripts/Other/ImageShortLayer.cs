/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using UnityEngine;

using UnityEngine.UI;
public class ImageShortLayer : MonoBehaviour
{

    [SerializeField]
    private int sortingOrder;
    [SerializeField]
    private Canvas canvas;
    void Start()
    {
        Image myImage = GetComponent<Image>();
        canvas.sortingOrder = sortingOrder;  // 設比 LineRenderer 高
    }


}

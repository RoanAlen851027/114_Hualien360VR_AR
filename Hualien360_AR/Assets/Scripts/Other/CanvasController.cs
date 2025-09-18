using UnityEngine;
/********************************
---------------------------------
著作者：RoanAlen
用途：Canvas控制畫面呈現
---------------------------------
*********************************/
public class CanvasController : MonoBehaviour
{

    [SerializeField]
    private Canvas canvas;

    void Start()
    {
        if (canvas == null)
        {
            canvas=this.GetComponent<Canvas>();
        }
    }


    public void CanvasLayoutControl(int Layout)
    {
        canvas.sortingOrder = Layout;
    }

}

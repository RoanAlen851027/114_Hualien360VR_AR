/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class PanelSelectionHandler : MonoBehaviour
{

    public int select_Int;
    public int Diselect_Int;

    public UnityEvent Select_Event;
    public UnityEvent Diselect_Event;
    public void OnSelect()
    {
        // 當前被選中（切到中間）時要做的事
        select_Int++;
        Select_Event?.Invoke();
        Debug.Log("目前中間的面板是：" + this.gameObject.name);
    }

    public void OnDeselect()
    {
        // 不是中間時要做的事
        Diselect_Int++;
        Diselect_Event?.Invoke();
        Debug.Log("目前這不是中間面板：" + this.gameObject.name);

    }

}

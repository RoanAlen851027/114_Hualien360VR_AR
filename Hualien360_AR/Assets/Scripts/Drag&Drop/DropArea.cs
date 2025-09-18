/*************************************************
  * 名稱：DropArea
  * 作者：RyanHsu
  * 功能說明：
  * ***********************************************/
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour, IDropHandler
{
    public UnityEvent<string> OnDropdown;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dragGo = eventData.pointerDrag;
        if (dragGo != null)
        {
            if (OnDropdown != null) OnDropdown.Invoke(dragGo.name);
        }
    }
}

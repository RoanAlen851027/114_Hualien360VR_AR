/*************************************************
  * 名稱：RayCharacterButton
  * 作者：RyanHsu
  * 功能說明：
  * ***********************************************/
using UnityEngine;
using UnityEngine.Events;

public class RayCharacterButton : MonoBehaviour, IPointerDown3D, IPointerClick3D
{
    public UnityEvent OnButtonDown; 
    public UnityEvent OnButtonClick;

    public void OnPointerDown3D(Collider collider)
    {
        OnButtonDown.Invoke();
    }

    public void OnPointerClick3D(Collider collider)
    {
        OnButtonClick.Invoke();
    }
}

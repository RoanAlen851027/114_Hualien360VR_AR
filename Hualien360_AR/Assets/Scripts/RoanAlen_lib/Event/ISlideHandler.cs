/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using UnityEngine;
using UnityEngine.EventSystems;

public interface ISlideHandler: IEventSystemHandler
{
    void OnSlide(Vector2 slideDelta);
}


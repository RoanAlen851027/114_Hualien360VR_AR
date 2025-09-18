/********************************
---------------------------------
著作者：RoanAlen
用途：事件庫基底
---------------------------------
*********************************/
using UnityEngine;
namespace RoanAlen
{
    namespace Library
    {
        public abstract class Event_Base : MonoBehaviour
        {
            [SerializeField]
            protected Event_Library eventLibrary = Event_Library.GetInstance();

            protected void OnEnable()
            {
                eventLibrary.OnEventScriptObject.AddListener(OnEventScriptObject);

            }

            protected void OnDisable()
            {
                eventLibrary.OnEventScriptObject.RemoveListener(OnEventScriptObject);
            }

            protected abstract void OnEventScriptObject(string id, object evt);
        }
    }
}
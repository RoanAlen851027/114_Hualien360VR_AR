/********************************
---------------------------------
著作者：RoanAlen
用途：事件資料庫
---------------------------------
*********************************/
using UnityEngine.Events;
namespace RoanAlen
{
    namespace Library
    {
        public class Event_Library
        {
            static Event_Library event_library = null;
            static public Event_Library GetInstance() => event_library ??= new Event_Library();

            public UnityEvent<string, object> OnEventScriptObject = new UnityEvent<string, object>();

        }
    }
}
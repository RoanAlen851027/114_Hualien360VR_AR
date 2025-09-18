/********************************
---------------------------------
著作者：RoanAlen
用途：事件資料 SO
---------------------------------
*********************************/
using UnityEngine;
namespace RoanAlen
{
    namespace Library
    {
        [CreateAssetMenu(fileName = "Event_ScriptObject", menuName = "RoanAlen_Library/EventScriptObject")]
        public class Event_ScriptObject : ScriptableObject
        {
            private readonly Event_Library eventLibrary = Event_Library.GetInstance();
            [Tooltip("此欄位為事件的識別字串")]
            public string msg = "";

            private void InvokeEvent(object send_Msg = null)
            {
                if (eventLibrary?.OnEventScriptObject != null)
                {
                    eventLibrary.OnEventScriptObject.Invoke(msg, send_Msg);
                }
                else
                {
                    Debug.LogWarning($"這個事件'{msg}' 找不到！請確認事件是否有註冊");
                }
            }

            public void SendEvent() => InvokeEvent();
            public void SendEvent(float sendMsg) => InvokeEvent(sendMsg);
            public void SendEvent(int sendMsg) => InvokeEvent(sendMsg);
            public void SendEvent(string msg) => InvokeEvent(msg);
            public void SendEvent(bool sendMsg) => InvokeEvent(sendMsg);
#if UNITY_EDITOR
            private void OnEnable()
            {
                string assetPath = UnityEditor.AssetDatabase.GetAssetPath(this);
                string assetName = System.IO.Path.GetFileNameWithoutExtension(assetPath);
                if (!string.IsNullOrEmpty(assetName) && string.IsNullOrEmpty(msg))
                {
                    msg = assetName;
                }
            }
#endif
        }

    }
}
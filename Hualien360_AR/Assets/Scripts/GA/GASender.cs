/*************************************************
  * 名稱：GASender
  * 作者：RyanHsu
  * 功能說明：
  * ***********************************************/

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[CreateAssetMenu(fileName = "GASender", menuName = "Custom/GASender")]
public class GASender : ScriptableObject
{
    public string eventName;
    public string eventLabel;

    public bool debug;

    public void SendGA()
    {
        // 如果 eventValue 是數字，保持原樣；如果是字符串，加上引號
        string eventParamsJson = $"{{\"{eventLabel}\":\"{eventName}\"}}";
        GAEventTracker.TrackEvent(eventName, eventParamsJson);
        if (debug)
        {
            Debug.Log($"Send::{eventParamsJson}");
        }
    }

#if UNITY_EDITOR

    [CustomEditor(typeof(GASender))]
    public class GASenderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var config = (GASender)target;
            var path = AssetDatabase.GetAssetPath(config);
            var fileName = System.IO.Path.GetFileNameWithoutExtension(path);

            if (!string.IsNullOrEmpty(fileName))
            {
                config.eventName = fileName;
            }
        }
    }
#endif
}

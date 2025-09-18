/********************************
---------------------------------
著作者：RoanAlen
用途：Inspector - 快捷上鎖 

% 表示 Ctrl（在 macOS 上是 Cmd）
# 表示 Shift
& 表示 Alt
_ 表示無修飾按鍵（單一按鍵）
---------------------------------
*********************************/
#if UNITY_EDITOR
using UnityEditor;
namespace RoanAlen
{
    namespace Editor
    {
        public class LockInspector
        {
            [MenuItem("RoanAlen/Lock Inspector %&A")]
            public static void Lock()
            {
                ActiveEditorTracker.sharedTracker.isLocked = !ActiveEditorTracker.sharedTracker.isLocked;
                ActiveEditorTracker.sharedTracker.ForceRebuild();
            }
        }

#endif
    }
}
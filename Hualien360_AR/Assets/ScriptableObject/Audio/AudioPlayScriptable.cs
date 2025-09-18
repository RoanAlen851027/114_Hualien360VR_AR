/*************************************************
  * 名稱：AudioPlayScriptable
  * 作者：RyanHsu
  * 功能說明：
  * ***********************************************/

using UnityEngine;

[CreateAssetMenu(fileName = "AudioPlayScriptable", menuName = "AudioPlayScriptable")]
public class AudioPlayScriptable : ScriptableObject
{
    DispatchEvent dispatcher = DispatchEvent.GetInstance();
    public AudioClip clip;

    public void Play()
    {
        if (clip != null)
        {
            dispatcher.OnPlayOneShot.Invoke(clip);
        }
    }

    public void BGM()
    {
        if (clip != null)
        {
            dispatcher.OnPlayBGM.Invoke(clip);
        }
    }

    static public void Stop()
    {
        DispatchEvent.GetInstance().OnPlayBGM.Invoke(null);
    }
}

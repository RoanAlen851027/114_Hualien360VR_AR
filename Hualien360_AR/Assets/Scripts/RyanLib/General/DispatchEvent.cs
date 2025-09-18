/*************************************************
  * 名稱：DispatchEvent
  * 作者：RyanHsu
  * 功能說明：事件集中管理
  * ***********************************************/
using UnityEngine;
using UnityEngine.Events;

public class DispatchEvent
{
    static DispatchEvent dispatcher = null;
    static public DispatchEvent GetInstance() => dispatcher ??= new DispatchEvent(); //Lazzy Initlize

    /// <summary>DispatchEvent延時Invoke</summary>
    public void DelayInvok<T>(float delayTime, UnityEvent<T> evt, T data)
    {
        MonoManager.DelayCall(delayTime, () => { evt?.Invoke(data); });
    }

    public UnityEvent<string, object> OnEventScriptObject = new UnityEvent<string, object>();

    /// <summary> MouseEvents </summary>
    public UnityEvent<Collider> OnPointerClick3D = new UnityEvent<Collider>();
    /// <summary> MouseEvents </summary>
    public UnityEvent<Collider> OnPointerDown3D = new UnityEvent<Collider>();

    /// <summary> AudioEvents </summary>
    public UnityEvent<AudioClip> OnPlayOneShot = new UnityEvent<AudioClip>();
    /// <summary> AudioEvents </summary>
    public UnityEvent<AudioClip> OnPlayBGM = new UnityEvent<AudioClip>();

    /// <summary> EffectEvents </summary>
    public UnityEvent<string> OnEffectCall = new UnityEvent<string>();

    /// <summary> LoadingEvents </summary>
    public UnityEvent<float> OnLoadingFadeOut = new UnityEvent<float>();

    public UnityEvent<Collider2D> OnCollider2D = new UnityEvent<Collider2D>();

    public UnityEvent OnGameCompleted = new UnityEvent();

    private DispatchEvent() { }//Singleton

}

public interface IDispatchEvent
{
    static DispatchEvent dispatcher;
}

public interface IPointerDown3D
{
    void OnPointerDown3D(Collider collider);
}

public interface IPointerClick3D
{
    void OnPointerClick3D(Collider collider);
}
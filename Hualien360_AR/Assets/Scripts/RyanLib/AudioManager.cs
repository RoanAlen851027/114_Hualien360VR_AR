/*************************************************
  * 名稱：AudioManager
  * 作者：RyanHsu
  * 功能說明：
  * ***********************************************/
using System;
using UnityEngine;

[Serializable]
public enum enum_AudioStatuses
{
    none,
    Playing,
    Stop,
}

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour, IDispatchEvent
{
    public static AudioManager _self = null;
    static enum_AudioStatuses statuses = enum_AudioStatuses.none;
    static public enum_AudioStatuses Statuses { get => statuses; }
    static enum_AudioStatuses _Statuses
    {
        set
        {
            if (value != statuses)
            {
                statuses = value;
                if (OnSwitch != null) OnSwitch.Invoke(statuses);
            }
        }
    }

    static public event Action<enum_AudioStatuses> OnSwitch;

    DispatchEvent dispatcher = DispatchEvent.GetInstance();
    [SerializeField, DisplayOnly] AudioSource m_AudioSource;

    AudioManager() { }//SingleTone

    void Awake()
    {
        //Singleton MonoBehaviours
        if (_self == null)
        {
            _self = this;
            statuses = enum_AudioStatuses.none;
            if (m_AudioSource == null) m_AudioSource = GetComponent<AudioSource>();
            dispatcher.OnPlayOneShot.AddListener(OnPlayOneShot);
            dispatcher.OnPlayBGM.AddListener(OnPlayBGM);
            //
            DontDestroyOnLoad(this);
        } else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        dispatcher.OnPlayOneShot.RemoveListener(OnPlayOneShot);
        dispatcher.OnPlayBGM.RemoveListener(OnPlayBGM);
        m_AudioSource = null;
    }

    void OnPlayOneShot(AudioClip clip)
    {
        if (clip != null)
        {
            if (m_AudioSource != null) m_AudioSource.PlayOneShot(clip);
        }
        clip = null;
    }
    void OnPlayBGM(AudioClip clip)
    {
        if (clip != null)
        {
            if (m_AudioSource != null)
            {
                _Statuses = enum_AudioStatuses.Playing;
                m_AudioSource.clip = clip;
                m_AudioSource.loop = true;
                m_AudioSource.Play();
            }
            clip = null;
        } else
        {
            _Statuses = enum_AudioStatuses.Stop;
            m_AudioSource.Stop();
        }
    }

    private void OnValidate()
    {
        if (m_AudioSource == null) m_AudioSource = GetComponent<AudioSource>();
    }
}
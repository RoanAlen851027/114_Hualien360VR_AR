/*************************************************
  * 名稱：EventMono
  * 作者：RyanHsu
  * 功能說明：
  * ***********************************************/
using System;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;

public class EventMono : MonoBehaviour
{
    [Serializable]
    public class TriggerEvent : UnityEvent { }

    [Serializable]
    public enum EventMonoType
    {
        OnAwake, OnStart, OnEnable, OnDisable, OnDestroy
    }

    [Serializable]
    public class Entry
    {
        public EventMonoType eventID = EventMonoType.OnAwake;
        public TriggerEvent callback = new TriggerEvent();
    }

    [SerializeField]
    private List<Entry> m_Delegates;

    protected EventMono()
    { }

    public List<Entry> triggers
    {
        get
        {
            if (m_Delegates == null)
                m_Delegates = new List<Entry>();
            return m_Delegates;
        }
        set { m_Delegates = value; }
    }

    private void Execute(EventMonoType id)
    {
        for (int i = 0; i < triggers.Count; ++i)
        {
            var ent = triggers[i];
            if (ent.eventID == id && ent.callback != null)
                ent.callback.Invoke();
        }
    }

    private void Awake()
    {
        CallAwake();
    }
    private void Start()
    {
        CallStart();
    }

    private void OnEnable()
    {
        CallEnable();
    }

    private void OnDisable()
    {
        CallDisable();
    }

    private void OnDestroy()
    {
        CallDestroy();
    }

    public virtual void CallAwake()
    {
        Execute(EventMonoType.OnAwake);
    }

    public virtual void CallStart()
    {
        Execute(EventMonoType.OnStart);
    }

    public virtual void CallEnable()
    {
        Execute(EventMonoType.OnEnable);
    }

    public virtual void CallDisable()
    {
        Execute(EventMonoType.OnDisable);
    }

    public virtual void CallDestroy()
    {
        Execute(EventMonoType.OnDestroy);
    }
}
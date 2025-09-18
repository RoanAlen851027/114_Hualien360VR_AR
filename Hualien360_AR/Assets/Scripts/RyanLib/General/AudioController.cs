/*************************************************
  * 名稱：AudioController
  * 作者：RyanHsu
  * 功能說明：
  * ***********************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioController : MonoBehaviour
{
    [Header("Depend")]
    public BooleanDispatcher booleanDispatcher;
    public AudioPlayScriptable bgm;
    bool locked = false;

    void OnEnable()
    {
        if (AudioManager.Statuses == enum_AudioStatuses.Playing)
        {
            SwitchIcon(true);
        } else
        {
            SwitchIcon(false);
        }
        AudioManager.OnSwitch += OnSwitch;
    }

    private void OnDisable()
    {
        AudioManager.OnSwitch -= OnSwitch;
    }

    void OnSwitch(enum_AudioStatuses audioStatuses)
    {
        SwitchIcon(audioStatuses == enum_AudioStatuses.Playing);
    }

    void SwitchIcon(bool io)
    {
        locked = true;
        booleanDispatcher.io = io;
        locked = false;
    }

    public void Audio(bool io)
    {
        if (locked) return;

        if (booleanDispatcher.enabled)
        {
            if (io) bgm.BGM();
            else AudioPlayScriptable.Stop();
        }
    }

#if UNITY_EDITOR
    //void OnValidate() { }
    //void OnDrawGizmos() { }
    //void OnDrawGizmosSelected() { }
#endif

}

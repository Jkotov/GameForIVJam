using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Settings : MonoBehaviour
{
    public static Settings Instance { get; private set; }
    public bool ShowSubtitles { get; set; } = true;

    public bool DisableVoice
    {
        get => _disableVoice;
        set
        {
            _disableVoice = value;
            VoiceStatus?.Invoke();
        }
    }

    private bool _disableVoice;
    public delegate void VoiceMuteHandler();
    [CanBeNull] public event VoiceMuteHandler VoiceStatus;
    public bool DisableMusic { set => BGAudio.Instance.Source.mute = value; }
    public Lang lang;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        
        DisableVoice = !DisableVoice;
        StartCoroutine(test());
    }

    IEnumerator test()
    {
        yield return new WaitForSeconds(1);
        DisableVoice = !DisableVoice;
    }
}

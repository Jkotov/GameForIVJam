using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Story : MonoBehaviour
{
    [SerializeField] private float timeoutBeforeFirstPhrase;
    [SerializeField] private float subtitlesTimeoutAppearing;
    [SerializeField] private Color subtitlesColor;
    private AudioSource _audioSource;
    [SerializeField] private TextMeshPro textBox;
    [Lang(lines = 3), SerializeField]
    private List<string> firstText;

    [Lang, SerializeField] private List<AudioClip> firstAudio;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.mute = Settings.Instance.DisableVoice;
        Settings.Instance.VoiceStatus += VoiceStatus;
        GameManager.Instance.Solved += EndText;
        StartCoroutine(FirstPhraseTimeout());
    }
    IEnumerator FirstPhraseTimeout()
    {
        yield return new WaitForSeconds(timeoutBeforeFirstPhrase);
        StartFirstPhrase();
    }

    IEnumerator TextBoxDoColor(Color endValue, float duration)
    {
        int framesCount = (int) (duration / Time.fixedDeltaTime);
        var deltaColor = (endValue - textBox.color) / framesCount;
        while (framesCount-- > 0)
        {
            textBox.color += deltaColor;
            yield return new WaitForFixedUpdate();
        }
        textBox.color = endValue;
    }
    private void StartFirstPhrase()
    {
        _audioSource.PlayOneShot(firstAudio[(int)Settings.Instance.lang]);
        textBox.text = firstText[(int)Settings.Instance.lang];
        if (Settings.Instance.ShowSubtitles)
            StartCoroutine(ShowSubtitles());
    }

    private void Awake()
    {
        textBox.color = Color.clear;
    }

    IEnumerator ShowSubtitles()
    {
        StartCoroutine(TextBoxDoColor(subtitlesColor, subtitlesTimeoutAppearing));
        yield return new WaitForSeconds(subtitlesTimeoutAppearing + firstAudio[(int)Settings.Instance.lang].length);
        StartCoroutine(TextBoxDoColor(Color.clear, subtitlesTimeoutAppearing));
    }
    private void VoiceStatus()
    {
        _audioSource.mute = Settings.Instance.DisableVoice;
    }

    private void EndText()
    {
        
    }
}

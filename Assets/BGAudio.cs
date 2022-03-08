using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGAudio : MonoBehaviour
{
    public static BGAudio Instance { get; private set; }
    public AudioSource Source;
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
        Source = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource MusicTrackSource;
    [SerializeField]
    private AudioSource AlarmSource;

    [SerializeField]
    private AudioClip MusicTrackClip1;
    [SerializeField]
    private AudioClip MusicTrackClip2;
    [SerializeField]
    private AudioClip MusicTrackClip3;
    [SerializeField]
    private AudioClip AlarmClip;
    [SerializeField]
    private AudioClip CountdownAudio;

    private void Awake()
    {
        GameManager.OnGameStateChange += GameManagerOnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChange -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.CountDown:
                AlarmSource.PlayOneShot(CountdownAudio);
                break;
            case GameState.IntensityLow:
                MusicTrackSource.PlayOneShot(MusicTrackClip1);
                break;
            case GameState.IntensityMedium:
                AlarmSource.PlayOneShot(AlarmClip);
                MusicTrackSource.Stop();
                MusicTrackSource.PlayOneShot(MusicTrackClip2);
                break;
            case GameState.IntensityHigh:
                AlarmSource.PlayOneShot(AlarmClip);
                MusicTrackSource.Stop();
                MusicTrackSource.PlayOneShot(MusicTrackClip3);
                break;
            case GameState.End:
                MusicTrackSource.Stop();
                break;
        }
    }

}

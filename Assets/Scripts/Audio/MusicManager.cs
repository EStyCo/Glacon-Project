using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class MusicManager : MonoBehaviour
{
    [Inject] private GameManager gameManager;

    [SerializeField] private AudioClip[] musicClips;
    List<AudioClip> recordClips = new List<AudioClip>();

    [SerializeField] private AudioSource musicSource;

    private float currentClipDuration;

    private void Start()
    {
        SmashClips();
    }
    private void SmashClips()
    {
        recordClips = new List<AudioClip>(musicClips);
        recordClips = recordClips.OrderBy(x => Guid.NewGuid()).ToList();

        PlayMusic();
    }

    public void PlayMusic()
    {
        musicSource.clip = recordClips.Last();
        currentClipDuration = musicSource.clip.length;

        musicSource.Play();
        StartCoroutine(DelayMusic());
    }

    IEnumerator DelayMusic()
    {
        yield return new WaitForSeconds(currentClipDuration);
        NextSong();
    }

    public void SetVolume(float volume)
    {
        musicSource.volume = volume;

        SoundManager.Instance.SetVolume(volume);
    }

    public void NextSong()
    {
        StopAllCoroutines();

        musicSource.Stop();

        if (recordClips.Count > 1)
        {
            recordClips.Remove(recordClips.Last());
            PlayMusic();
        }
        else SmashClips();
    }
}

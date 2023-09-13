using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    public AudioClip[] musicClips; // ������ � ��������� �������.
    List<AudioClip> recordClips = new List<AudioClip>(); 
    
    public AudioSource audioSource;

    private float currentClipDuration;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SmashClips();
    }
    private void SmashClips()
    {
        recordClips = new List<AudioClip>(musicClips);
        recordClips = recordClips.OrderBy(x => Guid.NewGuid()).ToList();

        PlayMusic();
    } // �������������� �� ������� � ������ � ��������������.
    public void PlayMusic()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = recordClips.Last();

        currentClipDuration = audioSource.clip.length;

        audioSource.Play();
        StartCoroutine(DelayMusic());
    } // ������������ �����.

    IEnumerator DelayMusic()
    { 
        yield return new WaitForSeconds(currentClipDuration);
        NextSong();
    } // ������ ������������ �����.

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;

        SoundManager.Instance.SetVolume(volume);
    }
    public void NextSong()
    {
        StopAllCoroutines();

        audioSource.Stop();

        if (recordClips.Count > 1)
        {
            recordClips.Remove(recordClips.Last());
            PlayMusic();
        }
        else SmashClips();
    }
}

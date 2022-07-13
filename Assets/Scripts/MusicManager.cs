using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class MusicManager : MonoBehaviour
{
    private static MusicManager _instance;

    public static MusicManager Instance
    {
        get { return _instance; }
    }

    private AudioSource _audioSource;
    public AudioClip[] songs;
    public float volume;
    private float _trackTimer;
    private float _songsPlayed;
    private bool[] _beenPlayed;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(_instance);
        }
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        _beenPlayed = new bool[songs.Length];
        if(!_audioSource.isPlaying)
            ChangeSong(UnityEngine.Random.Range(0,songs.Length));
    }

    private void Update()
    {
        _audioSource.volume = volume;
        if (_audioSource.isPlaying)
            _trackTimer += 1 * Time.deltaTime;
        if (!_audioSource.isPlaying || _trackTimer >= _audioSource.clip.length || Input.GetKeyDown(KeyCode.Space))
            ChangeSong(UnityEngine.Random.Range(0, songs.Length));

        ResetShuffle();
    }

    

    public void ChangeSong(int songPicked)
    {
        if (!_beenPlayed[songPicked])
        {
            _trackTimer = 0;
            _songsPlayed++;
            _beenPlayed[songPicked] = true;
            _audioSource.clip = songs[songPicked];
            _audioSource.Play();
        }
        else
        {
            _audioSource.Stop();
        }
    }
    private void ResetShuffle()
    {
        if (_songsPlayed == songs.Length)
        {
            _songsPlayed = 0;
            for (int i = 0; i < songs.Length; i++)
            {
                if (i == songs.Length)
                    break;
                else
                    _beenPlayed[i] = false;
            }
        }
    }

}

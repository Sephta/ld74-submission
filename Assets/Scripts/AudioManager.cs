using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] public List<AudioClip> _musicClips = new List<AudioClip>();
    [SerializeField] public List<AudioClip> _sfxClips = new List<AudioClip>();
    [SerializeField, ReadOnly] public List<AudioSource> _musicSources = new List<AudioSource>();
    [SerializeField, ReadOnly] public List<AudioSource> _sfxSources = new List<AudioSource>();

    [Header("Instance Data")]
    public static AudioManager _instance;
    private bool gameStart = false;

    [SerializeField, ReadOnly] public bool battleMusic = false;
    
    void Awake()
    {
        if (!gameStart)
        {
            _instance = this;
            DontDestroyOnLoad(_instance);
            gameStart = true;
        }
    }

    void Start()
    {
        // Initialize all audio clips with an associated source
        for (int i = 0; i < _musicClips.Count; i++)
        {
            _musicSources.Add(new AudioSource());
            _musicSources[i] = gameObject.AddComponent<AudioSource>();
            _musicSources[i].playOnAwake = false;
            _musicSources[i].clip = _musicClips[i];
        }

        for (int i = 0; i < _sfxClips.Count; i++)
        {
            _sfxSources.Add(new AudioSource());
            _sfxSources[i] = gameObject.AddComponent<AudioSource>();
            _sfxSources[i].playOnAwake = false;
            _sfxSources[i].clip = _sfxClips[i];
        }
    }

    void Update()
    {
        
    }

    // void FixedUpdate() {}

    public void PlaySFX(int index)
    {
        // avoid index out of range exception
        if (index > (_sfxClips.Count - 1))
            return;

        _sfxSources[index].volume = GameSettings._instance._sfxVolume;

        _sfxSources[index].PlayOneShot(_sfxSources[index].clip);
    }

    public void PlayMusic(AudioClip toPlay)
    {
        if (GameSettings._instance == null)
            return;

        foreach (AudioSource source in _musicSources)
        {
            if (source.isPlaying)
            {
                source.Stop();
            }
        }

        foreach (AudioSource source in _musicSources)
        {
            if (source.clip == toPlay)
            {
                if (!source.isPlaying)
                {
                    source.Play();
                    source.volume = GameSettings._instance._musicVolume;
                    source.loop = true;
                }
            }
        }
    }

    public void PlayBattleMusic(int index)
    {
        if (index > (_musicClips.Count - 1))
            return;

        _musicSources[index].Play();
        _musicSources[index].volume = GameSettings._instance._musicVolume;
        _musicSources[index].loop = true;
        battleMusic = true;
    }

    public void StopBattleMusic(int index)
    {
        if (_musicSources[index].isPlaying)
            _musicSources[index].Stop();
        battleMusic = false;
    }
}

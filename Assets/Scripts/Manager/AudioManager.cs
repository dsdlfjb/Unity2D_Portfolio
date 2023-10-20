using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region 싱글턴
    public static AudioManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        Init();
    }
    #endregion

    [Header("[ BGM ]")]
    public AudioClip _bgmClip;
    public float _bgmVolume;
    AudioSource _bgmPlayer;
    AudioHighPassFilter _bgmEffect;

    [Header("[ SFX ]")]
    public AudioClip[] _sfxClips;
    public float _sfxVolume;
    public int _channels;
    AudioSource[] _sfxPlayers;
    int _channelIdx;
    
    public enum ESfx { Dead, Hit, LevelUp = 3, Lose, Melee, Range = 7,  Select, Win }

    void Init()
    {
        // 배경음 플레이어 초기화

        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        _bgmPlayer = bgmObject.AddComponent<AudioSource>();
        _bgmPlayer.playOnAwake = false;
        _bgmPlayer.loop = true;
        _bgmPlayer.volume = _bgmVolume;
        _bgmPlayer.clip = _bgmClip;

        // 효과음 플레이어 초기화

        GameObject sfxObject = new GameObject("sfxPlayer");
        sfxObject.transform.parent = transform;
        _sfxPlayers = new AudioSource[_channels];

        for (int idx = 0; idx < _sfxPlayers.Length; idx++)
        {
            _sfxPlayers[idx] = sfxObject.AddComponent<AudioSource>();
            _sfxPlayers[idx].playOnAwake = false;
            _sfxPlayers[idx].bypassListenerEffects = true;
            _sfxPlayers[idx].volume = _sfxVolume;
        }
    }
    
    public void PlayBGM(bool isPlay)
    {
        if (isPlay)
            _bgmPlayer.Play();
        else
            _bgmPlayer.Stop();
    }

    public void EffectBGM(bool isPlay)
    {
        return;
        _bgmEffect.enabled = isPlay;
    }

    public void PlaySFX (ESfx eSfx)
    {
        for (int idx = 0; idx < _sfxPlayers.Length; idx++)
        {
            int loopIdx = (idx + _channelIdx) % _sfxPlayers.Length;

            if (_sfxPlayers[loopIdx].isPlaying)
                continue;

            int rndIdx = 0;

            if (eSfx == ESfx.Hit || eSfx == ESfx.Melee)
                rndIdx = Random.Range(0, 2);

            _channelIdx = loopIdx;
            _sfxPlayers[loopIdx].clip = _sfxClips[(int)eSfx];
            _sfxPlayers[loopIdx].Play();
            break;
        }
        _sfxPlayers[0].clip = _sfxClips[(int)eSfx];
        _sfxPlayers[0].Play();
        
    }
}

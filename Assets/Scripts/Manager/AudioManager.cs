using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region �̱���
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

    [Header("[ SFX ]")]
    public AudioClip[] _sfxClips;
    public float _sfxVolume;
    public int _channels;
    AudioSource[] _sfxPlayers;
    int _channelIdx;
    
    public enum ESfx { Dead, Hit, LevelUp = 3, Lose, Melee, Range = 7,  Select, Win }

    void Init()
    {
        // ����� �÷��̾� �ʱ�ȭ

        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        _bgmPlayer = bgmObject.AddComponent<AudioSource>();
        _bgmPlayer.playOnAwake = false;
        _bgmPlayer.loop = true;
        _bgmPlayer.volume = _bgmVolume;
        _bgmPlayer.clip = _bgmClip;

        // ȿ���� �÷��̾� �ʱ�ȭ

        GameObject sfxObject = new GameObject("sfxPlayer");
        sfxObject.transform.parent = transform;
        _sfxPlayers = new AudioSource[_channels];

        for (int idx = 0; idx < _sfxPlayers.Length; idx++)
        {
            _sfxPlayers[idx] = sfxObject.AddComponent<AudioSource>();
            _sfxPlayers[idx].playOnAwake = false;
            _sfxPlayers[idx].volume = _sfxVolume;
        }
    }
    
    public void PlaySFX (ESfx eSfx)
    {
        _sfxPlayers[0].clip = _sfxClips[(int)eSfx];
        _sfxPlayers[0].Play();
    }
}

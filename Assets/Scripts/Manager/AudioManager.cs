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

    [Header("[ SFX ]")]
    public AudioClip[] _sfxClips;
    public float _sfxVolume;
    public int _channels;
    AudioSource[] _sfxPlayers;
    int _channelIdx;
    
    void Init()
    {
        // 배경음 플레이어 초기화
        GameObject bgmObject = new GameObject("BgmPlayer");
        _bgmPlayer.transform.parent = transform;
        // 효과음 플레이어 초기화

    }
}

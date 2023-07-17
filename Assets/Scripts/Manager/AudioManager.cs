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
    
    void Init()
    {
        // ����� �÷��̾� �ʱ�ȭ
        GameObject bgmObject = new GameObject("BgmPlayer");
        _bgmPlayer.transform.parent = transform;
        // ȿ���� �÷��̾� �ʱ�ȭ

    }
}

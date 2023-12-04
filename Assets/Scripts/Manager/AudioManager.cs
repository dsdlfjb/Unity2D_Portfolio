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
            Init();
            DontDestroyOnLoad(gameObject);
        }

        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    #endregion

    public enum ESfx { Dead, Hit, LevelUp = 3, Lose, Melee, Range = 7, Select, Win }

    [Header("===== BGM =====")]
    public AudioClip _bgmClip;
    public float _bgmVolume;
    AudioSource _bgmPlayer;
    AudioHighPassFilter _bgmEffect;

    [Header("===== SFX =====")]
    public AudioClip[] _sfxClips;
    public float _sfxVolume;
    public int _channels;
    AudioSource[] _sfxPlayers;
    int _channelIndex;

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
        _bgmEffect = Camera.main.GetComponent<AudioHighPassFilter>();

        // ȿ���� �÷��̾� �ʱ�ȭ
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        _sfxPlayers = new AudioSource[_channels];

        // for�� ���鼭 ��� ȿ���� ����� �ҽ� �����ϸ鼭 ����
        for (int i = 0; i < _sfxPlayers.Length; i++)
        {
            _sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            _sfxPlayers[i].playOnAwake = false;
            _sfxPlayers[i].bypassListenerEffects = true;
            _sfxPlayers[i].volume = _sfxVolume;
        }
    }

    public void PlayBgm(bool isPlay)
    {
        if (isPlay)
            _bgmPlayer.Play();

        else
            _bgmPlayer.Stop();
    }

    public void EffectBgm(bool isPlay)
    {
        _bgmEffect.enabled = isPlay;
    }

    public void PlaySfx(ESfx eSfx)
    {
        for (int i = 0; i < _sfxPlayers.Length; i++)
        {
            int loopIndex = (i + _channelIndex) % _sfxPlayers.Length;

            if (_sfxPlayers[loopIndex].isPlaying)
                continue;

            // ȿ������ 2�� �̻��� ���� ���� �ε����� ���ϱ�
            int ranIndex = 0;
            if (eSfx == ESfx.Hit || eSfx == ESfx.Melee)
            {
                ranIndex = Random.Range(0, 2);
            }

            _channelIndex = loopIndex;
            _sfxPlayers[loopIndex].clip = _sfxClips[(int)eSfx + ranIndex];
            // ����� �ҽ��� Ŭ���� �����ϰ� Play �Լ��� ȣ��
            _sfxPlayers[loopIndex].Play();
            break;
        }
    }
}
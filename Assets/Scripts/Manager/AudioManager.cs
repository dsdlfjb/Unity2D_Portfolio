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

    public float _masterVolume;
    public float _bgmVolume;
    public float _sfxVolume;

    [Header("===== BGM =====")]
    public AudioClip[] _bgmClip;
    AudioSource _bgmPlayer;
    //AudioHighPassFilter _bgmEffect;

    [Header("===== SFX =====")]
    public AudioClip[] _sfxClips;
    public int _channels;
    AudioSource[] _sfxPlayers;
    int _channelIndex;

    void Init()
    {
        _masterVolume = PlayerPrefs.GetFloat("Master");
        _bgmVolume = PlayerPrefs.GetFloat("BGM") * _masterVolume;
        _sfxVolume = PlayerPrefs.GetFloat("SFX") * _masterVolume;

        // 배경음 플레이어 초기화
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        _bgmPlayer = bgmObject.AddComponent<AudioSource>();
        _bgmPlayer.playOnAwake = true;
        _bgmPlayer.loop = true;
        _bgmPlayer.volume = _bgmVolume;
        //_bgmEffect = Camera.main.GetComponent<AudioHighPassFilter>();

        // 효과음 플레이어 초기화
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        _sfxPlayers = new AudioSource[_channels];

        // for문 돌면서 모든 효과음 오디오 소스 생성하면서 저장
        for (int i = 0; i < _sfxPlayers.Length; i++)
        {
            _sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            _sfxPlayers[i].playOnAwake = false;
            _sfxPlayers[i].bypassListenerEffects = true;
            _sfxPlayers[i].volume = _sfxVolume;
        }
    }

    public void ChangeVolume()
    {
        _masterVolume = PlayerPrefs.GetFloat("Master");
        _bgmVolume = PlayerPrefs.GetFloat("BGM") * _masterVolume;
        _sfxVolume = PlayerPrefs.GetFloat("SFX") * _masterVolume;

        _bgmPlayer.volume = _bgmVolume;

        for (int i = 0; i < _sfxPlayers.Length; i++)
        {
            _sfxPlayers[i].volume = _sfxVolume;
        }
    }

    public void PlayBgm(bool isPlay)
    {
        if (isPlay)
        {
            _bgmPlayer.clip = _bgmClip[Managers.Instance.CurrentStageLevel];
            _bgmPlayer.Play();
        }
        else
            _bgmPlayer.Stop();
    }

    /*
    public void EffectBgm(bool isPlay)
    {
        _bgmEffect.enabled = isPlay;
    }
    */

    public void PlaySfx(ESfx eSfx)
    {
        for (int i = 0; i < _sfxPlayers.Length; i++)
        {
            int loopIndex = (i + _channelIndex) % _sfxPlayers.Length;

            if (_sfxPlayers[loopIndex].isPlaying)
                continue;

            // 효과음이 2개 이상인 것은 랜덤 인덱스를 더하기
            int ranIndex = 0;
            if (eSfx == ESfx.Hit || eSfx == ESfx.Melee)
            {
                ranIndex = Random.Range(0, 2);
            }

            _channelIndex = loopIndex;
            _sfxPlayers[loopIndex].clip = _sfxClips[(int)eSfx + ranIndex];
            // 오디오 소스의 클립을 변경하고 Play 함수를 호출
            _sfxPlayers[loopIndex].Play();
            break;
        }
    }
}
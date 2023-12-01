using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region ╫л╠шео
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
    }

    #endregion

    [Header("===== Audio Sorce =====")]
    [SerializeField] AudioSource _musicSource;
    [SerializeField] AudioSource _SFXSource;

    [Header("===== Audio Clip =====")]
    public AudioClip _background;
    public AudioClip _death;        //O
    public AudioClip _playerHit;
    public AudioClip _enemyHit;     //O
    public AudioClip _levelUp;
    public AudioClip _lose;     //O
    public AudioClip _win;      //O
    public AudioClip _select;
    public AudioClip _melee;
    public AudioClip _range;

    private void Start()
    {
        _musicSource.clip = _background;
        _musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        _SFXSource.PlayOneShot(clip);
    }
}
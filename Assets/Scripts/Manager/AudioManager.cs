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
    [SerializeField] AudioClip _background;
    [SerializeField] AudioClip _death;
    [SerializeField] AudioClip _playerHit;
    [SerializeField] AudioClip _enemyHit;
    [SerializeField] AudioClip _levelUp;
    [SerializeField] AudioClip _lose;
    [SerializeField] AudioClip _win;
    [SerializeField] AudioClip _select;
    [SerializeField] AudioClip _melee;
    [SerializeField] AudioClip _range;

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
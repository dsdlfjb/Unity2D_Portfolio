using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class OptionManager : MonoBehaviour
{
    [SerializeField] AudioMixer _myMixer;
    [SerializeField] Slider _masterSlider;
    [SerializeField] Slider _bgmSlider;
    [SerializeField] Slider _sfxSlider;

    private void Start()
    {
        SetMusicVolume();    
    }

    public void SetMusicVolume()
    {
        float masterVolume = _masterSlider.value;
        float bgmVolume = _bgmSlider.value;
        float sfxVolume = _sfxSlider.value;

        _myMixer.SetFloat("Master", Mathf.Log10(masterVolume) * 20);
        _myMixer.SetFloat("BGM", Mathf.Log10(bgmVolume) * 20);
        _myMixer.SetFloat("SFX", Mathf.Log10(sfxVolume) * 20);
    }
}

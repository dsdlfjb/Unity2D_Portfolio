using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class OptionManager : MonoBehaviour
{
    [SerializeField] AudioMixer _myMixer;
    [SerializeField] Slider _masterSlider;
    [SerializeField] Slider _bgmSlider;
    [SerializeField] Slider _sfxSlider;

    private void Start()
    {
        LoadMusicVolume();    
    }

    public void LoadMusicVolume()
    {
        //float masterVolume = _masterSlider.value;
        //float bgmVolume = _bgmSlider.value;
        //float sfxVolume = _sfxSlider.value;

        //_myMixer.SetFloat("Master", Mathf.Log10(masterVolume) * 20);
        //_myMixer.SetFloat("BGM", Mathf.Log10(bgmVolume) * 20);
        //_myMixer.SetFloat("SFX", Mathf.Log10(sfxVolume) * 20);

        _masterSlider.value = PlayerPrefs.GetFloat("Master"); //����� ������ ���� �� ����
        _bgmSlider.value = PlayerPrefs.GetFloat("BGM"); //����� ��� ���� ���� �� ����
        _sfxSlider.value = PlayerPrefs.GetFloat("SFX"); //����� ȿ���� ���� �� ����
    }

    public void SetMasterVolume() //�ǽð����� ������ ���� �� ���� (0.0f ~ 1.0f)
    {
        PlayerPrefs.SetFloat("Master", _masterSlider.value);

        AudioManager.Instance.ChangeVolume();
    }

    public void SetBGMVolume() //�ǽð����� ��� ���� ���� �� ���� (0.0f ~ 1.0f)
    {
        PlayerPrefs.SetFloat("BGM", _bgmSlider.value);

        AudioManager.Instance.ChangeVolume();
    }

    public void SetSFXVolume()//�ǽð����� ȿ���� ���� �� ���� (0.0f ~ 1.0f)
    {
        PlayerPrefs.SetFloat("SFX", _sfxSlider.value);

        AudioManager.Instance.ChangeVolume();
    }

    public void Move_To_Main() //����ȭ�� ������
    {
        AudioManager.Instance.PlaySfx(AudioManager.ESfx.Select);
        SceneManager.LoadScene(1);
    }
}

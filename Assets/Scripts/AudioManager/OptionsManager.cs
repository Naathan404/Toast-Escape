using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        LoadSlider();
    }

    public void SetMusicVolume()
    {
        float musicVolume = musicSlider.value;
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
    }

    public void SetSFXVolume()
    {
        float sfxVolume = sfxSlider.value;
        audioMixer.SetFloat("SfxVolume", Mathf.Log10(sfxVolume) * 20);
        PlayerPrefs.SetFloat("SfxVolume", sfxVolume);
    }

    private void LoadSlider()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SfxVolume", 1f);
    }

    public void OnResetButtonClick()
    {
        PlayerPrefs.SetInt("HighScore", 0);
        PlayerPrefs.SetInt("HadPlayed", 0);
        AudioManager.instance.PlaySFX(AudioManager.instance.alert);
    }
}

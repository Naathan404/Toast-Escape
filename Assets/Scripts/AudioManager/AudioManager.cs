using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private float fadeOutDuration;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip backgroundMusic;
    public AudioClip hitCoinSFX;
    public AudioClip hitScoreSFX;
    public AudioClip hitBombSFX;
    public AudioClip powerUp;
    public AudioClip clickButton;
    public AudioClip destroyObstacle;
    public AudioClip powerTimeOut;
    public AudioClip alert;
    public AudioClip fillerSfx;
    public AudioClip shopTabChange;
    public AudioClip cancle;
    public AudioClip accept;

    private float lastSFXTime = -1f;
    private float sfxCoolDown = 0.03f;

    private void Start()
    {
        SetVolume();
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip sfx)
    {
        if (Time.unscaledTime - lastSFXTime >= sfxCoolDown)
        {
            SFXSource.PlayOneShot(sfx);
            lastSFXTime = Time.unscaledTime;
        }
    }

    public void FadeOutMusic()
    {
        StartCoroutine(FadeOutMusicCoroutine());
    }

    private IEnumerator FadeOutMusicCoroutine()
    {
        float startVolume = musicSource.volume;

        float time = 0f;
        while (time < fadeOutDuration)
        {
            time += Time.unscaledDeltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, 0f, time / fadeOutDuration);
            yield return null;
        }

        musicSource.Stop();
        musicSource.volume = startVolume;
    }

    public void SetVolume()
    {
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float sfxVolume = PlayerPrefs.GetFloat("SfxVolume", 1f);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);
        audioMixer.SetFloat("SfxVolume", Mathf.Log10(sfxVolume) * 20);
    }

    public void PauseMusic()
    {
        if(musicSource.isPlaying)
            musicSource.Pause();
    }

    public void UnPauseMusic()
    {   
        if(!musicSource.isPlaying)
            musicSource.UnPause();
    }
}

using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private SoundLibrary sfxLibrary;
    [SerializeField] private AudioSource sfx2DSource;

    [Header("Audio Mixer")]
    public AudioMixer audioMixer;

    [Header("Music")]
    public AudioClip mainMenuMusic;
    public AudioClip gameMusic;
    private AudioSource musicSource;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
        if (audioMixer != null)
            musicSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Music")[0];
    }

    void Start()
    {
        float savedMusic = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        float savedSFX = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
        SetMusicVolume(savedMusic);
        SetSFXVolume(savedSFX);
    }

    // ---- Sound methods ----

    public void PlaySound3D(AudioClip clip, Vector3 pos)
    {
        if (clip != null)
            AudioSource.PlayClipAtPoint(clip, pos);
    }

    public void PlaySound3D(string soundName, Vector3 pos)
    {
        PlaySound3D(sfxLibrary.GetClipFromName(soundName), pos);
    }

    public void PlaySound2D(string soundName)
    {
        sfx2DSource.PlayOneShot(sfxLibrary.GetClipFromName(soundName));
    }

    // ---- Music methods ----

    public void PlayMainMenuMusic()
    {
        if (musicSource.clip != mainMenuMusic || !musicSource.isPlaying)
        {
            musicSource.clip = mainMenuMusic;
            musicSource.Play();
        }
        musicSource.volume = 1f;
    }

    public void PlayGameMusic()
    {
        if (musicSource.clip != mainMenuMusic || !musicSource.isPlaying)
        {
            musicSource.clip = mainMenuMusic;
            musicSource.Play();
        }
        musicSource.volume = 0.3f;
    }

    // ---- Volume control ----

    public void SetMusicVolume(float value)
    {
        float db = value > 0.001f ? Mathf.Log10(value) * 20 : -80f;
        audioMixer.SetFloat("MusicVolume", db);
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void SetSFXVolume(float value)
    {
        float db = value > 0.001f ? Mathf.Log10(value) * 20 : -80f;
        audioMixer.SetFloat("SFXVolume", db);
        PlayerPrefs.SetFloat("SFXVolume", value);
    }

    public float GetMusicVolume() => PlayerPrefs.GetFloat("MusicVolume", 0.75f);
    public float GetSFXVolume() => PlayerPrefs.GetFloat("SFXVolume", 0.75f);
}
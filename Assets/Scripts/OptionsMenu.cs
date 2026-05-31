using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    void OnEnable()
    {
        if (SoundManager.Instance != null)
        {
            musicSlider.value = SoundManager.Instance.GetMusicVolume();
            sfxSlider.value = SoundManager.Instance.GetSFXVolume();
        }
    }

    public void OnMusicSliderChanged(float value)
    {
        if (SoundManager.Instance != null)
            SoundManager.Instance.SetMusicVolume(value);
    }

    public void OnSFXSliderChanged(float value)
    {
        if (SoundManager.Instance != null)
            SoundManager.Instance.SetSFXVolume(value);
    }
}
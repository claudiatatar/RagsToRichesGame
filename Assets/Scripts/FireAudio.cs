using UnityEngine;

public class FireAudio : MonoBehaviour
{
    public float maxHearDistance = 10f;
    private AudioSource audioSource;

    void Start()
    {
        AudioClip clip = SoundManager.Instance != null
            ? FindObjectOfType<SoundLibrary>().GetClipFromName("FireCrackle")
            : null;

        if (clip == null) return;

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.spatialBlend = 1f;
        audioSource.maxDistance = maxHearDistance;
        audioSource.rolloffMode = AudioRolloffMode.Linear;

        // Route through SFX mixer group
        if (SoundManager.Instance != null && SoundManager.Instance.GetComponent<AudioSource>() != null)
            audioSource.outputAudioMixerGroup = 
                SoundManager.Instance.GetComponent<AudioSource>().outputAudioMixerGroup;

        audioSource.Play();
    }
}
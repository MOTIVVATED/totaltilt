using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class MusicVolumeApplier : MonoBehaviour
{
    private AudioSource audioSource;

    private static MusicVolumeApplier instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        { Destroy(gameObject); return; }
        
        DontDestroyOnLoad(gameObject);
            
        audioSource = GetComponent<AudioSource>();
        Apply();
    }
    private void OnEnable()
    {
        Apply();
    }
    public void Apply()
    {
        float volume = 1f;

        if (SettingsManager.Instance != null)
            volume = SettingsManager.Instance.musiVolume;
        else
            volume = PlayerPrefs.GetFloat("settings_music", 1f);

        audioSource.volume = Mathf.Clamp01(volume);
    }
}

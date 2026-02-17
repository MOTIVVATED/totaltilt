using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MusicVolumeApplier : MonoBehaviour
{
    private AudioSource audioSource;
    private bool subscribed;
    //private static MusicVolumeApplier instance;


    private void Awake()
    {
        //if (instance != null && instance != this)
        //{ Destroy(gameObject); return; }
        
        //DontDestroyOnLoad(gameObject);
            
        audioSource = GetComponent<AudioSource>();
        ApplyFromPrefs();

        //Apply();
    }
    private void Start()
    {
        StartCoroutine(SubscribeWhenReady() );
    }
    private IEnumerator SubscribeWhenReady()
    {
        while(SettingsManager.Instance == null)
            yield return null;

        if (subscribed) yield break;

        SettingsManager.Instance.OnChanged += ApplyFromSettings;
        subscribed = true;
    }
    //private void OnEnable()
    //{
    //    if (SettingsManager.Instance != null)
    //        SettingsManager.Instance.OnChanged += ApplyFromSettings;
    //}
    private void OnDisable()
    {
        if (SettingsManager.Instance != null)
            SettingsManager.Instance.OnChanged -= ApplyFromSettings;
    }
    private void ApplyFromSettings()
    {
        audioSource.volume = SettingsManager.Instance.musicVolume;
        Debug.Log("[Music Apply volume = " + audioSource.volume);
    }
    private void ApplyFromPrefs()
    {
        float v = PlayerPrefs.GetFloat(SettingsManager.MusicKey, 1f);
        audioSource.volume = v;
        Debug.Log("[Music] Apply from prefs volume = " + audioSource.volume);
    }
    public void Apply()
    {
        float volume = 1f;

        if (SettingsManager.Instance != null)
            volume = SettingsManager.Instance.musicVolume;
        else
            volume = PlayerPrefs.GetFloat("settings_music", 1f);

        audioSource.volume = Mathf.Clamp01(volume);
    }
}

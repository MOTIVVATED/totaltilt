using UnityEngine;
using System;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }

    private const string MusicKey = "settings_music";
    private const string SfxKey = "settings_sfx";
    private const string DarkModeKey = "settings_darkmode";

    public event Action OnChanged;

    public float musicVolume { get; private set; } = 1f;
    public float sfxVolume { get; private set; } = 1f;
    public bool darkModeOn { get; private set; } = true;

    private void Awake()
    {
        Debug.Log("[Settings] Awake");

        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        Load();
    }

    public void SetMusic(float value)
    {
        musicVolume = Mathf.Clamp01(value);
        PlayerPrefs.SetFloat(MusicKey, musicVolume);
        PlayerPrefs.Save();

        // add audioManager later!!!
        // adding =)
        //var applier = FindFirstObjectByType<MusicVolumeApplier>();
        //if (applier != null) applier.Apply();

        Debug.Log("[Settings] Saved");
        Debug.Log("[Settings] PlayerPrefs value = " + PlayerPrefs.GetFloat("settings_music", -1f));
        OnChanged?.Invoke();
    }
    public void SetSfx(float value)
    {
        sfxVolume = Mathf.Clamp01(value);
        PlayerPrefs.SetFloat(SfxKey, sfxVolume);
        PlayerPrefs.Save();
        OnChanged?.Invoke();
    }
    public void SetDarkMode(bool on)
    {
        darkModeOn = on;
        PlayerPrefs.SetInt(DarkModeKey, on ? 1 : 0);
        PlayerPrefs.Save();
        OnChanged?.Invoke();
    }
    public void ResetToDefaults()
    {
        SetMusic(1.0f);
        SetSfx(1.0f);
        SetDarkMode(true);
    }
    private void Load()
    {
        musicVolume = PlayerPrefs.GetFloat(MusicKey, 1f);
        sfxVolume = PlayerPrefs.GetFloat(SfxKey, 1f);
        darkModeOn = PlayerPrefs.GetInt(DarkModeKey, 1) == 1;

        Debug.Log($"[Settings] Load music={musicVolume} sfx={sfxVolume} vibro={darkModeOn}");
    }
}

using UnityEngine;
using System;
[DefaultExecutionOrder(0)]
public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }

    public const string MusicKey = "settings_music";
    public const string SfxKey = "settings_sfx";
    private const string DarkModeKey = "settings_darkmode";

    public event Action OnMusicChanged;
    public event Action OnSfxChanged;
    public event Action OnDarkModeChanged;

    public float musicVolume { get; private set; } = 1f;
    public float sfxVolume { get; private set; } = 1f;
    public bool darkModeOn { get; private set; } = true;

    private void Awake()
    {
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

        OnMusicChanged?.Invoke();
    }
    public void SetSfx(float value)
    {
        sfxVolume = Mathf.Clamp01(value);
        PlayerPrefs.SetFloat(SfxKey, sfxVolume);
        PlayerPrefs.Save();
        OnSfxChanged?.Invoke();
    }
    public void SetDarkMode(bool on)
    {
        darkModeOn = on;
        PlayerPrefs.SetInt(DarkModeKey, on ? 1 : 0);
        PlayerPrefs.Save();
        OnDarkModeChanged?.Invoke();
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
    }
}

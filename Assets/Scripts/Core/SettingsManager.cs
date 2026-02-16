using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }

    private const string MusicKey = "settings_music";
    private const string SfxKey = "settings_sfx";
    private const string DarkModeKey = "settings_darkmode";

    public float musiVolume { get; private set; } = 1.0f;
    public float sfxVolume { get; private set; } = 1.0f;
    public bool darkMode { get; private set; } = true;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        Load();
        Debug.Log("[Settings] Awake");
    }

    public void SetMusic(float value)
    {
        musiVolume = Mathf.Clamp01(value);
        PlayerPrefs.SetFloat(MusicKey, musiVolume);
        PlayerPrefs.Save();

        // add audioManager later!!!

        // adding =)
        var applier = FindFirstObjectByType<MusicVolumeApplier>();
        if (applier != null) applier.Apply();

        Debug.Log("[Settings] Saved");
        Debug.Log("[Settings] PlayerPrefs value = " + PlayerPrefs.GetFloat("settings_music", -1f));

    }
    public void SetSfx(float value)
    {
        sfxVolume = Mathf.Clamp01(value);
        PlayerPrefs.SetFloat(SfxKey, sfxVolume);
        PlayerPrefs.Save();
    }
    public void SetDarkMode(bool on)
    {
        darkMode = on;
        PlayerPrefs.SetInt(DarkModeKey, on ? 1 : 0);
        PlayerPrefs.Save();
    }
    public void ResetToDefaults()
    {
        SetMusic(1.0f);
        SetSfx(1.0f);
        SetDarkMode(true);
    }
    private void Load()
    {
        musiVolume = PlayerPrefs.GetFloat(MusicKey, 1f);
        sfxVolume = PlayerPrefs.GetFloat(SfxKey, 1f);
        darkMode = PlayerPrefs.GetInt(DarkModeKey, 1) == 1;
    }
}

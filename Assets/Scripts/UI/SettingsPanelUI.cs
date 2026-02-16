using UnityEngine;
using UnityEngine.UI;

public class SettingsPanelUI : MonoBehaviour
{
    [Header("Root")]
    [SerializeField] private GameObject panelRoot;

    [Header("UI")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Toggle darkModeToggle;

    private bool ignoreEvents;

    private void OnEnable()
    {
        SyncFromSettings();
    }
    public void Open()
    {
        panelRoot.SetActive(true);
        SyncFromSettings();
    }
    public void Close()
    {
        panelRoot.SetActive(false);
    }
    public void OnMusicChanged(float value)
    {
        if (ignoreEvents) return;
        SettingsManager.Instance.SetMusic(value);
        Debug.Log("[Settings] Music slider changed: " + value);
    }
    public void OnSfxChanged(float value)
    {
        if (ignoreEvents) return;
        SettingsManager.Instance.SetSfx(value);
    }
    public void OnDarkModeChanged(bool on)
    {
        if (ignoreEvents) return;
        SettingsManager.Instance.SetDarkMode(on);
    }
    public void OnResetClicked()
    {
        SettingsManager.Instance.ResetToDefaults();
        SyncFromSettings();
    }
    private void SyncFromSettings()
    {
        if (SettingsManager.Instance == null) return;

        ignoreEvents = true;

        musicSlider.value = SettingsManager.Instance.musiVolume;
        sfxSlider.value = SettingsManager.Instance.sfxVolume;

        if (darkModeToggle != null)
        {
            darkModeToggle.isOn = SettingsManager.Instance.darkMode;
        }
        ignoreEvents = false;
    }
}

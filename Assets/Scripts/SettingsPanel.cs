using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Audio;

public class SettingsPanel : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject mainMenuPanel;
    
    [Header("Audio Settings")]
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioMixer audioMixer;
    
    [Header("Graphics Settings")]
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;
    
    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;
    private float currentRefreshRate;

    private void Start()
    {
        // Setup audio
        if (volumeSlider != null && audioMixer != null)
        {
            float savedVolume = PlayerPrefs.GetFloat("Volume", 1f);
            volumeSlider.value = savedVolume;
            audioMixer.SetFloat("MasterVolume", Mathf.Log10(savedVolume) * 20);
            
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        }

        // Setup resolutions
        if (resolutionDropdown != null)
        {
            SetupResolutions();
        }

        // Setup fullscreen
        if (fullscreenToggle != null)
        {
            fullscreenToggle.isOn = Screen.fullScreen;
            fullscreenToggle.onValueChanged.AddListener(OnFullscreenToggled);
        }
    }

    private void SetupResolutions()
    {
        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();
        currentRefreshRate = Screen.currentResolution.refreshRate;

        // Filter resolutions to only include those with the current refresh rate
        foreach (Resolution resolution in resolutions)
        {
            if (resolution.refreshRate == currentRefreshRate)
            {
                filteredResolutions.Add(resolution);
            }
        }

        // Create resolution options
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            string option = $"{filteredResolutions[i].width}x{filteredResolutions[i].height} @ {filteredResolutions[i].refreshRateRatio.numerator}/{filteredResolutions[i].refreshRateRatio.denominator} Hz";
            options.Add(option);

            if (filteredResolutions[i].width == Screen.width && 
                filteredResolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.onValueChanged.AddListener(OnResolutionChanged);
    }

    private void OnVolumeChanged(float volume)
    {
        if (audioMixer != null)
        {
            audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("Volume", volume);
        }
    }

    private void OnResolutionChanged(int index)
    {
        if (filteredResolutions != null && index < filteredResolutions.Count)
        {
            Resolution resolution = filteredResolutions[index];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }
    }

    private void OnFullscreenToggled(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void OnCancelClicked()
    {
        if (mainMenuPanel != null)
        {
            gameObject.SetActive(false);
            mainMenuPanel.SetActive(true);
        }
    }
} 
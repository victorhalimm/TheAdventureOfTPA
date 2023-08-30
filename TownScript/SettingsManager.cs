using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    public AudioMixer mainMixer;

    private Resolution[] resolutions;

    public TMP_Dropdown resolutionDropdown;

    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject settingUI;

    private void Start()
    {
        resolutions = Screen.resolutions;
        // hapus opsi lama (mastiin sesuai sama kondisi devicenya)
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currResIdx = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string availableRes = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(availableRes);
            // dapetin index resolusi yang sekarang dipake
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currResIdx = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currResIdx;
        resolutionDropdown.RefreshShownValue();
    }

    public void closeSetting()
    {
        menuUI.SetActive(true);
        settingUI.SetActive(false);

    }
    public void setFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void setResolution(int resIdx)
    {
        Resolution resolution = resolutions[resIdx];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void setVolume(float volumeValue)
    {
        mainMixer.SetFloat("Volume", volumeValue);
    }
}

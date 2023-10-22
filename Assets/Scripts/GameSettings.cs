using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class GameSettings : MonoBehaviour
{
    [SerializeField] MasterScript gameMaster;

    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private TMP_Dropdown qualityDropdown;


    private void Start()
    {
        SetMusicVolume(gameMaster.currentMusicVolume);
        SetSfxVolume(gameMaster.currentSfxVolume);
        SetQuality(gameMaster.currentQualitySettingsIndex);

        if (musicSlider != null)
        {
            musicSlider.value = gameMaster.currentMusicVolume;
        }

        if (sfxSlider != null)
        {
            sfxSlider.value = gameMaster.currentSfxVolume;
        }

        if (qualityDropdown != null)
        {
            qualityDropdown.value = gameMaster.currentQualitySettingsIndex;
        }
    }

    private void OnDisable()
    {
        gameMaster.SaveGame();
    }


    public void SetMusicVolume(float volume)
    {
        //Debug.Log(volume);

        audioMixer.SetFloat("musicVolume", volume);
        gameMaster.currentMusicVolume = volume;
    }


    public void SetSfxVolume(float volume)
    {
        audioMixer.SetFloat("sfxVolume", volume);
        gameMaster.currentSfxVolume = volume;
    }


    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        gameMaster.currentQualitySettingsIndex = qualityIndex;
    }


    public void SetControlScheme(int controlIndex)
    {
        gameMaster.selectedTouchControllerIndex = controlIndex;
    }


    public void SaveSettings()
    {
        gameMaster.SaveGame();
    }
}

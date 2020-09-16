using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Slider musicSlider = default;
    [SerializeField] private Slider sfxSlider = default;

    [Header("Read-Only")]
    [SerializeField] private SoundData sliderData;

    private SoundManager soundManager;

    private void Awake() {
        soundManager = SoundManager.Instance;
    }

    private void Start() {
        sliderData = SaveManager.LoadSoundData();

        musicSlider.value = sliderData.MusicVolume;
        sfxSlider.value = sliderData.SfxVolume;
    }

    public void UpdateMusicVolume(float sliderValue) {
        sliderData.MusicVolume = sliderValue;
        UpdateVolumes();
    }

    public void UpdateSfxVolume(float sliderValue) {
        sliderData.SfxVolume = sliderValue;
        UpdateVolumes();
    }

    public void UpdateVolumes() {
        soundManager.UpdateVolumeLevels(sliderData);
    }

    public void SaveSettings() {
        SaveManager.SaveSoundData(sliderData);
    }
}

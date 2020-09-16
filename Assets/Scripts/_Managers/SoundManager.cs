using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : Singleton<SoundManager> {

    [Header("Audio Mixer Groups")]
    [SerializeField] private AudioMixer mainAudioMixer = default;

    [Header("Music & Sounds")]
    [SerializeField] private Sound[] sounds = default;

    [Header("Read-Only")]
    [SerializeField] private SoundData storedData;

    private void Awake() {
        if(Instance != this) {
            Destroy(gameObject);
            return;
        }

        foreach(Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.outputAudioMixerGroup = s.audioMixerGroup;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        DontDestroyOnLoad(this);
    }

    private void Start() {
        storedData = SaveManager.LoadSoundData();

        // Initialize saved sound volumes
        UpdateVolumeLevels(storedData);
    }

    public void PlaySound(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null) {
            return;
        }

        s.source.Play();
    }

    public void StopSound(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null) {
            return;
        }

        s.source.Stop();
    }

    public void UpdateVolumeLevels(SoundData sliderData) {
        mainAudioMixer.SetFloat("MusicVol", Mathf.Log10(sliderData.MusicVolume) * 20f);
        mainAudioMixer.SetFloat("SFXVol", Mathf.Log10(sliderData.SfxVolume) * 20f);
    }
}

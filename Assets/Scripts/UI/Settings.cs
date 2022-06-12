using System;
using Infrastructure.Services;
using Services.PersistentProgress;
using Services.SaveLoad;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI
{
    public class Settings : MonoBehaviour
    {
        [SerializeField] private MainMenu _mainMenu;
        [SerializeField] private Button _back;
        [SerializeField] private Toggle _mute;
        [SerializeField] private Slider _volume;
        [SerializeField] private AudioMixerGroup _mixer;
        
        private const string Master = "MasterVolume"; 
        private const string Music = "MusicVolume";
        private const string Effects = "EffectsVolume";
        private const string UI = "UIVolume";

        private const float MaxValue = 0;
        private const float MinValue = -80;
        private void OnEnable()
        {
            _back.onClick.AddListener(OnBackButton);
            _mute.onValueChanged.AddListener(OnMuteChanged);
            _volume.onValueChanged.AddListener(OnVolumeChanged);
        }

        private void OnDisable()
        {
            _back.onClick.RemoveListener(OnBackButton);
            _mute.onValueChanged.RemoveListener(OnMuteChanged);
            _volume.onValueChanged.RemoveListener(OnVolumeChanged);
        }

        private void OnVolumeChanged(float value)
        {
            _mixer.audioMixer.SetFloat(Master, Mathf.Lerp(MinValue, MaxValue, value));
        }

        private void OnMuteChanged(bool enable)
        {
            _mixer.audioMixer.SetFloat(Music, enable ? MinValue : MaxValue);
            _mixer.audioMixer.SetFloat(Effects, enable ? MinValue : MaxValue);
            _mixer.audioMixer.SetFloat(UI, enable ? MinValue : MaxValue);
        }

        private void OnBackButton()
        {
            SaveSoundSettings();
            HideSettings();
            ShowMainMenu();
        }

        private void SaveSoundSettings()
        {
            IPersistentProgressService persistentProgressService = ServiceLocator.Container.Single<IPersistentProgressService>();
            persistentProgressService.Settings.SoundSettings.Mute = _mute.isOn ? 1 : 0;
            persistentProgressService.Settings.SoundSettings.Volume = _volume.value;
            ServiceLocator.Container.Single<ISaveLoadService>().SaveSettings();
        }

        private void ShowMainMenu()
        {
            _mainMenu.gameObject.SetActive(true);
        }

        private void HideSettings()
        {
            gameObject.SetActive(false);
        }
    }
}

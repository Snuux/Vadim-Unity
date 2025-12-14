using UnityEngine.Audio;
using UnityEngine.Rendering;

public class AudioHandler
{
    private const float _offVolumeValue = -80f;
    private const float _onVolumeValue = 0f;

    private const string _musicVolumeKey = "MusicVolume";
    private const string _soundsVolumeKey = "SoundsVolume";

    private AudioMixer _audioMixer;

    public AudioHandler(AudioMixer audioMixer)
    {
        _audioMixer = audioMixer;
    }

    public bool IsMusicOn()
    {
        _audioMixer.GetFloat(_musicVolumeKey, out float volume);
        return volume > _offVolumeValue;
    }
    public bool IsSoundsOn()
    {
        _audioMixer.GetFloat(_musicVolumeKey, out float volume);
        return volume > _offVolumeValue;
    }

    public void SetMusicVolumeOn() => SetMusicVolume(_onVolumeValue);

    public void SetMusicVolumeOff() => SetMusicVolume(_offVolumeValue);

    public void SetSoundsVolumeOn() => SetSoundsVolume(_onVolumeValue);

    public void SetSoundsVolumeOff() => SetSoundsVolume(_offVolumeValue);

    public void SetMusicVolume(float volume) => _audioMixer.SetFloat(_musicVolumeKey, volume);

    public void SetSoundsVolume(float volume) => _audioMixer.SetFloat(_soundsVolumeKey, volume);

}

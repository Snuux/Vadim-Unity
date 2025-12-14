using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private List<Mine> _mines;
    [SerializeField] private AudioSource _mineAudioSource;

    private AudioHandler _audioHandler; 

    private void Awake()
    {
        _audioHandler = new AudioHandler(_audioMixer);
    }
    private void LateUpdate()
    {
        foreach (Mine mine in _mines)
            if (mine.IsExploded)
                _mineAudioSource.Play();
    }

    public void SwitchMusicOnOff()
    {
        if (_audioHandler.IsMusicOn())
            _audioHandler.SetMusicVolumeOff();
        else
            _audioHandler.SetMusicVolumeOn();
    }

    public void SwitchSoundsOnOff()
    {
        if (_audioHandler.IsSoundsOn())
            _audioHandler.SetSoundsVolumeOff();
        else
            _audioHandler.SetSoundsVolumeOn();
    }
}

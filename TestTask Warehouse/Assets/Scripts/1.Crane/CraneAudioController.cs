using UnityEngine;
using System;

public class CraneAudioController : MonoBehaviour
{
    [SerializeField] private CraneController _controller;

    [SerializeField] private AudioSource _hookAudioSource;
    [SerializeField] private AudioSource _trolleyAudioSource;
    [SerializeField] private AudioSource _beamAudioSource;
    private AudioSource _activesource;


    private bool _isPlayingAudio = false;

    private void Start()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        _controller.Operating += HandleOperating;
        _controller.StopOperating += StopAudio;
    }

    private void StopAudio()
    {
        if(_activesource != null)
            _activesource.Stop();
        
        _activesource = null;
        _isPlayingAudio = false;
    }

    private void HandleOperating(CraneEngineTypes types, Vector3 direction)
    {
        if (!_isPlayingAudio)
        {
            _isPlayingAudio = true;
            switch (types)
            {
                case CraneEngineTypes.Hook:
                    _activesource = _hookAudioSource;
                    _activesource.Play();
                    break;

                case CraneEngineTypes.Trolley:
                    _activesource = _trolleyAudioSource;
                    _activesource?.Play();
                    break;

                case CraneEngineTypes.Beam:
                    _activesource = _beamAudioSource;
                    _activesource?.Play();
                    break;

                default: throw new ArgumentException(nameof(types));
            }
        }

    }

    private void OnDestroy()
    {
        _controller.Operating -= HandleOperating;
        _controller.StopOperating -= StopAudio;
    }

}

using UnityEngine;

public class AnalyzerAudioController : MonoBehaviour
{
    [SerializeField] private GazAnalyzer _controller;

    [SerializeField] private AudioSource _alarmAudioSource;

    private bool _isPlayingAudio = false;

    public void InitializeAnalyzerAudioController(GazAnalyzer gazAnalyzer)
    {
        _controller = gazAnalyzer;
        Subscribe();
    }

    private void Subscribe()
    {
        _controller.OnZoneEnter += HandleOperating;
        _controller.OnZoneExit += StopAudio;
    }

    private void StopAudio()
    {
        if (_isPlayingAudio)
        {
            _alarmAudioSource.Stop();
            _isPlayingAudio = false;
        }
    }

    private void HandleOperating()
    {
        if (!_isPlayingAudio)
        {
            _isPlayingAudio=true;
            _alarmAudioSource.Play();
        }

    }
}

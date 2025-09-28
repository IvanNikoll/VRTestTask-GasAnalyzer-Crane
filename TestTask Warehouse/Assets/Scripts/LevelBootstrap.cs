using UnityEngine;

public class LevelBootstrap : MonoBehaviour
{
    [SerializeField] private RemoteController _remoteController;
    [SerializeField] private CraneController _craneController;
    [SerializeField] private MovableSettings _craneSettings;
    [SerializeField] private GazAnalyzerSettings _gazAnalyzerSettings;
    [SerializeField] private GazAnalyzer _gazAnalyzer;
    [SerializeField] private GazAnalyzerView _AnalyzerView;

    private void Start()
    {
        _craneController.InitializeCrane(_remoteController, _craneSettings);
        _gazAnalyzer.InitializeAnalyzer(_AnalyzerView, _gazAnalyzerSettings);
    }
}

using UnityEngine;

public class LevelBootstrap : MonoBehaviour
{
    [SerializeField] private RemoteController _remoteController;
    [SerializeField] private CraneController _craneController;
    [SerializeField] private MovableStats _craneSettings;

    private void Start()
    {
        _craneController.InitializeCrane(_remoteController, _craneSettings);
    }
}

using System.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Runtime.CompilerServices;
using System.Collections.Generic;


public class LevelBootstrap : MonoBehaviour
{
    [SerializeField] private LevelSettings _levelSettings;
    [SerializeField] private MovableSettings _craneSettings;
    [SerializeField] private GazAnalyzerSettings _gazAnalyzerSettings;
    [SerializeField] private AssetReference _beamCranePrefab;
    [SerializeField] private AssetReference _craneControllerPrefab;
    [SerializeField] private AssetReference _gasAnalyzerPrefab;
    [SerializeField] private AssetReference _probePrefab;
    [SerializeField] private AssetReference _wirePrefab;
    [SerializeField] private AssetReference _warehousePrefab;
    [SerializeField] private AssetReference _dangerObjectPrefab;
    private AnalyzerAudioController _analizerAudioController;

    private async void Start()
    {
        await Spawn(_warehousePrefab, _levelSettings.WarehousePosition);
        await Task.Yield();
        SpawnDangerObjects();
        await SpawnCraneAndController();
        await SpawnAnalyzer();
        
    }

    private async void SpawnDangerObjects()
    {
        var tasks = new List<Task>();
        foreach (var pos in _levelSettings.DangerObjectPosition)
        {
            tasks.Add(Spawn(_dangerObjectPrefab, pos));
        }

        await Task.WhenAll(tasks);
    }

    private async Task SpawnAnalyzer()
    {
        GameObject gasAnalyzerObjObj = await Spawn(_gasAnalyzerPrefab, _levelSettings.GasAnalyzerPosition);
        GazAnalyzerView gasAnalyzer = gasAnalyzerObjObj.GetComponent<GazAnalyzerView>();
        if (gasAnalyzer == null)
            throw new Exception(nameof(gasAnalyzer));
        _analizerAudioController = gasAnalyzerObjObj.GetComponent<AnalyzerAudioController>();
        if (_analizerAudioController == null) throw new Exception(nameof(_analizerAudioController));

        GameObject probeObj = await Spawn(_probePrefab, _levelSettings.ProbePosition);
        GazAnalyzer probe = probeObj.GetComponent<GazAnalyzer>();
        if (probe == null) throw new Exception(nameof(probe));
        probe.InitializeAnalyzer(gasAnalyzer, _gazAnalyzerSettings);
        _analizerAudioController.InitializeAnalyzerAudioController(probe);
        GameObject wireObj = await Spawn(_wirePrefab, Vector3.zero);
        CableLine cableLine = wireObj.GetComponent<CableLine>();
        if (cableLine == null) throw new Exception(nameof(cableLine));
        cableLine.Initialize(gasAnalyzer.Socket.transform, probe.Socket.transform);
    }

    private async Task SpawnCraneAndController()
    {
        GameObject craneControllerObj = await Spawn(_craneControllerPrefab, _levelSettings.CraneControllerPosition);
        RemoteController remoteController = craneControllerObj.GetComponent<RemoteController>();
        if (remoteController == null)
            throw new Exception(nameof (remoteController));

        GameObject craneObj = await Spawn(_beamCranePrefab, _levelSettings.BeamCranePosition);
        CraneController crane = craneObj.GetComponent<CraneController>();
        if (crane == null)
            throw new Exception(nameof (crane));

        crane.InitializeCrane(remoteController, _craneSettings);
    }

    private async Task<GameObject> Spawn(AssetReference reference, Vector3 position)
    {
        var handle = reference.InstantiateAsync(position, Quaternion.identity);
        await handle.Task;

        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            throw new Exception($"Failed to spawn addressable: {reference.RuntimeKey}");
        }

        return handle.Result;
    }
}

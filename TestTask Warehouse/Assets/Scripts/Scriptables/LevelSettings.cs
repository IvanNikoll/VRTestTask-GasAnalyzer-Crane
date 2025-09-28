using UnityEngine;

[CreateAssetMenu(fileName = "LevelSettings", menuName = "Scriptables/Settings/LevelSettings")]
public class LevelSettings : ScriptableObject
{
    [SerializeField] private Vector3 _warehousePosition;
    [SerializeField] private Vector3 _beamCranePosition;
    [SerializeField] private Vector3 _craneControllerPosition;
    [SerializeField] private Vector3 _gasAnalyzerPosition;
    [SerializeField] private Vector3 _probePosition;
    [SerializeField] private Vector3 _dangerObjectposition;

    public Vector3 WarehousePosition { get { return _warehousePosition; } }
    public Vector3 BeamCranePosition { get {return _beamCranePosition; } }
    public Vector3 CraneControllerPosition { get { return _craneControllerPosition;} }
    public Vector3 GasAnalyzerPosition { get { return _gasAnalyzerPosition;} }
    public Vector3 ProbePosition { get { return _probePosition; } }
    public Vector3 DangerObjectPosition { get { return _dangerObjectposition; } }
}

using UnityEngine;

[CreateAssetMenu (fileName = "MovableStats", menuName = "Scriptables/Stats/MovableStats")]
public class MovableStats : ScriptableObject
{
    [SerializeField] private float _hookSpeed;
    [SerializeField] private float _trolleySpeed;
    [SerializeField] private float _beamSpeed;
    [SerializeField] private float _hookMaxpos;
    [SerializeField] private float _hookMinpos;
    [SerializeField] private float _trolleyMaxPos;
    [SerializeField] private float _trolleyMinPos;
    [SerializeField] private float _beamMaxPos;
    [SerializeField] private float _beamMinPos;

    public float HookSpeed { get { return _hookSpeed; } }
    public float TrolleySpeed { get { return _trolleySpeed; }}
    public float BeamSpeed { get { return _beamSpeed; }}
    public float HookMaxPos { get { return _hookMaxpos; }}
    public float HookMinPos { get { return _hookMinpos; }}
    public float TrolleyMaxPos { get { return _trolleyMaxPos;} }
    public float TrolleyMinPos { get { return _trolleyMinPos; }}
    public float BeamMaxPos { get { return _beamMaxPos; }}
    public float BeamMinPos { get {return _beamMinPos; }}

}

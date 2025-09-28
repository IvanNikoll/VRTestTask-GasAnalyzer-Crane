using UnityEngine;

[CreateAssetMenu(fileName = "AnalyzerSettings", menuName = "Scriptables/Settings/AnalyzerSettings")]

public class GazAnalyzerSettings : ScriptableObject
{
    [SerializeField] private Color _onColor;
    [SerializeField] private Color _offColor;
    [SerializeField] private float _powerDelayInterval;
    [SerializeField] private float _fadeDuration;
    
    public Color OnColor { get { return _onColor; }}
    public Color OffColor { get { return _offColor; }}
    public float PowerDelayInterval { get { return _powerDelayInterval; }}
    public float FadeDuration { get { return _fadeDuration; }}
}

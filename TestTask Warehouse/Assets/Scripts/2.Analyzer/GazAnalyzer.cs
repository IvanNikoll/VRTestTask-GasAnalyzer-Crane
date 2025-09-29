using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazAnalyzer : MonoBehaviour
{
    [SerializeField] private GameObject _socket;

    public event Action OnZoneEnter;
    public event Action OnZoneExit;

    public GameObject Socket {  get { return _socket; } }

    private GazAnalyzerView _view;
    private GameObject _screen;
    private Canvas _screenCanvas;
    private Coroutine _powerCoroutine;
    private Transform _nearestDangerZone;
    private readonly List<Transform> _dangerZones = new List<Transform>();

    private float _powerDelayInterval;
    private float _currentDistance;
    private float _fadeDuration = 0.5f;
    private bool _isOn;

    private Color _onColor;
    private Color _offColor;

    public void InitializeAnalyzer(GazAnalyzerView view, GazAnalyzerSettings settings)
    {
        _view = view;
        _screen = view.Screen;
        _screenCanvas = view.Canvas;
        _powerDelayInterval = settings.PowerDelayInterval;
        _fadeDuration = settings.FadeDuration;
        _onColor = settings.OnColor;
        _offColor = settings.OffColor;
        Subscribe();
        LocateDangerZones();
    }

    public void Update()
    {
        if (_isOn)
            UpdateDangerZoneDistance();
    }

    private void LocateDangerZones()
    {
        _dangerZones.Clear();
        var zones = FindObjectsByType<DangerZone>(FindObjectsSortMode.None);
        foreach (var zone in zones)
        {
            if (zone != null)
                _dangerZones.Add(zone.transform);
        }

    }

    private void UpdateDangerZoneDistance()
    {
        if (_dangerZones.Count == 0)
            return;

        float minDist = Mathf.Infinity;
        Transform closest = null;

        foreach (var zone in _dangerZones)
        {
            if (zone != null)
            {
                float dist = Vector3.Distance(transform.position, zone.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    closest = zone;
                }

            }
        }

        _nearestDangerZone = closest;
        _currentDistance = minDist;

        if (_nearestDangerZone != null)
        {
            string content = $"{_currentDistance:F2} m";
            _view.Show(content);
            IndicateDanger(_currentDistance);
        }
    }

    private void IndicateDanger(float distance)
    {
        if(distance <= 10)
        {
            OnZoneEnter?.Invoke();
            _view.Zone1Indicator.SetActive(true);
            _view.Zone2Indicator.SetActive(false);
            _view.Zone3Indicator.SetActive(false);
            if(distance <= 5)
                _view.Zone2Indicator.SetActive(true);
                _view.Zone3Indicator.SetActive(false);
                if (distance <= 2)
                    _view.Zone3Indicator.SetActive(true);
            return;
        }

        OnZoneExit?.Invoke();
        _view.Zone1Indicator.SetActive(false);
        _view.Zone2Indicator.SetActive(false);
        _view.Zone3Indicator.SetActive(false);
    }

    private void Subscribe()
    {
        _view.PowerButtonPressed += OnPowerPressed;
    }

    private void OnPowerPressed()
    {
        if (_powerCoroutine == null)
            _powerCoroutine = StartCoroutine(PowerCoroutine());
    }

    private void OnDestroy()
    {
        if (_view != null)
            _view.PowerButtonPressed -= OnPowerPressed;
    }

    private IEnumerator PowerCoroutine()
    {
        float time = 0f;

        while (_view.IsPressed && time < _powerDelayInterval)
        {
            time += Time.deltaTime;
            yield return null;
        }

        if (time >= _powerDelayInterval)
        {
            _isOn = !_isOn;
            StartCoroutine(ScreenCoroutine(_isOn));
            Debug.Log(_isOn ? "Power ON" : "Power OFF");
        }

        _powerCoroutine = null;
    }

    private IEnumerator ScreenCoroutine(bool turnOn)
    {
        if (!turnOn)
            _screenCanvas.gameObject.SetActive(false);
        var renderer = _screen.GetComponent<Renderer>();
        var materials = renderer.materials;
        var lightMat = materials[1];

        Color startColor = lightMat.color;
        Color targetColor = turnOn ? _onColor : _offColor;

        float time = 0f;

        while (time < _fadeDuration)
        {
            time += Time.deltaTime;
            float t = time / _fadeDuration;

            lightMat.color = Color.Lerp(startColor, targetColor, t);
            
            yield return null;
        }

        _screenCanvas.gameObject.SetActive(turnOn);
        lightMat.color = targetColor;
    }
}

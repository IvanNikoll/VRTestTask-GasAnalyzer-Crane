using System;
using TMPro;
using UnityEngine;

public class GazAnalyzerView : MonoBehaviour
{
    [SerializeField] private GameObject _screen;
    [SerializeField] private Canvas _screenCanvas;
    [SerializeField] private TextMeshProUGUI _distanceText;
    [SerializeField] private GameObject _socket;
    [SerializeField] private GameObject _zone1Image;
    [SerializeField] private GameObject _zone2Image;
    [SerializeField] private GameObject _zone3Image;

    public event Action PowerButtonPressed;
    public GameObject Socket {  get { return _socket; } }

    public GameObject Screen {  get { return _screen; } }
    public Canvas Canvas { get { return _screenCanvas; } }
    public GameObject Zone1Indicator {  get { return _zone1Image; } }
    public GameObject Zone2Indicator {  get { return _zone2Image; } }
    public GameObject Zone3Indicator {  get { return _zone3Image; } }

    public bool IsPressed;

    public void PressPowerButton()
    {
        IsPressed = true;
        PowerButtonPressed?.Invoke();
    }

    public void StopPressingButton()
    {
        IsPressed = false;
    }

    public void Show(string text)
    {
        _distanceText.text = text;
    }
}

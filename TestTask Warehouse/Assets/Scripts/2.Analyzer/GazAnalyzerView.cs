using System;
using TMPro;
using UnityEngine;

public class GazAnalyzerView : MonoBehaviour
{
    [SerializeField] private GameObject _screen;
    [SerializeField] private Canvas _screenCanvas;
    [SerializeField] private TextMeshProUGUI _distanceText;
    [SerializeField] private GameObject _socket;

    public event Action PowerButtonPressed;
    public GameObject Socket {  get { return _socket; } }

    public GameObject Screen {  get { return _screen; } }
    public Canvas Canvas { get { return _screenCanvas; } }

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

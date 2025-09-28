using TMPro;
using UnityEngine;

public class Hint : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _hintText;

    private void Awake()
    {
        if (_hintText == null)
            _hintText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ShowHint(string message)
    {
        _hintText.text = message;
        _hintText.gameObject.SetActive(true);
    }

    public void HideHint()
    {
        _hintText.text = "";
        _hintText.gameObject.SetActive(false);
    }
}

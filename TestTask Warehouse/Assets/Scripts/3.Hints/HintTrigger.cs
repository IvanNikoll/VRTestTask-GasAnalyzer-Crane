using UnityEngine;

public class HintTrigger : MonoBehaviour
{
    [SerializeField] private string _hintText;
    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent<Hint>(out Hint player);
        if (player != null)
            player.ShowHint(_hintText);
    }

    private void OnTriggerExit(Collider other)
    {
        other.TryGetComponent<Hint>(out Hint player);
        if (player != null)
            player.HideHint();
    }
}
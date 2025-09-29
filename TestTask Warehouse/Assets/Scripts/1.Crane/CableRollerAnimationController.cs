using UnityEngine;


public class CableRollerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _cableRollAnimator;
    [SerializeField] private CraneController _craneController;

    private bool _isAnimating = false;

    private void Start()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        _craneController.Operating += HandleOperating;
        _craneController.StopOperating += StopAnimations;
    }

    private void StopAnimations()
    {
        _isAnimating = false;
        _cableRollAnimator.SetBool("IsHookDown", false);
        _cableRollAnimator.SetBool("IsHookUP", false);
    }

    private void HandleOperating(CraneEngineTypes operationType, Vector3 vector)
    {
        if (!_isAnimating)
        {
            switch (operationType)
            {
                case CraneEngineTypes.Hook:
                    _isAnimating = true;
                    AnimateHook(vector);
                    break;
                default:
                    return;
            }

        }

    }

    private void AnimateHook(Vector3 direction)
    {
        if(direction == Vector3.down)
        {
            _isAnimating = true;
            _cableRollAnimator.SetBool("IsHookDown", true);

        }
        if (direction == Vector3.up) 
        {
            _isAnimating = true;
            _cableRollAnimator.SetBool("IsHookUP", true);

        }

    }

    private void OnDestroy()
    {
        _craneController.Operating -= HandleOperating;
        _craneController.StopOperating -= StopAnimations;
    }

}

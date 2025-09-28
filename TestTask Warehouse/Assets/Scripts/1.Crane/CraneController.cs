using System.Collections;
using UnityEngine;

public class CraneController : MonoBehaviour
{    
    [SerializeField] private GameObject _beam;
    [SerializeField] private GameObject _trolley;
    [SerializeField] private GameObject _hook;
    
    private RemoteController _remoteController;
    private Coroutine _currentRoutine;

    private float _hookSpeed;
    private float _trolleySpeed;
    private float _beamSpeed;
    private float _hookMaxpos;
    private float _hookMinpos;
    private float _trolleyMaxPos;
    private float _trolleyMinPos;
    private float _beamMaxPos;
    private float _beamMinPos;

    public void InitializeCrane(RemoteController remoteController, MovableSettings settings)
    {
        _remoteController = remoteController;
        _hookSpeed = settings.HookSpeed;
        _trolleySpeed = settings.TrolleySpeed;
        _beamSpeed = settings.BeamSpeed;
        _hookMaxpos = settings.HookMaxPos;
        _hookMinpos = settings.HookMinPos;
        _beamMaxPos = settings.BeamMaxPos;
        _beamMinPos = settings.BeamMinPos;
        _trolleyMaxPos = settings.TrolleyMaxPos;
        _trolleyMinPos = settings.TrolleyMinPos;

        Subscribe();
    }

    private void Subscribe()
    {
        _remoteController.OnMoveUp += StartMoveUp;
        _remoteController.OnMoveDown += StartMoveDown;
        _remoteController.OnMoveForward += StartMoveForward;
        _remoteController.OnMoveBackward += StartMoveBackward;
        _remoteController.OnMoveLeft += StartMoveLeft;
        _remoteController.OnMoveRight += StartMoveRight;
    }

    private void OnDisable()
    {
        _remoteController.OnMoveUp -= StartMoveUp;
        _remoteController.OnMoveDown -= StartMoveDown;
        _remoteController.OnMoveForward -= StartMoveForward;
        _remoteController.OnMoveBackward -= StartMoveBackward;
        _remoteController.OnMoveLeft -= StartMoveLeft;
        _remoteController.OnMoveRight -= StartMoveRight;
    }

    private void StartMoveUp() => StartMoveRoutine(Vector3.up);
    private void StartMoveDown() => StartMoveRoutine(Vector3.down);
    private void StartMoveForward() => StartMoveRoutine(Vector3.forward);
    private void StartMoveBackward() => StartMoveRoutine(Vector3.back);
    private void StartMoveLeft() => StartMoveRoutine(Vector3.left);
    private void StartMoveRight() => StartMoveRoutine(Vector3.right);

    private void StartMoveRoutine(Vector3 direction)
    {
        if (_currentRoutine != null)
            StopCoroutine(_currentRoutine);

        if (direction == Vector3.up || direction == Vector3.down)
            _currentRoutine = StartCoroutine(MoveHook(direction));

        if (direction == Vector3.forward || direction == Vector3.back)
            _currentRoutine = StartCoroutine(MoveBeam(direction));

        if (direction == Vector3.left || direction == Vector3.right)
            _currentRoutine = StartCoroutine(MoveTrolley(direction));

    }

    private IEnumerator MoveHook(Vector3 direction)
    {
        while (_remoteController.IsPressed)
        {
            float axisPos = _hook.transform.localPosition.y;
            if (direction == Vector3.up && axisPos >= _hookMaxpos)
                break;
            if (direction == Vector3.down && axisPos <= _hookMinpos)
                break;

            _hook.transform.Translate(direction * Time.deltaTime * _hookSpeed);
            yield return null;

        }

        _currentRoutine = null;
    }

    private IEnumerator MoveTrolley(Vector3 direction)
    {
        while (_remoteController.IsPressed)
        {
            float axisPos = _trolley.transform.localPosition.x;

            if (direction == Vector3.right && axisPos >= _trolleyMaxPos)
                break;
            if (direction == Vector3.left && axisPos <= _trolleyMinPos)
                break;

            _trolley.transform.Translate(direction * Time.deltaTime * _trolleySpeed);
            yield return null;

        }

        _currentRoutine = null;
    }
    private IEnumerator MoveBeam(Vector3 direction)
    {
        while (_remoteController.IsPressed)
        {
            float axisPos = _beam.transform.position.z;
            if (direction == Vector3.forward && axisPos >= _beamMaxPos)
                break;
            if (direction == Vector3.back && axisPos <= _beamMinPos)
                break;

            _beam.transform.Translate(direction * Time.deltaTime * _beamSpeed);
            yield return null;

        }

        _currentRoutine = null;
    }
}

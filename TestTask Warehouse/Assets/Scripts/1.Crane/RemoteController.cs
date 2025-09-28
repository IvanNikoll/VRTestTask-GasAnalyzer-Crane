using System;
using UnityEngine;

public class RemoteController : MonoBehaviour
{
    public event Action OnMoveForward;
    public event Action OnMoveBackward;
    public event Action OnMoveLeft;
    public event Action OnMoveRight;
    public event Action OnMoveUp;
    public event Action OnMoveDown;
    public bool IsPressed;

    public void MoveForward()
    {
        IsPressed = true;
        OnMoveForward?.Invoke();
    }

    public void MoveBackward()
    {
        IsPressed = true;
        OnMoveBackward?.Invoke();
    }

    public void MoveLeft()
    {
        IsPressed = true;
        OnMoveLeft?.Invoke();
    }

    public void MoveRight()
    {
        IsPressed = true;
        OnMoveRight?.Invoke();
    }

    public void MoveUp()
    {
        IsPressed = true;
        OnMoveUp?.Invoke();
    }

    public void MoveDown()
    {
        IsPressed = true;
        OnMoveDown?.Invoke();
    }

    public void StopPressingButton()
    {
        IsPressed = false;
    }
}

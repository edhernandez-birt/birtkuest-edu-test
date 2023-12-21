using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls2;

[CreateAssetMenu(fileName = "InputReaderArrows", menuName = "InputReaderArrows")]
public class InputReader2 : ScriptableObject, IPlayer2Actions
{
    public event Action<Vector2> MoveEvent;
    public event Action<bool> PrimaryFireEvent;
    private Controls2 controls;

    private void OnEnable()
    {
        if (controls == null)
        {
            controls = new Controls2();
            controls.Player2.SetCallbacks(this);
        }
        controls.Player2.Enable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnPrimaryFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PrimaryFireEvent?.Invoke(true);
        }
        else if (context.canceled)
        {
            PrimaryFireEvent?.Invoke(false);
        }
    }
}

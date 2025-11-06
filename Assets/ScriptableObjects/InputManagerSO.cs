using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "InputManager")]
public class InputManagerSO : ScriptableObject
{
	InputSystem_Actions myInputs;

	public event Action OnJump;
	public event Action<Vector2> OnMove;

	private void OnEnable()
	{
		myInputs = new InputSystem_Actions();
		myInputs.Player.Enable();
		myInputs.Player.Jump.started += Jump;
		myInputs.Player.Move.performed += Move;
		myInputs.Player.Move.canceled += Move;
	}

	private void Move(InputAction.CallbackContext context)
	{
		OnMove?.Invoke(context.ReadValue<Vector2>());
	}

	private void Jump(InputAction.CallbackContext context)
	{
		OnJump?.Invoke();
	}
}

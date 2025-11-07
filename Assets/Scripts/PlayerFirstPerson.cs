using System;
using UnityEngine;

public class PlayerFirstPerson : MonoBehaviour
{
	[SerializeField] private float velocityMovement;
	[SerializeField] private Transform cameraRef;
	[SerializeField] private InputManagerSO inputManager;
	private Vector3 moveDirection;
	private Vector3 inputDirection;
	private CharacterController controller;

	private void OnEnable()
	{
		inputManager.OnJump += Jump;
		inputManager.OnMove += Move;
	}

	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		controller = GetComponent<CharacterController>();
	}

	void Update()
	{
		moveDirection = cameraRef.forward * inputDirection.z + cameraRef.right * inputDirection.x;
		moveDirection.y = 0;

		controller.Move(moveDirection * velocityMovement * Time.deltaTime);

		if (moveDirection.sqrMagnitude > 0)
		{
			RotateToDestination();
		}
	}

	private void RotateToDestination()
	{
		Debug.Log("direction: " + moveDirection);

		Quaternion rotateObjective = Quaternion.LookRotation(moveDirection);
		this.transform.rotation = rotateObjective;
	}

	private void Move(Vector2 vector)
	{
		inputDirection = new Vector3(vector.x, 0, vector.y);
	}

	private void Jump()
	{
		Debug.Log("El player salta");
	}
}

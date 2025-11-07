using System;
using UnityEngine;

public class PlayerFirstPerson : MonoBehaviour
{
	[Header("Player data")]
	[SerializeField] private float velocityMovement;
	[SerializeField] private float gravity;
	[SerializeField] private Transform cameraRef;
	[SerializeField] private InputManagerSO inputManager;
	[SerializeField] private float heightJump;

	[Header("Floor detection")]
	[SerializeField] private Transform feets;
	[SerializeField] private float radioDetection;
	[SerializeField] private LayerMask floorMask;
	
	#region Private variables
	private Vector3 moveDirection;
	private Vector3 inputDirection;
	private Vector3 verticalVelocity;
	private CharacterController controller;
	#endregion

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

		//Si hemos aterrizado, reseteamos la velocidad en vertical
		if(FloorDetection() && verticalVelocity.y < 0)
		{
			verticalVelocity.y = 0;
		}

		ApplyGravity();
	}

	private void ApplyGravity()
	{
		verticalVelocity.y += gravity * Time.deltaTime;
		controller.Move(verticalVelocity * Time.deltaTime);
	}

	private bool FloorDetection()
	{
		return Physics.CheckSphere(feets.position, radioDetection, floorMask);
	}

	private void Move(Vector2 vector)
	{
		inputDirection = new Vector3(vector.x, 0, vector.y);
	}

	private void Jump()
	{
		if (FloorDetection())
		{
			verticalVelocity.y = Mathf.Sqrt(-2 * gravity * heightJump);	
		}
	}

	private void OrawGizmos()
	{
		Gizmos.DrawSphere(feets.position, radioDetection);		
	}
}

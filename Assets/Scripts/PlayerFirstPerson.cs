using System;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerFirstPerson : MonoBehaviour
{
	[Header("Player data")]
	[SerializeField] private float velocityMovement;
	[SerializeField] private float gravity;
	[SerializeField] private Transform cameraRef;
	[SerializeField] private InputManagerSO inputManager;
	[SerializeField] private float heightJump;
	
	[Header("Animation references")]
	[SerializeField] private Animator anim;
	[SerializeField] private ParticleSystem shootParticles;

	[Header("Floor detection")]
	[SerializeField] private Transform feets;
	[SerializeField] private float radioDetection;
	[SerializeField] private LayerMask floorMask;
	
	#region Private variables
	private Vector3 moveDirection;
	private Vector3 inputDirection;
	private Vector3 verticalVelocity;
	private CharacterController controller;
	private float shootDistance = 500;
	#endregion

	private void OnEnable()
	{
		inputManager.OnJump += Jump;
		inputManager.OnMove += Move;
		inputManager.OnShoot += Shoot;
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

		//Si hemos aterrizado, reseteamos la velocidad en vertical
		if (FloorDetection() && verticalVelocity.y < 0)
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

	private void RotateToDestination()
	{
		Quaternion rotateObjective = Quaternion.LookRotation(moveDirection);
		this.transform.rotation = rotateObjective;
	}

	private void Shoot()
	{
		anim.SetTrigger("Shoot");
		shootParticles.Play();

		//Check if raycast with something
		if (Physics.Raycast(cameraRef.position, cameraRef.forward, out RaycastHit hitInfo, shootDistance))
		{

			//Check if that something is damagable
			if(hitInfo.transform.gameObject.TryGetComponent(out Damagable component))
			{
				component.GetDamage(50);
			}
		}
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

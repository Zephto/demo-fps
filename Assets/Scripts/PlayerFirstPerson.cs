using UnityEngine;

public class PlayerFirstPerson : MonoBehaviour
{
	[SerializeField] private float velocityMovement;
	[SerializeField] private Transform cameraRef;
	private float inputH;
	private float inputV;
	private CharacterController controller;

	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		controller = GetComponent<CharacterController>();
	}

	void Update()
	{
		inputH = Input.GetAxisRaw("Horizontal");
		inputV = Input.GetAxisRaw("Vertical");

		Vector3 moveDir = (cameraRef.forward * inputV + cameraRef.right * inputH).normalized;
		moveDir.y = 0;

		controller.Move(moveDir * velocityMovement * Time.deltaTime);

		if (inputH != 0 && inputV != 0) RotateToDestination(moveDir);
	}

	private void RotateToDestination(Vector3 destination)
	{
		Quaternion rotateObjective = Quaternion.LookRotation(destination);
		this.transform.rotation = rotateObjective;
	}
}

using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour
{
	public bool lockCursor;
	public float mouseSensitivity = 10;
	public Transform target;
	public float dstFromTarget = 2;
	public Vector2 pitchMinMax = new Vector2(-40, 85); //Setting a min and max camera rotation clamp

	public float rotationSmoothTime = .12f;
	Vector3 rotationSmoothVelocity;
	Vector3 currentRotation;

	float yaw;
	float pitch;

	void Start()
	{
		if (lockCursor)
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}

	void LateUpdate()
	{
		yaw += Input.GetAxis("Mouse X") * mouseSensitivity; //Move camera yaw based on mouse
		pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity; //Move camera pitch based on mouse
		pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y); //Applying a clamp with the pitch variables

		currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
		transform.eulerAngles = currentRotation;

		transform.position = target.position - transform.forward * dstFromTarget;
	}
}

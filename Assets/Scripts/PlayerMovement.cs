﻿using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	public float walkSpeed = 2;
	public float runSpeed = 6;
	public float gravity = -12;
	public float jumpHeight = 1;
	[Range(0, 1)]
	public float airControlPercent;

	public float turnSmoothTime = 0.2f;
	float turnSmoothVelocity;

	public float speedSmoothTime = 0.1f;
	float speedSmoothVelocity;
	float currentSpeed;
	float velocityY;

	public PickUp pickUp;
    public BallThrow ballThrow;

    public Animator animator;
	public Transform cameraT;
	public CharacterController controller;


	void Start()
	{
		controller.enabled = true;
		animator = GetComponent<Animator>();
		cameraT = Camera.main.transform;
		controller = GetComponent<CharacterController>();
	}

	void Update()
	{
		// input
		Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		Vector2 inputDir = input.normalized;
		bool running = Input.GetKey(KeyCode.LeftShift);

		Move(inputDir, running);

		if (Input.GetKeyDown(KeyCode.Space))
		{
			Jump();
		}
		// animator
		float animationSpeedPercent = ((running) ? currentSpeed / runSpeed : currentSpeed / walkSpeed * .5f);

		if (!pickUp.playThrow)
		{
            animator.SetBool("playMovement", true);
            animator.SetBool("playThrowBall", false);
			animator.SetBool("playPickUp", false);
			animator.SetFloat("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);
		}

		if (pickUp.playPickUp)
		{
			animator.SetBool("playMovement", false);
			animator.SetBool("playThrowBall", false);
			animator.SetBool("playPickUp", true);
		}

		if (pickUp.playThrow)
		{
            animator.SetBool("playMovement", false);
            animator.SetBool("playThrowBall", true);
			animator.SetBool("playPickUp", false);
		}
	}

	void Move(Vector2 inputDir, bool running)
	{
		if (inputDir != Vector2.zero)
		{
			float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
			transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, GetModifiedSmoothTime(turnSmoothTime));
		}

		float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
		currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, GetModifiedSmoothTime(speedSmoothTime));

		velocityY += Time.deltaTime * gravity;
		Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocityY;

		controller.Move(velocity * Time.deltaTime);
		currentSpeed = new Vector2(controller.velocity.x, controller.velocity.z).magnitude;

		if (controller.isGrounded)
		{
			velocityY = 0;
		}
	}

	void Jump()
	{
		if (controller.isGrounded)
		{
			float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
			velocityY = jumpVelocity;
		}
	}

	float GetModifiedSmoothTime(float smoothTime)
	{
		if (controller.isGrounded)
		{
			return smoothTime;
		}

		if (airControlPercent == 0)
		{
			return float.MaxValue;
		}
		return smoothTime / airControlPercent;
	}

	public void AttachToHand()
	{
		pickUp.PickingUp();
	}

	public void Release()
    {
        ballThrow.ReleaseMe();
        ballThrow.Throw();
		pickUp.UnFreeze();
    }

    public void StopAnimation()
    {
        pickUp.playThrow = false;
		pickUp.playPickUp = false;
        Debug.Log("Stopping anim");

		startMovement();
    }

	public void stopMovement()
	{
		controller.enabled = false;
	}

	public void startMovement()
	{
		controller.enabled = true;
	}
}
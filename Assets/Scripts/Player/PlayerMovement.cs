﻿using UnityEngine;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour {
	public float speed = 6f;            // The speed that the player will move at.
	public ParticleSystem particlesSpeed;
	public ParticleSystem particlesSoul;
	public Slider speedSlider;
	public Slider soulSlider;
	public PlayerShooting PlayerShooting;

	Vector3 movement;                   // The vector to store the direction of the player's movement.
	Animator anim;                      // Reference to the animator component.
	Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
	Light playerLight;

	bool canUseSoul = false;
	bool canUseSpeed = true;
	bool alreadyPressed = false;
	
	int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
	float camRayLength = 100f;          // The length of the ray from the camera into the scene.


	void Awake () {
		// Create a layer mask for the floor layer.
		floorMask = LayerMask.GetMask ("Floor");
		
		// Set up references.
		anim = GetComponent <Animator> ();
		playerRigidbody = GetComponent <Rigidbody> ();

	}
	
	
	void FixedUpdate () {
		// Store the input axes.			
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		// Check 
		Dash();

		Souls();

		// Move the player around the scene.
		Move (h, v);
		
		// Turn the player to face the mouse cursor.
		Turning ();
		
		// Animate the player.
		Animating (h, v);
	}
	
	
	void Move (float h, float v) {
		// Set the movement vector based on the axis input.
		movement.Set (h, 0f, v);
		
		// Normalise the movement vector and make it proportional to the speed per second.
		movement = movement.normalized * speed * Time.deltaTime;
		
		// Move the player to it's current position plus the movement.
		playerRigidbody.MovePosition (transform.position + movement);
	}
	
	
	void Turning () {
		// Create a ray from the mouse cursor on screen in the direction of the camera.
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		
		// Create a RaycastHit variable to store information about what was hit by the ray.
		RaycastHit floorHit;
		
		// Perform the raycast and if it hits something on the floor layer...
		if(Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
		{
			// Create a vector from the player to the point on the floor the raycast from the mouse hit.
			Vector3 playerToMouse = floorHit.point - transform.position;
			
			// Ensure the vector is entirely along the floor plane.
			playerToMouse.y = 0f;
			
			// Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
			Quaternion newRotatation = Quaternion.LookRotation (playerToMouse);
			
			// Set the player's rotation to this new rotation.
			playerRigidbody.MoveRotation (newRotatation);
		}
	}
	
	
	void Animating (float h, float v) {
		// Create a boolean that is true if either of the input axes is non-zero.
		bool walking = h != 0f || v != 0f;
		
		// Tell the animator whether or not the player is walking.
		anim.SetBool ("IsWalking", walking);
	}

	private void Dash() {

		if(speedSlider.value == 100) canUseSpeed = true;


		if(Input.GetKey(KeyCode.Space) && speedSlider.value > 0 && canUseSpeed) {
			if(particlesSpeed != null) particlesSpeed.Play();
			speed = 12;
			speedSlider.value -= 1;

		}else {
			if(particlesSpeed != null) particlesSpeed.Stop();
			speed = 6;
			canUseSpeed = false;
			speedSlider.value += 1;

		}
	}

	private void Souls() {

		if(soulSlider.value == 200) canUseSoul = true;

		if((Input.GetKeyDown(KeyCode.G) && canUseSoul) || alreadyPressed) {
			soulSlider.value -= 1;
			playerRigidbody.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
			PlayerShooting.damagePerShot = 40;
			if(particlesSoul != null) particlesSoul.Play();
			alreadyPressed = true;
			if(soulSlider.value == 0) {
				if(particlesSoul != null) particlesSoul.Stop();
				canUseSoul = false;
				alreadyPressed = false;
				playerRigidbody.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
				PlayerShooting.damagePerShot = 20;


			} 

		}
	}
}
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Ship : MonoBehaviour
{
	//To avoid repetitively retrieving the Rigidbody 2D component every time thrust is applied,
	//declare a private field ‘rb2D’ within the Ship class to store this component.
	private Rigidbody2D rb2d;
	//defining movement direction
	private Vector2 thrustDirection;


	//constant value
	private float thrustForce = 2f;
	//store the radius of the ship's collider
	private float colliderRadius;
	//rotate speed
	private const float rotateDegreesPerSecond = 50f;

	// Start is called before the first frame update
	void Start()
	{
		//assign this field the Rigidbody 2D component attached to the Ship game object.
		rb2d = GetComponent<Rigidbody2D>();
		//retrieve and store the radius
		colliderRadius = GetComponent<CircleCollider2D>().radius;
	}

	//// Update is called once per frame
	void Update()
	{
		float rotationInput = Input.GetAxis("Rotate");
		// calculate rotation amount and apply rotation
		float rotationAmount = rotateDegreesPerSecond * Time.deltaTime;
		if (rotationInput < 0)
		{
			rotationAmount *= -1;
		}
		transform.Rotate(Vector3.forward, rotationAmount);
		//assign thrustDirection as same as rotate direction
		thrustDirection = CalculateDynamicThrustDirection();
	}
	//used to interact with physics
	private void FixedUpdate()
	{
		float thrustInput = Input.GetAxis("Thrust");
		if (thrustInput > 0f)
		{
			rb2d.AddForce(thrustDirection * thrustForce, ForceMode2D.Force);
		}
	}

	// Disables the behaviour when the ship is invisible
	void OnBecameInvisible()
	{
		//enabled = false;
		KeepInScreen();

	}
	void KeepInScreen()
	{
		//if ship exits ScreenRight, ship comes out from ScreenLeft
		Vector2 position = transform.position;
		if (position.x + colliderRadius > ScreenUtils.ScreenRight)
		{
			position.x = ScreenUtils.ScreenLeft + colliderRadius;
			transform.position = position;
			Debug.Log("Entering screen left: " + transform.position);

		}
		//if ships exits ScreenLeft, ship comes out from ScreenRight
		if (position.x - colliderRadius < ScreenUtils.ScreenLeft)
		{
			position.x = ScreenUtils.ScreenRight + colliderRadius;
			transform.position = position;
			Debug.Log("Entering screen right: " + transform.position);

		}
		// if ship exits ScreenTop, ship comes out from ScreenBottom;
		if (position.y + colliderRadius > ScreenUtils.ScreenTop)
		{
			position.y = ScreenUtils.ScreenBottom + colliderRadius;
			transform.position = position;
			Debug.Log("Entering screen bottom: " + transform.position);

		}
		//if ships exits ScreenBottom, ship comes out from ScreenTop
		if (position.y - colliderRadius < ScreenUtils.ScreenBottom)
		{
			position.y = ScreenUtils.ScreenTop + colliderRadius;
			transform.position = position;
			Debug.Log("Entering screen top: " + transform.position);

		}
	}
	Vector2 CalculateDynamicThrustDirection()
	{
		// Extract the ship's rotation around the Z axis from eulerAngles.z.
		float shipRotationZ = transform.eulerAngles.z;

		// Convert the angle from degrees to radians
		float shipRotationRadians = shipRotationZ * Mathf.Deg2Rad;

		// Determine the new X and Y components of the thrustDirection vector
		float thrustDirectionX = Mathf.Cos(shipRotationRadians);
		float thrustDirectionY = Mathf.Sin(shipRotationRadians);

		// Return the calculated thrust direction as a Vector2
		return new Vector2(thrustDirectionX, thrustDirectionY);
	}
}

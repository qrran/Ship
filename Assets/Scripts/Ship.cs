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
	//private Vector2 thrustDirection = new Vector2(1, 0);
	//down
	//private Vector2 thrustDirection = new Vector2(0, -1);
	//up
	//private Vector2 thrustDirection = new Vector2(0, 1);
	//left
	private Vector2 thrustDirection = new Vector2(-1, 0);


	//constant value
	private float ThrustForce = 2f;
	//store the radius of the ship's collider
	private float colliderRadius;

	float colliderHalfWidth, colliderHalfHeight;

	// Start is called before the first frame update
	void Start()
	{
		//assign this field the Rigidbody 2D component attached to the Ship game object.
		rb2d = GetComponent<Rigidbody2D>();
		//retrieve and store the radius
		colliderRadius = GetComponent<CircleCollider2D>().radius;
	}

	//// Update is called once per frame
	//void Update()
	//   {

	//   }
	//used to interact with physics
	private void FixedUpdate()
	{
		float thrustInput = Input.GetAxis("Thrust");
		Debug.Log("Thrust: " + thrustInput);
		if (thrustInput > 0f)
		{
			Debug.Log("space pressed");
			rb2d.AddForce(thrustDirection * ThrustForce, ForceMode2D.Force);

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
			position.x = ScreenUtils.ScreenLeft - colliderRadius;
			transform.position = position;
			Debug.Log("Exits screen right: " + transform.position);

		}
		//if ships exits ScreenLeft, ship comes out from ScreenRight
		if (position.x + colliderRadius < ScreenUtils.ScreenLeft)
		{
			position.x = ScreenUtils.ScreenRight + colliderRadius;
			transform.position = position;
			Debug.Log("Exits screen left: " + transform.position);

		}
		// if ship exits ScreenTop, ship comes out from ScreenBottom;
		if (position.y + colliderRadius > ScreenUtils.ScreenTop)
		{
			position.y = ScreenUtils.ScreenBottom + colliderRadius;
			transform.position = position;
			Debug.Log("Exits screen top: " + transform.position);

		}
		//if ships exits ScreenBottom, ship comes out from ScreenTop
		if (position.y - colliderRadius < ScreenUtils.ScreenBottom)
		{
			position.y = ScreenUtils.ScreenTop + colliderRadius;
			transform.position = position;
			Debug.Log("Exits screen bottom: " + transform.position);

		}
	}
}

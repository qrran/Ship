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
    private Vector2 thrustDirection = new Vector2(1, 0);
    //constant value
    private float ThrustForce = 2f;
	// Start is called before the first frame update
	void Start()
    {
        //assign this field the Rigidbody 2D component attached to the Ship game object.
        rb2d = GetComponent<Rigidbody2D>();

	}

	//// Update is called once per frame
	//void Update()
 //   {
        
 //   }
    //used to interact with physics
	private void FixedUpdate()
	{
		float thrustInput = Input.GetAxis("Thrust");
		Debug.Log(thrustInput);
		if (thrustInput > 0f)
		{
			Debug.Log("space pressed");
			rb2d.AddForce(thrustDirection * ThrustForce, ForceMode2D.Force);

		}

	}
}

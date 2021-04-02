using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    Rigidbody myRigidbody;
    Vector3 oldVel;
    [SerializeField]
    private Transform spawnPos;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        oldVel = myRigidbody.velocity;
        //set the y component of the velocity to zero
        myRigidbody.velocity = new Vector3(myRigidbody.velocity.x,
                                 0,
                                 myRigidbody.velocity.z);
        _CheckBall();
    }

    void OnCollisionEnter(Collision c)
    {
        //colliding with Dynamic ball
        if (c.gameObject.tag == "BumperA")
        {
            ContactPoint cp = c.contacts[0];
            // calculate with addition of normal vector
            // myRigidbody.velocity = oldVel + cp.normal*2.0f*oldVel.magnitude;

            // calculate with Vector3.Reflect
            myRigidbody.velocity = Vector3.Reflect(oldVel, cp.normal);

            // bumper effect to speed up ball
            myRigidbody.velocity += cp.normal * 5.0f;
            myRigidbody.velocity = new Vector3(myRigidbody.velocity.x,
                         0,
                         myRigidbody.velocity.z);
        }
        //colliding with Bumpers
        if (c.gameObject.tag == "BumperS")
        {
            ContactPoint cp = c.contacts[0];
            // calculate with addition of normal vector
            // myRigidbody.velocity = oldVel + cp.normal*2.0f*oldVel.magnitude;

            // calculate with Vector3.Reflect
            myRigidbody.velocity = Vector3.Reflect(oldVel, cp.normal);

        }// if ball collide with toy bash increment score
        if (c.gameObject.tag == "toy")
        {
            ContactPoint cp = c.contacts[0];

            myRigidbody.velocity = Vector3.Reflect(oldVel, cp.normal);
           ScoreScript.scoreValue += 10;
            myRigidbody.velocity = new Vector3(myRigidbody.velocity.x,
                         0,
                         myRigidbody.velocity.z);

        }

        //colliding with blue walls
        if (c.gameObject.tag == "blue")
        {
            ContactPoint cp = c.contacts[0];

            myRigidbody.velocity = Vector3.Reflect(oldVel, cp.normal);
            ScoreScript.scoreValue += 5;
            myRigidbody.velocity += cp.normal * 2.0f;

            myRigidbody.velocity = new Vector3(myRigidbody.velocity.x,
                         0,
                         myRigidbody.velocity.z);

        }
        //colliding with the pink walls
        if (c.gameObject.tag == "wall")
        {
            ContactPoint cp = c.contacts[0];

            myRigidbody.velocity = Vector3.Reflect(oldVel, cp.normal);
            ScoreScript.scoreValue += 5;
            myRigidbody.velocity += cp.normal * 1.5f;

            myRigidbody.velocity = new Vector3(myRigidbody.velocity.x,
                         0,
                         myRigidbody.velocity.z);

        }
    }

    private void _CheckBall()
    {
		
        //if the ball has been kicked respawn the ball
        if (transform.position.y < -0.09)
        {
            transform.position = spawnPos.position;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            ScoreScript.scoreValue = 0;
            Debug.Log("check");
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperScript : MonoBehaviour
{
    public float restPosition = 0f;
    public float pressedPosition = 45f;
    public float hitStrength = 10000f;
    public float flipperDamper = 150f;
    public string flipperName;
    HingeJoint hinge;
    private JointSpring spring;

    // Start is called before the first frame update
    void Start()
    {
        hinge = GetComponent<HingeJoint>();
        hinge.useSpring = true;
        spring = new JointSpring();
        spring.spring = hitStrength;
        spring.damper = flipperDamper;

        hinge.spring = spring;
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetAxis(flipperName) == 1)
        {
            Debug.Log("here");
            spring.targetPosition = pressedPosition;

        }
        else
        {
            spring.targetPosition = restPosition;

        }
        hinge.spring = spring;
        //hinge.useLimits = true;
    }
}
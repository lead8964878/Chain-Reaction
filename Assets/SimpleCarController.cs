using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleCarController : MonoBehaviour
{
    public float steerAngle;
    public float enginePower;
    public Vector3 centerOfMass;
    public Rigidbody rb;
    public WheelCollider[] wheels;
    public Transform[] visualWheels;

    void Start()
    {
        rb.centerOfMass = centerOfMass;
    }

    void Update()
    {
        for(int i = 0; i < wheels.Length; i++)
        {
            Vector3 pos;
            Quaternion rot;
            wheels[i].GetWorldPose(out pos, out rot);
            visualWheels[i].position = pos;
            visualWheels[i].rotation = rot;
        }

        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        wheels[0].steerAngle = h * steerAngle;
        wheels[1].steerAngle = h * steerAngle;

        wheels[2].motorTorque = v * enginePower;
        wheels[3].motorTorque = v * enginePower;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [HideInInspector] public float horizontalInput;
    [HideInInspector] public float verticalInput;
    [HideInInspector] public bool isBraking;

    private float currentBrakeForce;
    private float currentSteeringAngle;
    private float currentSpeed;
    private float aang;

    [Header("Wheel Colliders")]
    [SerializeField] private WheelCollider wheelFR;
    [SerializeField] private WheelCollider wheelFL;
    [SerializeField] private WheelCollider wheelBR;
    [SerializeField] private WheelCollider wheelBL;

    [Header("Tyre Meshes")]
    [SerializeField] private Transform tyreFL;
    [SerializeField] private Transform tyreFR;
    [SerializeField] private Transform tyreBL;
    [SerializeField] private Transform tyreBR;

    [Header("Motor Parameters")]
    [SerializeField] private float motorForce;
    [SerializeField] private float brakeForce;

    [Header("Steering Parameters")]
    [SerializeField] private float steeringAngle;
    [SerializeField] private float maxSteeringAngle;


    private void HandleMotor()
    {
        wheelFL.motorTorque = verticalInput * motorForce;
        wheelFR.motorTorque = verticalInput * motorForce;
        //wheelBL.motorTorque = verticalInput * motorForce;
        //wheelBR.motorTorque = verticalInput * motorForce;
        currentBrakeForce = isBraking ? brakeForce : 0f;
    }

    private void ApplyBreaking()
    {
        wheelBL.brakeTorque = currentBrakeForce;
        wheelBR.brakeTorque = currentBrakeForce;
        wheelFL.brakeTorque = currentBrakeForce;
        wheelFR.brakeTorque = currentBrakeForce;
    }

    private void HandleSteering()
    {
        currentSteeringAngle = horizontalInput * maxSteeringAngle;

        wheelFR.steerAngle = currentSteeringAngle;
        wheelFL.steerAngle = currentSteeringAngle;

        Vector3 pos;
        Quaternion rot;
        wheelFL.GetWorldPose(out pos, out rot);

        Vector3 posB;
        Quaternion rotB;
        wheelBL.GetWorldPose(out posB, out rotB);

        if ((tyreFL != null) && (tyreFR != null) && (tyreBL != null) && (tyreBR != null))
        {
            tyreFL.rotation = rot;
            tyreFR.rotation = rot;
            tyreBL.rotation = rotB;
            tyreBR.rotation = rotB;
        }
    }

    private void FixedUpdate()
    {
        HandleMotor();
        HandleSteering();
        ApplyBreaking();

        //Debug.Log(this.GetComponent<Rigidbody>().velocity.magnitude);

        currentSpeed = this.GetComponent<Rigidbody>().velocity.magnitude;
        aang = Vector3.Angle(transform.up, Vector3.up);

        if (gameObject.tag == "Player")
        {
            
        }
    }

}

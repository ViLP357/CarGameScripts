using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public static CarController instanssi;
    private float horizontalInput, verticalInput;
    private float currentSteerAngle, currentbreakForce;
    private bool isBreaking;
    public TrailRenderer[] Tyremarks;

    // Settings
    [SerializeField] private float motorForce, breakForce, maxSteerAngle;
    [SerializeField] private float speedThreshold = 30f; // Nopeusraja tyylijälkien aktivoimiseksi
    [SerializeField] private float tightTurnAngleThreshold = 15f; // Kulmaraja tyylijälkien aktivoimiseksi

    // Wheel Colliders
    [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

    // Wheels
    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;

    private Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        instanssi = this;
        //PlayerPrefs.DeleteKey("BestTimeLevel");
        //PlayerPrefs.DeleteKey("BestTimeLevel1");
        //PlayerPrefs.DeleteKey("BestTimeLevel2");

        //PlayerPrefs.DeleteKey("currentLevel");

    }
    private void FixedUpdate() {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        ActiveTyremarks();
    }

    private void GetInput() {
        // Steering Input
        horizontalInput = Input.GetAxis("Horizontal");

        // Acceleration Input
        verticalInput = Input.GetAxis("Vertical");

        // Breaking Input
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor() {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        currentbreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();
    }

    private void ApplyBreaking() {
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    private void HandleSteering() {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels() {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform) {
        Vector3 pos;
        Quaternion rot; 
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }

    private void ActiveTyremarks() {
        // Tarkistetaan nopeus ja ohjauskulma
        float speed = rb.velocity.magnitude * 3.6f; // Nopeus km/h
        bool isTightTurn = Mathf.Abs(currentSteerAngle) > tightTurnAngleThreshold;

        // Tarkistetaan, täyttyykö vähintään yksi ehto tyylijälkien aktivoimiseksi
        if (speed > speedThreshold || isTightTurn) {
            foreach (TrailRenderer T in Tyremarks) {
                T.emitting = true;
            }
        } else {
            foreach (TrailRenderer T in Tyremarks) {
                T.emitting = false;
            }
        }
    }
    public float PalautaNopeus() {
        return rb.velocity.magnitude * 3.6f;
    }

}
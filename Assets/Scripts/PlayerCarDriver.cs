using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarController))]
public class PlayerCarDriver : MonoBehaviour
{
    private CarController carController;

    private void Start()
    {
        carController = GetComponent<CarController>();
    }

    private void FixedUpdate()
    {
        carController.horizontalInput = Input.GetAxis("Horizontal");
        carController.verticalInput = Input.GetAxis("Vertical");
        carController.isBraking = Input.GetButton("Brake");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSensor : MonoBehaviour
{
    [HideInInspector] public bool hasDetectedThePlayer;
    [HideInInspector] public float distanceToObstacle;
    [HideInInspector] public Transform playerTransform;

    [SerializeField] private float rayDistance;


    private void FixedUpdate()
    {
        RaycastHit theObstacle;
        if (Physics.Raycast(transform.position, transform.forward, out theObstacle, rayDistance))
        {
            if (theObstacle.transform.gameObject.tag == "Player" ||
                theObstacle.transform.gameObject.tag == "AIs")
            {
                hasDetectedThePlayer = true;
                playerTransform = theObstacle.transform;

                //Debug.DrawLine(transform.position, playerTransform.position, Color.yellow);
                //Debug.Log("Player is close");
            }

            else
            {
                hasDetectedThePlayer = false;
                playerTransform = null;

                //Debug.Log("Player is far");
            }
        }

        else
        {
            hasDetectedThePlayer = false;
            playerTransform = null;

            //Debug.Log("Player is far");
        }

    }
}

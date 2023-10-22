using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    [SerializeField] private float amount;

    [Header("Rotation Params")]
    [SerializeField] private bool shouldRotate;
    [SerializeField] private float rotationSpeed;


    private void Update()
    {
        if (shouldRotate)
        {
            transform.Rotate(Vector3.up, rotationSpeed);
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(other.attachedRigidbody != null && other.attachedRigidbody.gameObject.tag == "Player")
        {
            other.attachedRigidbody.gameObject.GetComponent<HealthSystem>().CollectPickup(tag, amount);

            this.gameObject.SetActive(false);
        }
    }
}

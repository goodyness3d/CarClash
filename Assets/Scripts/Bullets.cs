using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]

public class Bullets : MonoBehaviour
{
    private Rigidbody rigidBody;
    private Collider colliderComponent;

    [SerializeField] private GameObject bulletMesh;
    [SerializeField] ParticleSystem explosionParticles;
    [SerializeField] AudioSource explosionFX;

    [SerializeField] AudioClip[] explosionAudioClips;

    [SerializeField] private float speed;
    [SerializeField] private float lifetime;
    
    public float damage;

    // Start is called before the first frame update
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        colliderComponent = GetComponent<Collider>();

        rigidBody.velocity = transform.TransformDirection(Vector3.forward * speed);
        StartCoroutine(DisableBullet());

        if (explosionFX != null)
        {
            explosionFX.PlayOneShot(explosionAudioClips[0]);
        }
    }

    IEnumerator DisableBullet()
    {
        yield return new WaitForSeconds(lifetime);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Bullet")
        {
            StopCoroutine(DisableBullet());

            rigidBody.velocity = Vector3.zero;
            colliderComponent.enabled = false;
            bulletMesh.SetActive(false);

            explosionParticles.Play();

            if (explosionFX != null)
            {
                explosionFX.PlayOneShot(explosionAudioClips[1]);
            }

            lifetime = explosionParticles.main.duration;
            StartCoroutine(DisableBullet());

            if (other.attachedRigidbody != null && (other.attachedRigidbody.gameObject.tag == "Player"
                || other.attachedRigidbody.gameObject.tag == "AIs"))
            {
                other.attachedRigidbody.gameObject.GetComponent<HealthSystem>().TakeDamage(damage);
            }
        }
        
    }
}

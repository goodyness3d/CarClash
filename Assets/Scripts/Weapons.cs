using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapons : MonoBehaviour
{
    [Header("Bullet things")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] ParticleSystem muzzleFlash;

    [Header("Firing")]
    [SerializeField] bool useRapidFire;
    [SerializeField] float waitTime;

    private WaitForSeconds wait;
    private bool canFire;

    [HideInInspector] public bool fireDown;

    private WeaponHandler weaponHandler;


    private void Start()
    {
        weaponHandler = transform.parent.parent.gameObject.GetComponent<WeaponHandler>();

        wait = new WaitForSeconds(waitTime);
        canFire = true;
    }


    // Update is called once per frame
    void Update()
    {
        //Check if the car with the weapon is an AI car
        if (weaponHandler != null && weaponHandler.isAI)
        {
            if (weaponHandler.OpponentIsVisible && canFire)
            {
                SprayTheBullets();
                canFire = false;
            }
        }

        //if the car is a player car
        else
        {
            if (useRapidFire)
            {
                //Check if the Fire button is held down
                if (weaponHandler.fireWeaponRapid && canFire)
                {
                    SprayTheBullets();
                    canFire = false;
                }
            }
            else
            {
                //Check if the Fire buttton was just pressed
                if (weaponHandler.fireWeapon && canFire)
                {
                    SprayTheBullets();
                    canFire = false;
                }
            }
        }
        
    }

    private void SprayTheBullets()
    {
        //Check if bullet and bullet spawn point have been assigned game objects
        if (bullet != null && bulletSpawnPoint != null)
        {
            GameObject clone = Instantiate(bullet, bulletSpawnPoint.position, transform.rotation);
            clone.transform.forward = bulletSpawnPoint.transform.forward;

            StartCoroutine(Downtime());
        }
    }

    IEnumerator Downtime()
    {
        yield return wait;
        canFire = true;
    }
}

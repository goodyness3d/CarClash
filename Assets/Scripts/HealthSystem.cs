using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private MasterScript gameMaster;
    [SerializeField] private CarStats carStats;
    [SerializeField] private GameObject deathExplosion;
    [SerializeField] private GameObject gameOverCanvas;

    [SerializeField] private TextMesh healthMeter;

    [SerializeField] private float startingHealth;

    [SerializeField] private Gradient colorGrad;

    private float health;
    private float percentageHealth;


    private void Start()
    {
        if (carStats != null)
        {
            startingHealth = carStats.maxHealth;
            health = carStats.maxHealth;
        }

        else
        {
            health = startingHealth;
        }

        UpdateHealthMeterVisuals();
    }

    private void LateUpdate()
    {
        healthMeter.gameObject.transform.forward = Camera.main.transform.forward;
    }


    public void TakeDamage(float bulletDamage)
    {
        //Debug.Log("Fuckin");

        health -= bulletDamage;
        //Debug.Log(health);

        UpdateHealthMeterVisuals();

        if (health <= 0)
        {
            Die();
        }
    }

    public void UpdateHealthMeterVisuals()
    {
        percentageHealth = (health / startingHealth) * 100f;
        percentageHealth = Mathf.Round(percentageHealth);
        
        healthMeter.text = percentageHealth.ToString() + " %";
        healthMeter.color = colorGrad.Evaluate(percentageHealth / 100);
    }

    private void Die()
    {
        health = 0;

        if(this.gameObject.tag == "AIs")
        {
            gameMaster.carsRemainingInWave -= 1;
        }

        if(this.gameObject.tag == "Player")
        {
            Instantiate(gameOverCanvas);
        }

        Instantiate(deathExplosion, transform.position, transform.rotation);

        this.gameObject.SetActive(false);
    }

    
    public void CollectPickup(string pickupTag, float amount)
    {
        if (pickupTag == "P.Health")
        {
            health += amount;
            health = Mathf.Clamp(health, 0f, startingHealth);

            UpdateHealthMeterVisuals();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private MasterScript gameMasterObject;
    [SerializeField] private GameObject[] weapons;
    [SerializeField] private CarSensor sensor;

    [HideInInspector] public bool OpponentIsVisible;
    [HideInInspector] public bool fireWeaponRapid;
    [HideInInspector] public bool fireWeapon;
    
    public bool isAI;

    private GameObject selectedWeapon;


    private void Start()
    {
        if (isAI == false)
        {
            selectedWeapon = weapons[gameMasterObject.selectedWeaponIndex].gameObject;
            selectedWeapon.SetActive(true);
        }
    }

    private void Update()
    {
        if (sensor != null)
        {
            OpponentIsVisible = sensor.hasDetectedThePlayer;
            //Debug.Log(fireWeapon);
        }
        
    }

    public void SwitchWeapon(int weaponToSwitchTo)
    {
        selectedWeapon.SetActive(false);

        selectedWeapon = weapons[weaponToSwitchTo];
        selectedWeapon.SetActive(true);
    }
}

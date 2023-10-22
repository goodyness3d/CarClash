using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Garage : MonoBehaviour
{
    [Header("Garage or Armory")]
    [SerializeField] private bool isGarage;

    [Header("Essentials")]
    [SerializeField] private MasterScript gameMaster;
    [SerializeField] private CarHandler carHandler;

    private WeaponHandler weaponHandler;

    [Header("UI Buttons")]
    [SerializeField] private GameObject selectButton;
    [SerializeField] private GameObject buyButton;

    [SerializeField] private TextMeshProUGUI selectButtonText;
    [SerializeField] private TextMeshProUGUI buyButtonText;

    [HideInInspector] public int garageIndex;
    [HideInInspector] public int armoryIndex;

    private int numberOfCarsInGarage;
    private int costOfTheCar;

    private int numberOfWeaponsInArmory;
    private int costOfTheWeapon;


    private void OnEnable()
    {
        garageIndex = gameMaster.selectedCarIndex;
        numberOfCarsInGarage = carHandler.gameObject.transform.childCount;

        armoryIndex = gameMaster.selectedWeaponIndex;
        weaponHandler = carHandler.weaponHandler;
        numberOfWeaponsInArmory = weaponHandler.gameObject.transform.childCount;

        //Debug.Log(numberOfWeaponsInArmory);

        ShowTheCorrectButton();

        //Debug.Log(numberOfCarsInGarage);
    }


    private void OnDisable()
    {
        carHandler.SwitchCar(gameMaster.selectedCarIndex);
        weaponHandler.SwitchWeapon(gameMaster.selectedWeaponIndex);

        gameMaster.SaveGame();
    }


    public void PressedRight()
    {
        if (isGarage)
        {
            garageIndex += 1;
            garageIndex = garageIndex % numberOfCarsInGarage;

            //Debug.Log(garageIndex);

            carHandler.SwitchCar(garageIndex);
        }

        else
        {
            armoryIndex += 1;
            armoryIndex = armoryIndex % numberOfWeaponsInArmory;

            //Debug.Log(armoryIndex);

            weaponHandler.SwitchWeapon(armoryIndex);
        }

        ShowTheCorrectButton();
    }


    public void PressedLeft()
    {
        if (isGarage)
        {
            if (garageIndex == 0)
            {
                garageIndex = numberOfCarsInGarage - 1;
            }

            else
            {
                garageIndex -= 1;
            }

            garageIndex = garageIndex % numberOfCarsInGarage;

            //Debug.Log(garageIndex);

            carHandler.SwitchCar(garageIndex);
        }

        else
        {
            if (armoryIndex == 0)
            {
                armoryIndex = numberOfWeaponsInArmory - 1;
            }

            else
            {
                armoryIndex -= 1;
            }

            armoryIndex = armoryIndex % numberOfWeaponsInArmory;

            //Debug.Log(armoryIndex);

            weaponHandler.SwitchWeapon(armoryIndex);
        }

        ShowTheCorrectButton();
    }


    private void ShowTheCorrectButton()
    {
        if (isGarage)
        {
            if (gameMaster.unlockedCars[garageIndex])
            {
                selectButton.SetActive(true);
                buyButton.SetActive(false);

                SetTheSelectButtonText();
            }

            else
            {
                selectButton.SetActive(false);
                buyButton.SetActive(true);

                costOfTheCar = gameMaster.carPriceTags[garageIndex];

                buyButtonText.text = costOfTheCar.ToString();
            }
        }

        else
        {
            if (gameMaster.unlockedWeapons[armoryIndex])
            {
                selectButton.SetActive(true);
                buyButton.SetActive(false);

                SetTheSelectButtonText();
            }

            else
            {
                selectButton.SetActive(false);
                buyButton.SetActive(true);

                costOfTheWeapon = gameMaster.weaponPriceTags[armoryIndex];

                buyButtonText.text = costOfTheWeapon.ToString();
            }
        }
    }


    public void SelectTheCar()
    {
        gameMaster.selectedCarIndex = garageIndex;

        SetTheSelectButtonText();
    }

    public void SelectTheWeapon()
    {
        gameMaster.selectedWeaponIndex = armoryIndex;

        SetTheSelectButtonText();
    }


    private void SetTheSelectButtonText()
    {
        if (isGarage)
        {
            if (gameMaster.selectedCarIndex == garageIndex)
            {
                selectButtonText.text = "Selected";
            }

            else
            {
                selectButtonText.text = "Select";
            }
        }

        else
        {
            if (gameMaster.selectedWeaponIndex == armoryIndex)
            {
                selectButtonText.text = "Selected";
            }

            else
            {
                selectButtonText.text = "Select";
            }
        }
    }


    public void BuyTheCar()
    {
        if (gameMaster.availableMoney > costOfTheCar)
        {
            gameMaster.availableMoney -= costOfTheCar;
            gameMaster.unlockedCars[garageIndex] = true;
            gameMaster.selectedCarIndex = garageIndex;

            ShowTheCorrectButton();
        }
    }


    public void BuyTheWeapon()
    {
        if (gameMaster.availableMoney > costOfTheWeapon)
        {
            gameMaster.availableMoney -= costOfTheWeapon;
            gameMaster.unlockedWeapons[armoryIndex] = true;
            gameMaster.selectedWeaponIndex = armoryIndex;

            ShowTheCorrectButton();
        }
    }
}

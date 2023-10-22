using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CarHandler : MonoBehaviour
{
    [SerializeField] private MasterScript gameMasterObject;
    [SerializeField] private CinemachineVirtualCamera carFollowCamera;


    [SerializeField] private GameObject[] carsList;

    [SerializeField] private GameObject pauseMenu;

    private GameObject currentCar;
    private CarController carEngine;
    private GameObject currentWeapon;
    private PlayerInput playerInput;
    
    [HideInInspector] public WeaponHandler weaponHandler;

    private InputAction moveAction;
    private InputAction brakeAction;
    private InputAction reverseAction;
    private InputAction fireAction;
    private InputAction pauseAction;
    private InputAction resetAction;

    private bool reversing;
    private bool braking;

    private Vector3 initialCarPosition;
    private Quaternion initialCarRotation;

    private void Start()
    {
        initialCarPosition = this.gameObject.transform.position;
        initialCarRotation = this.gameObject.transform.rotation;

        currentCar = carsList[gameMasterObject.selectedCarIndex].gameObject;
        carFollowCamera.Follow = currentCar.transform;
        currentCar.SetActive(true);

        carEngine = currentCar.GetComponent<CarController>();
        weaponHandler = currentCar.GetComponentInChildren<WeaponHandler>();

        playerInput = GetComponent<PlayerInput>();

        moveAction = playerInput.actions["Move"];
        brakeAction = playerInput.actions["Brake"];
        fireAction = playerInput.actions["Fire"];
        reverseAction = playerInput.actions["Reverse"];
        pauseAction = playerInput.actions["Pause"];
        resetAction = playerInput.actions["Reset"];

        brakeAction.performed += _ => Braking(true);
        brakeAction.canceled += _ => Braking(false);

        fireAction.performed += _ => Fire(true);
        fireAction.canceled += _ => Fire(false);

        reverseAction.performed += _ => Reverse(true);
        reverseAction.canceled += _ => Reverse(false);

        //carsList[globalVarObject.selectedCarIndex].gameObject.SetActive(true);
    }

    private void FixedUpdate()
    {
        if (carEngine != null)
        {
            carEngine.horizontalInput = moveAction.ReadValue<Vector2>().x;
            carEngine.verticalInput = reversing ? -1 : 1;
        }

        weaponHandler.fireWeapon = fireAction.triggered;

        if (pauseMenu != null)
        {
            if (pauseAction.triggered)
            {
                pauseMenu.SetActive(true);
            }
        }

        if (resetAction.triggered)
        {
            if (gameMasterObject.availableTickets > 0)
            {
                ResetCarPosition();

                gameMasterObject.availableTickets -= 1;
                gameMasterObject.SaveGame();
            }
        }
    }

    private void ProcessBraking(bool condition)
    {
        if (condition)
        {
            if (carEngine.verticalInput > 0)
            {
                carEngine.isBraking = true;
                reversing = true;
            }
            else
            {
                carEngine.isBraking = false;
                reversing = true;
            }
        }
        else
        {
            carEngine.isBraking = false;
            reversing = false;
        }
    }

    private void Braking(bool shouldBrake)
    {
        carEngine.isBraking = shouldBrake;
        //braking = shouldBrake;
    }

    private void Fire(bool shouldFire)
    {
        if (weaponHandler != null)
        {
            weaponHandler.fireWeaponRapid = shouldFire;
        }
    }

    private void Reverse(bool shouldReverse)
    {
        reversing = shouldReverse;
    }


    private void ResetCarPosition()
    {
        carEngine.transform.position = initialCarPosition;
        carEngine.transform.rotation = initialCarRotation;
    }

    public void SwitchCar(int carToSwitchTo)
    {
        currentCar.SetActive(false);

        currentCar = carsList[carToSwitchTo];
        currentCar.SetActive(true);

        weaponHandler = currentCar.GetComponentInChildren<WeaponHandler>();

    }
}

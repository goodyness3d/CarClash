using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnScreenController : MonoBehaviour
{
    [SerializeField] private MasterScript gameMaster;

    public GameObject[] onScreenGamepad;

    private GameObject currentOnScreenGamepad;


    void Start()
    {
        currentOnScreenGamepad = onScreenGamepad[gameMaster.selectedTouchControllerIndex];
        currentOnScreenGamepad.SetActive(true);
    }


    public void SwitchGamepad(int gamepadIndex)
    {
        currentOnScreenGamepad.SetActive(false);
        gameMaster.selectedTouchControllerIndex = gamepadIndex;
    }


    IEnumerator EnableTheGamepad()
    {
        yield return new WaitForSeconds(0.3f);
        currentOnScreenGamepad = onScreenGamepad[gameMaster.selectedTouchControllerIndex];
        currentOnScreenGamepad.SetActive(true);
    }
}

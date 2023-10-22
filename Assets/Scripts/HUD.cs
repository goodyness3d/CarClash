using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI wavesInfoText;
    [SerializeField] private TextMeshProUGUI carsInfoText;
    [SerializeField] private TextMeshProUGUI respawnInfoText;
    [SerializeField] private TextMeshProUGUI centralText;

    [SerializeField] private MasterScript gameMaster;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        wavesInfoText.text = "Wave " + gameMaster.currentWave.ToString() + " / " 
            + gameMaster.numberOfWaves.ToString();
        
        carsInfoText.text = "cars left : " + gameMaster.carsRemainingInWave.ToString();

        respawnInfoText.text = "Resets left : " + gameMaster.availableTickets.ToString();

        centralText.text = gameMaster.centralHUDText;
    }
}

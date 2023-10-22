using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GemTicketMonitor : MonoBehaviour
{
    [SerializeField] private MasterScript gameMaster;
    [SerializeField] private bool isGem;
    [SerializeField] private TextMeshProUGUI theGemOrTicketButton;



    // Update is called once per frame
    void Update()
    {
        theGemOrTicketButton.text = isGem ? gameMaster.availableMoney.ToString()
            : gameMaster.availableTickets.ToString();
    }
}

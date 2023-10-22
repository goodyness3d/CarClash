using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesHandler : MonoBehaviour
{
    [SerializeField] private MasterScript gameMaster;
    [SerializeField] private GameObject levelCompleteCanvas;

    [SerializeField] private GameObject[] wavesList;
    [SerializeField] private GameObject[] pickupsList;

    [SerializeField] private Transform pickupSpawnPoint;

    private GameObject currentWave;
    private int waveProgress;

    private bool waveHasBeenCleared;
    private bool isTheFinalWave;

    private void Shuffle(GameObject[] array)
    {
        for (int t = 0; t < array.Length; t++)
        {
            GameObject obj = array[t];
            int r = Random.Range(t, array.Length);
            array[t] = array[r];
            array[r] = obj;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentWave = wavesList[waveProgress];

        currentWave.SetActive(true);

        //Update Master Script Parameters
        gameMaster.carsRemainingInWave = currentWave.transform.childCount;
        gameMaster.numberOfWaves = wavesList.Length;
        gameMaster.currentWave = waveProgress + 1;
        gameMaster.centralHUDText = "";
        gameMaster.availableTickets = 3;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(gameMaster.carsRemainingInWave);

        if (isTheFinalWave)
        {
            if (gameMaster.carsRemainingInWave == 0 && !waveHasBeenCleared)
            {
                waveHasBeenCleared = true;

                //Debug.Log("Level has been cleared");
                //gameMaster.centralHUDText = "Level has been cleared";
                //StartCoroutine(ResetCentralHUDText());

                Instantiate(levelCompleteCanvas);

                gameMaster.availableMoney += 200;
                gameMaster.availableTickets += 2;
                gameMaster.SaveGame();
            }
        }

        else
        {
            if (gameMaster.carsRemainingInWave == 4 && !waveHasBeenCleared)
            {
                waveHasBeenCleared = true;

                gameMaster.availableMoney += 100;
                gameMaster.availableTickets += 1;
                gameMaster.SaveGame();

                //currentWave.SetActive(false);

                //Debug.Log("Wave has been cleared");
                //gameMaster.centralHUDText = "New wave loading";
                //StartCoroutine(ResetCentralHUDText());

                if (waveProgress == wavesList.Length - 2)
                {
                    isTheFinalWave = true;
                }

                waveProgress += 1;

                InstantiatePickups();
                StartCoroutine(StartTheNextWave());
            }
        }

        
    }


    private void InstantiatePickups()
    {
        if (pickupsList.Length > 0)
        {
            Shuffle(pickupsList);
            Instantiate(pickupsList[0], pickupSpawnPoint.position, transform.rotation);
        }
    }

    IEnumerator StartTheNextWave()
    {
        yield return new WaitForSeconds(5f);

        currentWave = wavesList[waveProgress];
        currentWave.SetActive(true);

        //Debug.Log("New wave");

        //Update Master Script Parameters
        gameMaster.carsRemainingInWave += currentWave.transform.childCount;
        gameMaster.currentWave = waveProgress + 1;
        
        waveHasBeenCleared = false;

        if (isTheFinalWave)
        {
            gameMaster.centralHUDText = "Final Wave";
        }
        else
        {
            gameMaster.centralHUDText = "New Wave";
        }

        StartCoroutine(ResetCentralHUDText());
    }


    IEnumerator ResetCentralHUDText()
    {
        yield return new WaitForSeconds(1f);
        gameMaster.centralHUDText = "";
    }
}

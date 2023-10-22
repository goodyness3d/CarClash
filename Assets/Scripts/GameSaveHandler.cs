using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameSaveHandler : MonoBehaviour
{
    [SerializeField] private MasterScript gameMaster;

    [Serializable]
    class SaveData
    {
        public int availableMoney;
        public int availableTickets;

        public bool[] unlockedCars;
        public bool[] unlockedWeapons;

        public float currentMusicVolume;
        public float currentSfxVolume;

        public int currentQualitySettingsIndex;

        public int selectedCarIndex;
        public int selectedWeaponIndex;
        public int selectedTouchControllerIndex;
    }


    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream saveFile = File.Create(Application.persistentDataPath + "/MySaveData.dat");

        SaveData data = new SaveData();

        data.availableMoney = gameMaster.availableMoney;
        data.availableMoney = gameMaster.availableTickets;
        data.currentMusicVolume = gameMaster.currentMusicVolume;
        data.currentQualitySettingsIndex = gameMaster.currentQualitySettingsIndex;
        data.currentSfxVolume = gameMaster.currentSfxVolume;
        data.selectedCarIndex = gameMaster.selectedCarIndex;
        data.selectedTouchControllerIndex = gameMaster.selectedTouchControllerIndex;
        data.selectedWeaponIndex = gameMaster.selectedWeaponIndex;
        data.unlockedCars = gameMaster.unlockedCars;
        data.unlockedWeapons = gameMaster.unlockedWeapons;

        bf.Serialize(saveFile, data);
        saveFile.Close();

        Debug.Log("Game data saved");
        Debug.Log(data.availableMoney);
    }


    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream saveFile = File.Open(Application.persistentDataPath + "/MySaveData.dat", FileMode.Open);

            SaveData data = (SaveData)bf.Deserialize(saveFile);
            saveFile.Close();

            gameMaster.availableMoney = data.availableMoney;
            gameMaster.availableTickets = data.availableMoney;
            gameMaster.currentMusicVolume = data.currentMusicVolume;
            gameMaster.currentQualitySettingsIndex = data.currentQualitySettingsIndex;
            gameMaster.currentSfxVolume = data.currentSfxVolume;
            gameMaster.selectedCarIndex = data.selectedCarIndex;
            gameMaster.selectedTouchControllerIndex = data.selectedTouchControllerIndex;
            gameMaster.selectedWeaponIndex = data.selectedWeaponIndex;
            gameMaster.unlockedCars = data.unlockedCars;
            gameMaster.unlockedWeapons = data.unlockedWeapons;

            Debug.Log("Game data loaded");
        }

        else
        {
            Debug.Log("Save data not found");
        }
    }
}

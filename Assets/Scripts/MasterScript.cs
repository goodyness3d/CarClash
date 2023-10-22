using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Master")]
public class MasterScript : ScriptableObject
{
    public int selectedCarIndex;
    public int selectedWeaponIndex;
    public int selectedVenueIndex;
    public int selectedTouchControllerIndex;

    [HideInInspector] public int currentWave;
    [HideInInspector] public int numberOfWaves;
    [HideInInspector] public int carsRemainingInWave;
    [HideInInspector] public string centralHUDText;

    [HideInInspector] public bool playerHasDied;
    [HideInInspector] public bool aiHasDied;

    public int availableMoney;
    public int availableTickets;

    public int[] carPriceTags;
    public int[] weaponPriceTags;

    public bool[] unlockedCars;
    public bool[] unlockedWeapons;

    public float currentMusicVolume;
    public float currentSfxVolume;

    public int currentQualitySettingsIndex;

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

        data.availableMoney = availableMoney;
        data.availableTickets = availableTickets;
        data.currentMusicVolume = currentMusicVolume;
        data.currentQualitySettingsIndex = currentQualitySettingsIndex;
        data.currentSfxVolume = currentSfxVolume;
        data.selectedCarIndex = selectedCarIndex;
        data.selectedTouchControllerIndex = selectedTouchControllerIndex;
        data.selectedWeaponIndex = selectedWeaponIndex;
        data.unlockedCars = unlockedCars;
        data.unlockedWeapons = unlockedWeapons;

        bf.Serialize(saveFile, data);
        saveFile.Close();

        Debug.Log("Game data saved");
    }


    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream saveFile = File.Open(Application.persistentDataPath + "/MySaveData.dat", FileMode.Open);

            SaveData data = (SaveData)bf.Deserialize(saveFile);
            saveFile.Close();

            availableMoney = data.availableMoney;
            availableTickets = data.availableTickets;
            currentMusicVolume = data.currentMusicVolume;
            currentQualitySettingsIndex = data.currentQualitySettingsIndex;
            currentSfxVolume = data.currentSfxVolume;
            selectedCarIndex = data.selectedCarIndex;
            selectedTouchControllerIndex = data.selectedTouchControllerIndex;
            selectedWeaponIndex = data.selectedWeaponIndex;
            unlockedCars = data.unlockedCars;
            unlockedWeapons = data.unlockedWeapons;

            Debug.Log("Game data loaded");
        }

        else
        {
            Debug.Log("Save data not found");
        }
    }

}

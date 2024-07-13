using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedSystem : MonoBehaviour
{
    public SkinManager skinManager;
    public HatManager hatManager;

    public SkinData defaultSkin;
    public HatData defaultHat;

    public bool isInGame;

    public static SavedSystem instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de ScoreManager dans la scène");
            return;
        }

        instance = this;
    }

    void Start()
    {
        if (!isInGame) 
        {
            LoadDataCoin();

            SaveDataCoin();
            LoadData();
            StartCoroutine(SavedIncOntinue());
        }
     
        
    }

    
    public void SaveData()
    {
        SavedData savedData = new SavedData
        {
            skins = skinManager.GetSkinList(),
            hats = hatManager.GetHatList(),

            currentSkin = skinManager.currentSkinedEquiped,
            currentHat = hatManager.currentHatEquiped

        };

        string jsonData = JsonUtility.ToJson(savedData);
        string filePath = Application.persistentDataPath + "/SavedDataCubeRace.json";
        System.IO.File.WriteAllText(filePath, jsonData);
        Debug.Log("Sauvegarde effectu�e dans ce fichier " + filePath);
    }

    public void LoadData()
    {
        string filePath = Application.persistentDataPath + "/SavedDataCubeRace.json";
        string jsonData = System.IO.File.ReadAllText(filePath);
        SavedData savedData = JsonUtility.FromJson<SavedData>(jsonData);

        skinManager.LoadData(savedData.skins);
        hatManager.LoadData(savedData.hats);
        
        if (savedData.currentSkin == null) 
        {
            savedData.currentSkin = defaultSkin;
        }

        if (savedData.currentHat == null)
        {
            savedData.currentHat = defaultHat;
        }

        hatManager.currentHatEquiped = savedData.currentHat;
        skinManager.currentSkinedEquiped = savedData.currentSkin;
        StateNameController.currentMaterialEquiped = skinManager.currentSkinedEquiped.skinMaterial;

        StateNameController.currentHatEquiped = hatManager.currentHatEquiped.hatPrefab;


        Debug.Log("Chargement effectu�e depuis ce fichier " + filePath);
    }

    public void SaveDataCoin()
    {
        SavedDataCoin savedData = new SavedDataCoin
        {
            spheraCount = StateNameController.spheraCount,
            

        };

        string jsonData = JsonUtility.ToJson(savedData);
        string filePath = Application.persistentDataPath + "/SavedDataCubeRaceCoin.json";
        System.IO.File.WriteAllText(filePath, jsonData);
    }

    public void LoadDataCoin()
    {
        string filePath = Application.persistentDataPath + "/SavedDataCubeRaceCoin.json";
        string jsonData = System.IO.File.ReadAllText(filePath);
        SavedDataCoin savedData = JsonUtility.FromJson<SavedDataCoin>(jsonData);
        StateNameController.spheraCount = savedData.spheraCount;

    }



    IEnumerator SavedIncOntinue()
    {
        while (true)
        {
            SaveData();
            yield return new WaitForSeconds(5f);
        }

    }


    private void OnApplicationQuit()
    {
        SaveData();
        SaveDataCoin();

    }

}

public class SavedData
{
    public List<SkinData> skins;
    public List<HatData> hats;
    public SkinData currentSkin;
    public HatData currentHat;



}

public class SavedDataCoin
{
    public int spheraCount;

}



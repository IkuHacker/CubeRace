using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class SkinManager : MonoBehaviour
{
    public SkinData[] skins;
    public Transform SellButtonsParent;
    public GameObject SellButtonPrefab;
    public List<SkinData> SkinList = new List<SkinData>();
    public TextMeshProUGUI coinTextMainMenu;
    public TextMeshProUGUI coinTextShop;

    public SkinData currentSkinedEquiped;
    public Transform hatPoint;
    public GameObject model;
    public GameObject canvasShop;



    public static SkinManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de SkinManager dans la scne");
            return;
        }

        instance = this;
    }

    void Start()
    {
        canvasShop.SetActive(true);

        UpdateSkinToSell(skins);
 
        Renderer playerRend = model.GetComponent<Renderer>();
        playerRend.enabled = true;

        if (currentSkinedEquiped != null)
        {
            playerRend.sharedMaterial = currentSkinedEquiped.skinMaterial;

        }
        canvasShop.SetActive(false);

    }




    public void UpdateSkinToSell(SkinData[] skins)
    {
        for (int i = 0; i < SellButtonsParent.childCount; i++)
        {
            Destroy(SellButtonsParent.GetChild(i).gameObject);
        }

        for (int i = 0; i < skins.Length; i++)
        {
            GameObject button = Instantiate(SellButtonPrefab, SellButtonsParent);
            SellButtonSkin buttonSkin = button.GetComponent<SellButtonSkin>();
            buttonSkin.skinVisual.sprite = skins[i].skinSprite;
            buttonSkin.priceText.text = skins[i].priceSkin.ToString();
            buttonSkin.skinData = skins[i];
            if (currentSkinedEquiped == skins[i])
            {
                buttonSkin.selection.SetActive(true);
            }
            Debug.Log(currentSkinedEquiped);

        }
    }

    public List<SkinData> GetSkinList()
    {
        return SkinList;
    }

    public void LoadData(List<SkinData> savedData)
    {
        SkinList = savedData;
        UpdateSkinToSell(skins);
    }

    private void Update()
    {
        coinTextMainMenu.text = StateNameController.spheraCount.ToString();
        coinTextShop.text = StateNameController.spheraCount.ToString();

        StateNameController.spheraCount = 100; //Ligen a retiré

        Renderer playerRend = model.GetComponent<Renderer>();
        playerRend.enabled = true;

        if(currentSkinedEquiped != null) 
        {
            playerRend.sharedMaterial = currentSkinedEquiped.skinMaterial;

        }
    }



}

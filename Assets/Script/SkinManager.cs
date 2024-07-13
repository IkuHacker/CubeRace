using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SkinManager : MonoBehaviour
{
    public SkinData[] skins;
    public Transform SellButtonsParent;
    public GameObject SellButtonPrefab;
    public List<SkinData> SkinList = new List<SkinData>();
    public Text coinText;
    public SkinData currentSkinedEquiped;

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
        SkinList.Add(SavedSystem.instance.defaultSkin);
        UpdateSkinToSell(skins);
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
            buttonSkin.skinName.text = skins[i].nameSkin;
            buttonSkin.skinVisual.sprite = skins[i].skinSprite;
            buttonSkin.priceText.text = skins[i].priceSkin.ToString() + " SPHERA";
            buttonSkin.skinData = skins[i];

            if (SkinList.Contains(skins[i]))
            {
                buttonSkin.SellButton.interactable = false;
            }

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
        coinText.text = "COINS : " + StateNameController.spheraCount;
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HatManager : MonoBehaviour
{
    public HatData[] hats;
    public Transform SellButtonsParent;
    public GameObject SellButtonPrefab;
    public List<HatData> hatList = new List<HatData>();
    public HatData currentHatEquiped;

    public static HatManager instance;

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
        UpdateSkinToSell(hats);
        hatList.Add(SavedSystem.instance.defaultHat);
    }

    public void UpdateSkinToSell(HatData[] hats)
    {
        for (int i = 0; i < SellButtonsParent.childCount; i++)
        {
            Destroy(SellButtonsParent.GetChild(i).gameObject);
        }

        for (int i = 0; i < hats.Length; i++)
        {
            GameObject button = Instantiate(SellButtonPrefab, SellButtonsParent);
            SellButtonHat buttonHat = button.GetComponent<SellButtonHat>();
            buttonHat.hatName.text = hats[i].nameHat;
            buttonHat.hatVisual.sprite = hats[i].hatSprite;
            buttonHat.priceText.text = hats[i].priceHat.ToString() + " SPHERA";
            buttonHat.hatData = hats[i];

            if (hatList.Contains(hats[i]))
            {
                buttonHat.SellButton.interactable = false;
            }

        }
    }

    public List<HatData> GetHatList()
    {
        return hatList;
    }

    public void LoadData(List<HatData> savedData)
    {
        hatList = savedData;
        UpdateSkinToSell(hats);
    }

    

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class SellButtonSkin : MonoBehaviour
{
    public SkinData skinData;
    public Image skinVisual;
    public Text skinName;
    public Text priceText;
    public Button SellButton;

    public void BuySkin() 
    {
        if (StateNameController.spheraCount >= skinData.priceSkin)
        {
            StateNameController.spheraCount -= skinData.priceSkin;
            StateNameController.currentMaterialEquiped = skinData.skinMaterial;
            SkinManager.instance.SkinList.Add(skinData);
            SellButton.interactable = false;
        } 
    }


    public void EquipedSkin()
    {

        if (SkinManager.instance.SkinList.Contains(skinData))
        {
            SkinManager.instance.currentSkinedEquiped = skinData;
            StateNameController.currentMaterialEquiped = skinData.skinMaterial;
        }
    }
}

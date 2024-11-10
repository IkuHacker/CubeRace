using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class SellButtonSkin : MonoBehaviour
{
    public SkinData skinData;
    public Image skinVisual;
    public Text priceText;
    public GameObject selection;

    public void BuySkin() 
    {
        if (SkinManager.instance.SkinList.Contains(skinData)) 
        {
            EquipedSkin();
            return;
        }
        if (StateNameController.spheraCount >= skinData.priceSkin)
        {
            StateNameController.spheraCount -= skinData.priceSkin;
            StateNameController.currentMaterialEquiped = skinData.skinMaterial;
            SkinManager.instance.SkinList.Add(skinData);
            EquipedSkin();
        } 
    }


    public void EquipedSkin()
    {
        Debug.Log("Objet équipé");
        GameObject[] selectionObjects = GameObject.FindGameObjectsWithTag("SelectionSkin");
        foreach (GameObject obj in selectionObjects)
        {
            if (obj != selection) // Ne pas désactiver l'objet actuel "selection"
            {
                obj.SetActive(false);
            }
        }


        if (selection.activeSelf)
        {
            selection.SetActive(false);
            Renderer playerRend = SkinManager.instance.model.GetComponent<Renderer>();
            playerRend.enabled = true;
            playerRend.sharedMaterial = SavedSystem.instance.defaultSkin.skinMaterial;

            SkinManager.instance.currentSkinedEquiped = SavedSystem.instance.defaultSkin;
            StateNameController.currentMaterialEquiped = SavedSystem.instance.defaultSkin.skinMaterial;
        }
        else
        {
            // Assurez-vous que tous les anciens skins sont désactivés/détruits ici.
            selection.SetActive(true);
            Renderer playerRend = SkinManager.instance.model.GetComponent<Renderer>();
            playerRend.enabled = true;
            playerRend.sharedMaterial = skinData.skinMaterial;

            SkinManager.instance.currentSkinedEquiped = skinData;
            StateNameController.currentMaterialEquiped = skinData.skinMaterial;
        }


    }

}

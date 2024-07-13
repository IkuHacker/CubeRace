using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SellButtonHat : MonoBehaviour
{
    public HatData hatData;
    public Image hatVisual;
    public Text hatName;
    public Text priceText;
    public Button SellButton;

    public void BuyHat()
    {
        if (StateNameController.spheraCount >= hatData.priceHat)
        {
            StateNameController.spheraCount -= hatData.priceHat;
            StateNameController.currentHatEquiped = hatData.hatPrefab;
            HatManager.instance.hatList.Add(hatData);
            SellButton.interactable = false;
        }
    }


    public void EquipedHat()
    {

        if (HatManager.instance.hatList.Contains(hatData))
        {
            HatManager.instance.currentHatEquiped = hatData;
            StateNameController.currentHatEquiped = hatData.hatPrefab;
        }
    }
}

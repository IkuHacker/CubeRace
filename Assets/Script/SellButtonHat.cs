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
            Destroy(HatManager.instance.hatOnModel);

            GameObject hatObject = Instantiate(hatData.hatPrefab, SkinManager.instance.hatPoint.position, Quaternion.identity);
            hatObject.transform.parent = SkinManager.instance.hatPoint;
            hatObject.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);

            HatManager.instance.hatOnModel = hatObject;

            HatManager.instance.currentHatEquiped = hatData;
            StateNameController.currentHatEquiped = hatData.hatPrefab;
        }
    }
}

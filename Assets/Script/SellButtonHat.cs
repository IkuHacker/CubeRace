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
    public GameObject selection;


    public void BuyHat()
    {
        if (HatManager.instance.hatList.Contains(hatData))
        {
            EquipedHat();
            return;
        }
        if (StateNameController.spheraCount >= hatData.priceHat)
        {
            StateNameController.spheraCount -= hatData.priceHat;

            
            StateNameController.currentHatEquiped = hatData.hatPrefab;
            HatManager.instance.hatList.Add(hatData);
            EquipedHat();
        }
    }


    public void EquipedHat()
    {

        if (HatManager.instance.hatList.Contains(hatData))
        {
            GameObject[] selectionObjects = GameObject.FindGameObjectsWithTag("SelectionHat");
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
                Destroy(HatManager.instance.hatOnModel);
                HatManager.instance.hatOnModel = null;

                HatManager.instance.currentHatEquiped = null;
                StateNameController.currentHatEquiped = null;
            }
            else
            {
                selection.SetActive(true);
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
}

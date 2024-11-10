using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    public MainMenu mainMenu;
    public void OpenShopButton()
    {
        mainMenu.buttonCanva.SetActive(false);
        mainMenu.shopWindow.SetActive(true);

    }

    public void StartGame()
    {
        mainMenu.StartGame();

    }

}

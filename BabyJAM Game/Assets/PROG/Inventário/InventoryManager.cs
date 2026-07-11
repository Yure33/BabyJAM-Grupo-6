using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    private bool MenuActivaded;

    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetButtonDown("Inventory") && MenuActivaded)
        {
            InventoryMenu.SetActive(false);
            MenuActivaded = false;
        }

        else if (Input.GetButtonDown("Inventory") && !MenuActivaded)
        {
            InventoryMenu.SetActive(true);
            MenuActivaded = true;
    }   } 
    
        public void AddItem(string itemName, int quantity, Sprite itemSprite)
    {
        Debug.Log("itemName = " + itemName + "quantity = " + quantity + quantity + "itemSprite = " + itemSprite);
}   }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    private bool MenuActivaded;

    public void OpenInventory(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            switch (MenuActivaded)
            {
                case false:
                    InventoryMenu.SetActive(true);
                    MenuActivaded = true;
                    break;
                case true:
                    InventoryMenu.SetActive(false);
                    MenuActivaded = false;
                    break;
            }
        }
    }
}

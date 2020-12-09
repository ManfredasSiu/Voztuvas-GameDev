using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] Text healthPotionsText;
    [SerializeField] InventoryController inventoryController;

    private void Start()
    {
        inventoryController.inventoryUpdateEvent += OnInventoryChange;
    }

    void OnInventoryChange(int amount, Items itemType)
    {
        switch (itemType)
        {
            case Items.HealthPotion:
                healthPotionsText.text = amount.ToString();
                break;
            // Extension point in case we add more items that should be visible in the UI
        }
    }
}

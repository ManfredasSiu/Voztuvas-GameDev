using System.Collections;
using Assets.Scripts.Entities;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryController : MonoBehaviour
{
    // Start is called before the first frame update

    public int potions = 0;
    public int potionHealingPower = 50;

    public Action<int, Items> inventoryUpdateEvent;

    // Called each tick, reacts to keyboard input to consume items.
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        { 
            ConsumePotion();
        }
    }

    public void ConsumePotion()
    {
        var HPControler = gameObject.GetComponent<HealthController>();
        if (potions >= 0 && HPControler.CanHeal())
        {
            HPControler.Heal(potionHealingPower);
            potions--;
            updateInventory(potions, Items.HealthPotion);
        }
    }

    public void AddItem(Items item)
    {
        switch (item)
        {
            case Items.HealthPotion:
                potions++;
                updateInventory(potions, Items.HealthPotion);
                break;
        }
    }

    void updateInventory(int amount, Items itemType)
    {
        inventoryUpdateEvent?.Invoke(amount, itemType);
    }
}

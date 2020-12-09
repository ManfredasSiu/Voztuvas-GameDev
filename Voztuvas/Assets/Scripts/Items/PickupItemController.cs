using Assets.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItemController : MonoBehaviour
{
    public Items itemType;
    private GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = PlayerInfo.instance.player;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _player.GetComponent<InventoryController>().AddItem(itemType);
            Destroy(gameObject);
        }
    }
}

public enum Items
{
    HealthPotion,
    SanityPills,
    Ammo,
    Weapon
}

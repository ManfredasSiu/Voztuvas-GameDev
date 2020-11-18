using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    // Trap object that will be activated when player interacts with its trigger.
    public GameObject trapObject;
    public GameObject trapStopper;
    bool trapExpired = false;

    // Activates the trap object. For example, boulders or spiders fall down.
    void EnableTrap()
    {
        trapObject.GetComponent<Rigidbody2D>().simulated = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!trapExpired)
        {
            if (trapObject != null)
            {
                EnableTrap();
            }

            if (trapStopper != null)
            {
                // Make the trap stopper disappear
                trapStopper.SetActive(false);
            }

            trapExpired = true;
        }   
    }
}

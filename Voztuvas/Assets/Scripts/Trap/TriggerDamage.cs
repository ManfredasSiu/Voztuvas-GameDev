using Assets.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDamage : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter(Collider other)
    {
        var health = other.gameObject.GetComponent<HealthController>();
        if(health != null && other.gameObject.tag == "Player" || other.gameObject.tag == "Mob")
        {
            health.ApplyDamage(damage);
            var rb = health.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.AddForceAtPosition(new Vector2(20, 10), this.transform.position);
        }
        Destroy(gameObject, 0.5f);
    }
}

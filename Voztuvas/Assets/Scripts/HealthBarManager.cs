using Assets.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{

    [SerializeField] Slider healthSlider;
    [SerializeField] HealthController healthController;

    private void Start()
    {
        healthController.healthUpdateEvent += OnDamageDone;
    }

    void OnDamageDone(float health)
    {
        healthSlider.value = health;
    }
}

using System;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class HealthController : MonoBehaviour
    {
        [SerializeField]
        private float _health = 100;

        public Action<float> healthUpdateEvent;

        public void ApplyDamage(float damage)
        {
            _health -= damage;
            healthUpdateEvent?.Invoke(_health);
            if (_health <= 0)
                Destroy(gameObject);
        }
    }
}
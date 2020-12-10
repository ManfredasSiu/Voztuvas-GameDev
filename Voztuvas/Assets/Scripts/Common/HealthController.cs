using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Entities
{
    public class HealthController : MonoBehaviour
    {
        [SerializeField]
        private float _health = 100;
        public float maxHealth = 100;

        public Action<float> healthUpdateEvent;

        public void ApplyDamage(float damage)
        {
            _health -= damage;
            updateHealth();
            if (_health <= 0)
            {
                if (gameObject.tag == "Player")
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                else
                {
                    if (transform.parent == null)
                        Destroy(gameObject);
                    else
                        Destroy(transform.parent.gameObject);
                }
            }
        }

        public bool CanHeal()
        {
            return _health < maxHealth;
        }

        public void Heal(int amount)
        {
            _health = Math.Min(maxHealth, _health + amount);
            updateHealth();
        }

        void updateHealth()
        {
            healthUpdateEvent?.Invoke(_health);
        }
    }
}
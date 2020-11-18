using System;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            {
                if (gameObject.tag == "Player")
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class HealthController : MonoBehaviour
    {
        [SerializeField]
        private float _health = 100;

        void ApplyDamage(float damage)
        {
            _health -= damage;

            if (_health <= 0)
                Destroy(gameObject);
        }
    }
}
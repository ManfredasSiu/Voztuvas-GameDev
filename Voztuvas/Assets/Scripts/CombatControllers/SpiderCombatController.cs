using Assets.Scripts.AnimationControllers;
using Assets.Scripts.Entities;
using UnityEngine;

namespace Assets.Scripts.CombatControllers
{
    public class SpiderCombatController : MonoBehaviour
    {
        [SerializeField] private float _attackRate = 2000;
        [SerializeField] private float _attackDamage = 7;

        private Rigidbody2D _rigidbody;
        private SpiderAnimationController _animationController;

        private float _nextAttack = 0;

        // Start is called before the first frame update
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animationController = GetComponent<SpiderAnimationController>();
        }


        public void AttackTarget(GameObject target)
        {
            if (Time.time >= _nextAttack)
            {
                _animationController.AttackAnimation();
                target.GetComponent<HealthController>().ApplyDamage(7);

                _nextAttack = Time.time + _attackRate;
            }
        }
    }
}
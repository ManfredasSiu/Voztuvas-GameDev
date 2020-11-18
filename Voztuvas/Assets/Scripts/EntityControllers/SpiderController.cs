using Assets.Scripts.AnimationControllers;
using Assets.Scripts.CombatControllers;
using UnityEngine;

namespace Assets.Scripts.EntityControllers
{
    public class SpiderController : MonoBehaviour
    {
        [SerializeField] private float _visionRange = 10;
        [SerializeField] private float _attackRange = 1;
        [SerializeField] private GameObject _target;

        private SpiderMovementController _movementController;
        private SpiderCombatController _combatController;

        void Start()
        {
            _movementController = GetComponent<SpiderMovementController>();
            _combatController = GetComponent<SpiderCombatController>();
        }

        // Update is called once per frame
        void Update()
        {
            if (TargetInAttackRange())
            {
                _combatController.AttackTarget(_target);
                _movementController.StopMoving();
            }
            else if (TargetInVisionRange())
            {
                _movementController.MoveToTarget(_target.transform);
            }
            else
            {
                _movementController.StopMoving();
            }
        }

        private bool TargetInVisionRange()
        {
            return Vector3.Distance(transform.position, _target.transform.position) <= _visionRange;
        }

        private bool TargetInAttackRange()
        {
            return Vector3.Distance(transform.position, _target.transform.position) <= _attackRange;
        }
    }
}
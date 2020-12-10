using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Entities;
using UnityEngine;

namespace Assets.Scripts.SpiderControllers
{
    public class CombatController : MonoBehaviour
    {
        public float TimeBetweenAttacks = 2;
        public float AttackRange = 1;
        public float AttackStrength = 20f;
        public List<LegController> LegControllers;
        public List<GameObject> Legs;
        public GameObject Target;

        private bool _isAttacking = false;
        private float _nextAttack = 0;

        void Start()
        {
            Target = PlayerInfo.instance.player;

            StartCoroutine(TryAttack());
        }

        void Update()
        {
            
        }

        IEnumerator TryAttack()
        {
            for (; ; )
            {
                if (!_isAttacking && !IsAttackInCooldown())
                    for (var i = 0; i < Legs.Count; i++)
                    {
                        if (Vector3.Distance(Target.transform.position, Legs[i].transform.position) <= AttackRange &&
                            LegControllers[i].CanStep)
                        {
                            Attack(i);
                            yield return new WaitForSeconds(1);
                            LegControllers[i].TryMoveLeg();
                            break;
                        }
                    }

                yield return new WaitForSeconds(.3f);
            }
        }

        bool IsAttackInCooldown()
        {
            return _nextAttack > Time.time;
        }

        void Attack(int legId)
        {
            var targetPos = Target.transform.position;
            targetPos += new Vector3(0, 0.3f, 0); // hit just a bit above ground.

            LegControllers[legId].MoveLeg(targetPos);

            _nextAttack = Time.time + TimeBetweenAttacks;

            Target.GetComponent<HealthController>().ApplyDamage(AttackStrength);
        }

    }
}
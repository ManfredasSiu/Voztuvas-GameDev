using Assets.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharCombatController : MonoBehaviour
{
    CharAnimationController CAC;

    public Meele_Weapon_Object Weapon;

    private CapsuleCollider2D charCollider;

    float NextAttack = 0;
    float direction = -1;

    void Start()
    {
        charCollider = this.GetComponent<CapsuleCollider2D>();
        CAC = this.GetComponent<CharAnimationController>();
    }
    // Update is called once per frame
    void Update()
    {
        NextAttack += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && Weapon.cooldown <= NextAttack)
        {
            NextAttack = 0;
            Invoke("AttackCommit", 0);
        }
    }

    void AttackCommit()
    {
        CAC.AttackAnimation(Weapon.cooldown);
        if (Weapon.Meele)
            MeeleAttack();
    }

    private void OnDrawGizmos()
    {
        if (charCollider != null)
            Gizmos.DrawCube(new Vector2(this.transform.position.x + 1f*direction, this.transform.position.y), charCollider.bounds.size);
    }

    void MeeleAttack()
    {
        if (CAC.LookingRight)
            direction = 1;
        else
            direction = -1;

        var hit = Physics2D.BoxCastAll(new Vector2(this.transform.position.x + 1f*direction, this.transform.position.y), charCollider.bounds.size, 0, Vector2.up);
        if(hit != null)
        {
            foreach(var col in hit)
            {
                var health = col.collider.gameObject.GetComponent<HealthController>();
                if(health != null)
                {
                    health.ApplyDamage(Weapon.DMG);
                }
            }
        }
    }
}

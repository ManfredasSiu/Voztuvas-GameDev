using Assets.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharCombatController : MonoBehaviour
{
    CharAnimationController CAC;

    public Meele_Weapon_Object Sword;

    public weapons currentlyEquiped;

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
        if (Input.GetKey(KeyCode.Space) && Sword.cooldown <= NextAttack && currentlyEquiped != weapons.None)
        {
            NextAttack = 0;
            Invoke("AttackCommit", 0);
        }
        if(Input.GetKey(KeyCode.Q))
        {
            var raylist =Physics2D.BoxCastAll(transform.position, new Vector2(1f, 1f), 0, Vector2.down);
            foreach (var ray in raylist)
            {
                if (ray.collider != null)
                {
                    var weap = ray.collider.gameObject.GetComponent<WeaponEquip>();
                    if (weap != null)
                    {
                        currentlyEquiped = weap.weapons;
                        Destroy(weap.gameObject);
                    }
                }
            }
        }
    }

    void AttackCommit()
    {
        CAC.AttackAnimation(Sword.cooldown);
        if (Sword.Meele)
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
                    health.ApplyDamage(Sword.DMG);
                }
            }
        }
    }
}

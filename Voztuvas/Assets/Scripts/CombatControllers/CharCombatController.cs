using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharCombatController : MonoBehaviour
{
    CharAnimationController CAC;

    public Meele_Weapon_Object Weapon;
    // Start is called before the first frame update

    float NextAttack;

    void Start()
    {
        NextAttack = Time.time;
        CAC =this.GetComponent<CharAnimationController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time >= NextAttack)
        {
            AttackCommit();
        }
    }

    void AttackCommit()
    {
        NextAttack += Weapon.cooldown;
        CAC.AttackAnimation(Weapon.cooldown);
    }
}

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
    }
}

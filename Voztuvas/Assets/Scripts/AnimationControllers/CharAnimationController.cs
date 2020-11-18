using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAnimationController : MonoBehaviour
{

    public bool LookingRight = true;
    private bool Walking = false;
    private bool Running = false;
    private bool Jumping = false;
    private bool Falling = false;

    Animator Anim;

    private void Start()
    {
        Anim = this.GetComponent<Animator>(); 
    }

    public void ChangeDirection(float xAxis)
    {
        if (LookingRight && xAxis < 0)
        {
            this.transform.eulerAngles = new Vector3(0, 180, 0);
            LookingRight = false;
        }
        else if (!LookingRight && xAxis > 0)
        {
            this.transform.eulerAngles = new Vector3(0, 0, 0);
            LookingRight = true;
        }
    }

    public void AttackAnimation(float AttackSpeed)
    {
        //float currentMult = Anim.GetFloat("MeeleAttack");
        
        Anim.SetFloat("MeeleAttack", 0.5f/AttackSpeed);
        Anim.SetTrigger("Attack");
        
    }
    
    public void GroundMovingAnimation(float xAxis, bool isGrounded)
    {
        if (Input.GetKey(KeyCode.LeftShift) && xAxis != 0 && isGrounded)
        {
            Running = true;
            Anim.SetBool("Running", true);
        }
        else if (xAxis != 0 && isGrounded)
        {
            Walking = true;
            Anim.SetBool("Walking", true);
        }
        else if (xAxis == 0 && ( Running || Walking))
        {
            Debug.Log(xAxis);
            Running = false;
            Anim.SetBool("Running", false);
            Walking = false;
            Anim.SetBool("Walking", false);
        }
    }

    public void JumpAnimation(float VelocityY, bool isGrounded)
    {
        if (VelocityY > 0 && !isGrounded)
        {
            Jumping = true;
            Falling = false;
            Anim.SetBool("Jumping", true);
            Anim.SetBool("Falling", false);
        }
        else if (VelocityY < 0 && !isGrounded)
        {
            Jumping = false;
            Falling = true;
            Anim.SetBool("Jumping", false);
            Anim.SetBool("Falling", true);
        }
        else
        {
            Jumping = false;
            Falling = false;
            Anim.SetBool("Jumping", false);
            Anim.SetBool("Falling", false);
        }
    }
}

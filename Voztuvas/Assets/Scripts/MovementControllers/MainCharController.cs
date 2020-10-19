using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharController : MonoBehaviour
{
    private Rigidbody2D RB;
    private Animator Anim;
    private Collider2D Coll;
    private AudioSource AS;

    private bool Walking = false;
    private bool Running = false;
    private bool Jumping = false;
    private bool Falling = false;
    private bool LookingRight = true; 

    [SerializeField]
    private float Speed = 2000;
    [SerializeField]
    private float JumpPower = 2000;
    [SerializeField]
    private float RunningMultiplier = 2f;
    [SerializeField]
    private LayerMask layer;

    // Start is called before the first frame update
    void Start()
    {
        RB = this.GetComponent<Rigidbody2D>();
        Anim = this.GetComponent<Animator>();
        Coll = this.GetComponent<CapsuleCollider2D>();
        AS = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        Xmovement(xAxis);
        if (isGrounded())
            Ymovement(yAxis);

        ChangeDirection(xAxis);
        JumpAnimation(yAxis);
        GroundMovingAnimation(xAxis);
    }

    void ChangeDirection(float xAxis)
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

    void GroundMovingAnimation(float xAxis)
    {
        if (Input.GetKey(KeyCode.LeftShift) && xAxis != 0 && RB.velocity.y == 0)
        {
            Running = true;
            Anim.SetBool("Running", true);
        }
        else if (xAxis != 0 && RB.velocity.y == 0)
        {
            Walking = true;
            Anim.SetBool("Walking", true);
        }
        else if (Running == true || Walking == true)
        {
            Running = false;
            Anim.SetBool("Running", false);
            Walking = false;
            Anim.SetBool("Walking", false);
        }
    }

    void Xmovement(float xAxis)
    {
        float multiplier = 1;
        if (Running)
            multiplier = RunningMultiplier;
        RB.AddForce(Time.deltaTime * new Vector2(xAxis, 0) * Speed * multiplier);
    }

    void JumpAnimation(float yAxis)
    {
        if (RB.velocity.y > 0)
        {
            Jumping = true;
            Falling = false;
            Anim.SetBool("Jumping", true);
            Anim.SetBool("Falling", false);
        }
        else if (RB.velocity.y < 0)
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

    void Ymovement(float yAxis)
    {
        RB.AddForce(Time.deltaTime * new Vector2(0, yAxis * JumpPower), ForceMode2D.Impulse);
        RB.velocity = Vector2.ClampMagnitude(RB.velocity, 7);
    }

    bool isGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(Coll.bounds.center, Coll.bounds.size, 0, Vector2.down, 0.1f, layer.value);
        return hit.collider != null;
    }

}

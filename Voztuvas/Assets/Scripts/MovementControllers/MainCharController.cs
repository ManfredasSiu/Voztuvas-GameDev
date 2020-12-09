using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharController : MonoBehaviour
{
    private Rigidbody2D RB;
    private Animator Anim;
    private Collider2D Coll;
    private AudioSource AS;
    public bool grounded;

    private CharAnimationController CAC;

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
        CAC = this.GetComponent<CharAnimationController>();
    }

    // Update is called once per frame
    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        CAC.ChangeDirection(xAxis);
        CAC.GroundMovingAnimation(xAxis, isGrounded());
        CAC.JumpAnimation(RB.velocity.y, isGrounded());

        Xmovement(xAxis);
        grounded = isGrounded();
        if (grounded)
            Ymovement(yAxis);
    }

    void Xmovement(float xAxis)
    {
        float multiplier = 1;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            multiplier = RunningMultiplier;
        }
        float clamp = 4;

        RB.AddForce(new Vector2(xAxis * Speed * multiplier * Time.deltaTime, 0));
        
        RB.velocity = new Vector2(Mathf.Clamp(RB.velocity.x, -clamp * multiplier, clamp * multiplier), RB.velocity.y );
        //RB.velocity = new Vector2(xAxis * Speed * multiplier * Time.deltaTime, RB.velocity.y);
        //Debug.Log(RB.velocity);
        if(xAxis == 0 && RB.velocity.y==0)
            RB.velocity = new Vector2(0, RB.velocity.y);
    }

    void Ymovement(float yAxis)
    {
        if (yAxis > 0)
        {
            RB.velocity = new Vector2(RB.velocity.x, 0);

            RB.AddForce(new Vector2(0, Time.deltaTime * JumpPower), ForceMode2D.Impulse);

            RB.velocity = new Vector2(RB.velocity.x, Mathf.Clamp(RB.velocity.y, -10, +10));
        }
    }

    private void OnDrawGizmos()
    {
        if(Coll != null)
            Gizmos.DrawCube(new Vector2(Coll.bounds.center.x, Coll.bounds.min.y), new Vector3(Coll.bounds.size.x*0.8f, 0.1f));
    }

    bool isGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(new Vector2(Coll.bounds.center.x, Coll.bounds.min.y), new Vector3(Coll.bounds.size.x*0.8f, 0.1f), 0, Vector2.down, 0.1f, layer.value);
        return hit.collider != null;
    }

}

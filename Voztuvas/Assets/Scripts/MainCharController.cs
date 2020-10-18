using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharController : MonoBehaviour
{
    private Rigidbody2D RB;
    [SerializeField]
    private float Speed = 1000;
    // Start is called before the first frame update
    void Start()
    {
        RB = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
         RB.AddForce(Time.deltaTime * new Vector2(Input.GetAxis("Horizontal"), RB.velocity.y) * Speed);
    }
}

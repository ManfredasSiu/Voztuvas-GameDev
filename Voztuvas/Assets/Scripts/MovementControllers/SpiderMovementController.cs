using Assets.Scripts.AnimationControllers;
using UnityEngine;

public class SpiderMovementController : MonoBehaviour
{
    private Rigidbody2D RB;
    private SpiderAnimationController CAC;

    [SerializeField] private float _speed = 2000;
    [SerializeField] private float _visionRange = 10;
    [SerializeField] private Transform _target;

    // Start is called before the first frame update
    void Start()
    {
        RB = this.GetComponent<Rigidbody2D>();
        this.GetComponent<Animator>();
        this.GetComponent<BoxCollider2D>();
        CAC = this.GetComponent<SpiderAnimationController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TargetInRange())
        {
            CAC.GroundMovingAnimation(isMoving: true);
            MoveToTarget();
        }
        else
        {
            CAC.GroundMovingAnimation(isMoving: false);
        }
    }

    private bool TargetInRange()
    {
        return Vector3.Distance(transform.position, _target.position) <= _visionRange;
    }

    private void MoveToTarget()
    {
        var direction = transform.position.x > _target.position.x ? -1 : 1;
        CAC.ChangeDirection(-direction);

        //move towards target
        RB.AddForce(Time.deltaTime * new Vector2(direction, 0) * _speed);
    }
}

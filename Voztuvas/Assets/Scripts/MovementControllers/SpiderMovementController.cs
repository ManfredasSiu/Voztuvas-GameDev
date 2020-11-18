using Assets.Scripts.AnimationControllers;
using UnityEngine;

public class SpiderMovementController : MonoBehaviour
{
    [SerializeField] private float _speed = 2000;

    private Rigidbody2D _rigidbody;
    private SpiderAnimationController _animationController;

    private Transform _target;
    private bool _isMoving;


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = this.GetComponent<Rigidbody2D>();
        _animationController = this.GetComponent<SpiderAnimationController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isMoving)
        {
            _animationController.GroundMovingAnimation(isMoving: true);
            MoveToTarget();
        }
        else
        {
            _animationController.GroundMovingAnimation(isMoving: false);
        }
    }

    public void MoveToTarget(Transform target)
    {
        _target = target;
        _isMoving = true;
    }

    public void StopMoving()
    {
        _isMoving = false;
    }

    private void MoveToTarget()
    {
        var direction = transform.position.x > _target.position.x ? -1 : 1;
        _animationController.ChangeDirection(-direction);

        //move towards target
        _rigidbody.AddForce(Time.deltaTime * new Vector2(direction, 0) * _speed);
    }
}

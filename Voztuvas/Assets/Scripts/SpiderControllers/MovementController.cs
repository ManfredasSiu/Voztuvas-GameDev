using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.SpiderControllers
{
    public class MovementController : MonoBehaviour
    {
        public LayerMask GroundMask;
        public float RunSpeed = 10f;
        public float RotationDelta = 1;
        public List<GameObject> LeftLegs = new List<GameObject>();
        public List<GameObject> RightLegs = new List<GameObject>();

        public List<LegController> LeftLegsControllers = new List<LegController>();
        public List<LegController> RightLegsControllers = new List<LegController>();

        private Rigidbody2D _rb;
        private float _xAxis;
        private float _yAxis;
        private bool _canMove = true;

        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();

            DisableSameSideLegsOnStep(LeftLegsControllers);
            DisableSameSideLegsOnStep(RightLegsControllers);
        }

        void Update()
        {
            _xAxis = Input.GetAxis("Horizontal");
            _yAxis = Input.GetAxis("Vertical");

            if (Input.GetKey(KeyCode.Space))
                StartCoroutine(DropFromCeiling());

            if (_canMove)
            {
                Run(new Vector2(_xAxis, _yAxis));

                var optimalRotation = ComputeOptimalRotation();

                if (Math.Abs(transform.rotation.z - optimalRotation) >= RotationDelta)
                {
                    RotateBody(optimalRotation);
                }
            }
        }

        private IEnumerator DropFromCeiling()
        {
            _canMove = false;
            var firstLegs = Random.Range(0, 1) * 2;

            var legs = LeftLegs.Concat(RightLegs).ToList();
            var controllers = LeftLegsControllers.Concat(RightLegsControllers).ToList();

            controllers.ToList()
                .ForEach(leg => leg.CanStep = false);

            legs.Take(2).ToList()
                .ForEach(leg => leg.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic);

            yield return new WaitForSeconds(0.3f);
            _rb.gravityScale = 1;



            transform.rotation = new Quaternion();

            legs.Skip(firstLegs).Take(2).ToList()
                .ForEach(leg => leg.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic);
        }

        private void StandUp()
        {
            _canMove = true;
            _rb.gravityScale = 0;

            LeftLegsControllers.ForEach(leg => leg.CanStep = true);
            RightLegsControllers.ForEach(leg => leg.CanStep = true);
            LeftLegs.ForEach(leg => leg.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static);
            RightLegs.ForEach(leg => leg.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static);
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log(collision.collider);
            if (!_canMove && _rb.IsTouchingLayers(GroundMask.value))
            {
                StandUp();
            }
        }

        private void Run(Vector2 dir)
        {
            _rb.velocity = new Vector2(dir.x * RunSpeed, dir.y * RunSpeed);
        }

        void DisableSameSideLegsOnStep(List<LegController> legs)
        {
            foreach (var leg in legs)
            {
                leg.OnStepStarted = () =>
                {
                    var legC = legs.Single(l => l != leg).GetComponent<LegController>();
                    legC.CanStep = false;
                };

                leg.OnStepEnded = () =>
                {
                    var legC = legs.Single(l => l != leg).GetComponent<LegController>();
                    legC.CanStep = true;
                    if (legC.WouldLikeToStep)
                        legC.TryMoveLeg();
                };
            }
        }

        private void RotateBody(float rotation)
        {
            transform.eulerAngles = new Vector3(0, 0, rotation);
        }

        private float ComputeOptimalRotation()
        {
            var leftLegs = LegsSum(LeftLegs);
            var rightLegs = LegsSum(RightLegs);

            if (leftLegs.x > rightLegs.x)
            {
                return 180f - Quaternion.LookRotation((leftLegs - rightLegs).normalized).eulerAngles.x;
            } 

            return Quaternion.LookRotation((leftLegs - rightLegs).normalized).eulerAngles.x;
        }

        private Vector3 LegsSum(IEnumerable<GameObject> legs) =>
            legs
                .Select(leg => leg.transform.position)
                .Aggregate((current, point) => current + point);
    }
}

using System;
using System.Collections;
using Pathfinding;
using UnityEngine;

namespace Assets.Scripts.SpiderControllers
{
    public class LegController : MonoBehaviour
    {
        public bool CanStep = true;
        public bool WouldLikeToStep = false;
        public float StepSize = 2f;
        public LayerMask GroundMask;
        public Transform Leg;
        public Transform Body;
        public Action OnStepStarted;
        public Action OnStepEnded;
        public float VelScale = 1;

        private bool _isStepping = false;


        void Start()
        {
            transform.position = Leg.position;
        }

        void Update()
        {
            if (_isStepping || !CanStep) return;

            WouldLikeToStep = false;

            TryMoveLeg();
        }

        Vector2? GetTargetPoint()
        {
            var rotation = Body.TransformDirection(Vector3.down);
            var velocity = ToVector3(Body.GetComponent<AIPath>().desiredVelocity.normalized)*0.35f;

            var forDebug = new Vector3(rotation.x, rotation.y, rotation.z) + velocity;
            forDebug.Scale(new Vector3(30, 30, 30));
            Debug.DrawRay(transform.position + Reversed(rotation), forDebug, Color.red);

            var raycast = Physics2D
                .Raycast(transform.position + Reversed(rotation), rotation + velocity, 4f, GroundMask);

            return raycast.transform == null ? (Vector2?)null : raycast.point;
        }

        public void TryMoveLeg()
        {
            var target = GetTargetPoint();

            if (target != null)
                Debug.DrawLine(ToVector3(target.Value), ToVector3(target.Value) + Vector3.up, Color.red);

            if (target != null && Vector2.Distance(Leg.position, target.Value) >= StepSize)
                MoveLeg(target.Value);
        }
        public void MoveLeg(Vector2 target)
        {
            if (CanStep)
            {
                OnStepStarted?.Invoke();
                StartCoroutine(MoveLegCoroutine(target));
            }
            else
                WouldLikeToStep = true;
        }

        IEnumerator MoveLegCoroutine(Vector2 target)
        {
            if (!CanStep || _isStepping)
                yield break;

            _isStepping = true;
            var stepTarget = target;

            Leg.position = Vector3.Lerp(Leg.position, Body.position, Time.deltaTime * 7);
            yield return null;

            while (Vector3.Distance(stepTarget, Leg.position) > 0.1f)
            {
                Leg.position = Vector3.Lerp(Leg.position, stepTarget, Time.deltaTime * 7);
                yield return null;
            }

            Leg.position = stepTarget;

            _isStepping = false;
            OnStepEnded?.Invoke();
        }

        Vector3 Reversed(Vector3 vector)
        {
            return new Vector3(-1.5f * vector.x, -1.5f * vector.y, -1.5f*vector.z);
        }

        Vector3 ToVector3(Vector2 vector)
        {
            return new Vector3(vector.x, vector.y, 0);
        }
    }
}
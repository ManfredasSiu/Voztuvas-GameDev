using UnityEngine;

namespace Assets.Scripts.AnimationControllers
{
    public class SpiderAnimationController : MonoBehaviour
    {
        private bool _lookingRight = true;
        private bool _walking = false;

        Animator Anim;

        private void Start()
        {
            Anim = this.GetComponent<Animator>();
        }

        public void ChangeDirection(float xAxis)
        {
            if (_lookingRight && xAxis < 0)
            {
                this.transform.eulerAngles = new Vector3(0, 180, 0);
                _lookingRight = false;
            }
            else if (!_lookingRight && xAxis > 0)
            {
                this.transform.eulerAngles = new Vector3(0, 0, 0);
                _lookingRight = true;
            }
        }

        public void GroundMovingAnimation(bool isMoving)
        {
            _walking = isMoving;
            Anim.SetBool("Walking", isMoving);
        }

        public void AttackAnimation()
        {

        }
    }
}
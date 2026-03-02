using UnityEngine;

namespace GameCharacter
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(GravityBody))]
    public class Character : MonoBehaviour, ICharacter, IGravityOrientable
    {
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _jumpForce = 7f;
        [SerializeField] private float _alignSpeed = 5f;
        [SerializeField] private float _groundCheckDistance = 1.1f;
        [SerializeField] private float _groundSphereRadius = 0.45f;

        private IMover _mover;
        private IGroundChecker _groundChecker;
        private IGravityAligner _gravityAligner;
        private GravityBody _gravityBody;
        private bool _jumpPressed;

        private void Awake()
        {
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.freezeRotation = true;

            _gravityBody = GetComponent<GravityBody>();

            _groundChecker = new GroundChecker(transform,
                _gravityBody,
                _groundCheckDistance,
                _groundSphereRadius);

            _mover = new Mover(transform, rigidbody, _gravityBody);
        }

        private void OnEnable()
        {
            _gravityAligner?.Enable();
        }

        private void OnDisable()
        {
            _gravityAligner?.Disable();
        }

        public void Initialize(IUpdateService updateService)
        {
            _gravityAligner = new GravityAligner(transform, _gravityBody, updateService, _alignSpeed);

            if (enabled)
                _gravityAligner.Enable();
        }

        public void Look(Vector2 input, float sensitivity, float deltaTime)
        {
            float horizontal = input.x * sensitivity * deltaTime;
            transform.Rotate(Vector3.up * horizontal, Space.Self);
        }

        public void Jump() =>
            _jumpPressed = true;

        public void Move(Vector2 input)
        {
            if (_mover == null)
                return;

            Vector3 gravityUp = -_gravityBody.GetGravityDirection();
            _mover?.Move(input, _moveSpeed);

            if (_jumpPressed && _groundChecker.IsGround())
            {
                _mover?.Jump(_jumpForce, gravityUp);
                _jumpPressed = false;
            }
        }

        private void OnDrawGizmosSelected()
        {

        }
    }
}
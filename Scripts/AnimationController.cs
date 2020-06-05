using UnityEngine;

public class AnimationController : MonoBehaviour
{
    #region Serialized
#pragma warning disable CS0649
    [SerializeField]
    private Rigidbody2D _rigidbody;
    [SerializeField, Range(5, 20)]
    private float _maxSpeed = 10f;
    [SerializeField]
    private BoolVariable _isAttacking;
    [SerializeField]
    private BoolVariable _isOnGround;
    [SerializeField]
    private BoolVariable _isPreparingJump;

    [SerializeField]
    private GameEvent _footstepSound;
#pragma warning restore CS0649
    #endregion

    #region Private
    private Animator _animator;
    private int _runningId;
    private int _jumpingId;
    private int _attackingId;
    private int _onGroundId;
    private int _preparingJumpId;
    #endregion

    #region Unity Lifecycle
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _runningId = Animator.StringToHash("VelocityX");
        _jumpingId = Animator.StringToHash("VelocityY");
        _attackingId = Animator.StringToHash("isAttacking");
        _onGroundId = Animator.StringToHash("isOnGround");
        _preparingJumpId = Animator.StringToHash("isPreparingJump");
    }

    private void Update()
    {
        Run();
        Jump();
        Attack();
        PrepareJumpNLand();
    }
    #endregion

    #region MyFunctions
    private void Run()
    {
        float velocityX = _rigidbody.velocity.x;
        _animator.SetFloat(_runningId, Mathf.Abs(velocityX) / _maxSpeed);
    }

    private void Jump()
    {
        float velocityY = _rigidbody.velocity.y;
        _animator.SetFloat(_jumpingId, Mathf.Abs(velocityY));
    }

    private void Attack()
    {
        _animator.SetBool(_attackingId, _isAttacking.value);
    }

    private void PrepareJumpNLand()
    {
        _animator.SetBool(_onGroundId, _isOnGround.value);
        _animator.SetBool(_preparingJumpId, _isPreparingJump.value);
    }
    #endregion
}

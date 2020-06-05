using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyWalkTowardTarget : MonoBehaviour
{
    #region Serialized
    [SerializeField] 
    private EnemyAttack _enemyAttack;
    [SerializeField]
    private float _speed = 1f;
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private int _aggroRange = 10;
    #endregion

    #region Private
    private Rigidbody2D _rigidbody;
    private Transform _transform;
    private Animator _animator;
    private int _isWalkingId;
    #endregion

    #region Unity Lifecycle
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();

        _animator = GetComponent<Animator>();
        _isWalkingId = Animator.StringToHash("isWalking");
    }

    private void FixedUpdate()
    {
        if (_target == null) return;
        // if target isn't within range, we don't move
        if (!WithinAggroRangeX())
        {
            _animator.SetBool(_isWalkingId, false);
            return;
        }
        // we move toward the target because it's within our range
        else
        {
            // if we're within attack range, we stop to attack
            if (Mathf.Abs(_target.position.x - transform.position.x) < _enemyAttack.m_attackRange)
            {
                _animator.SetBool(_isWalkingId, false);
                _enemyAttack.m_attackTriggered = true;
                return;
            }
            // else, we move toward target
            inAir();
            Walk();
        }
    }
    #endregion

    #region MyFunctions
    private void Walk()
    {
        Vector3 direction = _target.position - transform.position;
        _rigidbody.velocity = new Vector2(direction.x * _speed * Time.deltaTime, 0);
        if (_rigidbody.velocity.x > 0) _transform.localScale = new Vector3(-Mathf.Abs(_transform.localScale.x), _transform.localScale.y, _transform.localScale.z);
        else if (_rigidbody.velocity.x < 0) _transform.localScale = new Vector3(Mathf.Abs(_transform.localScale.x), _transform.localScale.y, _transform.localScale.z);
        _animator.SetBool(_isWalkingId, true);
    }

    private bool WithinAggroRangeX()
    {
        return Mathf.Abs(_target.position.x - _transform.position.x) <= _aggroRange ? true : false;
    }

    private void inAir()
    {
        if (Mathf.Abs(_rigidbody.velocity.y) > 0) _rigidbody.gravityScale = 100;
        else _rigidbody.gravityScale = 1;
    }
    #endregion
}

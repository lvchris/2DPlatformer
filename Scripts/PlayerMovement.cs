using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Serialized
#pragma warning disable CS0649
    [SerializeField, Range(5, 20)]
    private float _speed = 10f;
    [SerializeField, Range(5, 20)]
    private float _maxHorizontalSpeed = 10f;

    [SerializeField]
    private float _jumpPower = 5f;
    [SerializeField, Range(1, 4)]
    private int _maxJump = 1;

    [SerializeField, Min(0)]
    private float _cdAttack = 1f;

    [SerializeField]
    private LayerMask _groundLayer;

    [SerializeField]
    private bool _activateKeyboard = true;
    [SerializeField]
    private bool _activateGamepad = true;

    [SerializeField]
    private BoolVariable _isAttacking;
    [SerializeField]
    private BoolVariable _isOnGround;
    [SerializeField]
    private BoolVariable _isPreparingJump;

    [SerializeField]
    private GameEvent _jumpingSound;
#pragma warning restore CS0649
    #endregion

    #region Private
    private Rigidbody2D _rigidbody;
    private Transform _transform;
    private bool _jumpTriggered;
    private float _directionX;
    private float _initialCDAttack;
    private int _actualNumberOfJump;
    #endregion

    #region Unity Lifecycle
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
        _initialCDAttack = _cdAttack;
        _isAttacking.value = false;
        _actualNumberOfJump = 0;
    }

    private void Update()
    {
        _isAttacking.value = false;
        _isPreparingJump.value = false;
        IsGrounded();
        if (_activateKeyboard) KeyboardMovement();
        if(_activateGamepad) GamepadMovement();
        if (_cdAttack > 0) _cdAttack -= Time.deltaTime;
        
    }
    #endregion

    #region Gamepad
    private void GamepadMovement()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            if (_rigidbody.velocity.x < _maxHorizontalSpeed && Input.GetAxisRaw("Horizontal") > 0 ||
                _rigidbody.velocity.x > -_maxHorizontalSpeed && Input.GetAxisRaw("Horizontal") < 0)
                _directionX = Input.GetAxisRaw("Horizontal");
                
            if (_rigidbody.velocity.x < 0) _transform.localScale = new Vector3(-Mathf.Abs(_transform.localScale.x), _transform.localScale.y, _transform.localScale.z);
            else if (_rigidbody.velocity.x > 0) _transform.localScale = new Vector3(Mathf.Abs(_transform.localScale.x), _transform.localScale.y, _transform.localScale.z);
        }
        if (Input.GetButtonDown("Jump"))
        {
            _jumpTriggered = true;
        }
        if (Input.GetButtonDown("Fire1") && _cdAttack <= 0)
        {
            _isAttacking.value = true;
            _cdAttack = _initialCDAttack;
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity += new Vector2(_directionX *_speed * Time.deltaTime, 0);
        if (_jumpTriggered)
        {
            Jump();
            _jumpTriggered = false;
        }
    }
    #endregion

    #region Keyboard
    private void KeyboardMovement()
    {
        if (Input.GetKey(KeyCode.D))
        {
            if(_rigidbody.velocity.x < _maxHorizontalSpeed) _rigidbody.velocity += Vector2.right * _speed * Time.deltaTime;
            if (_transform.localScale.x < 0) _transform.localScale = new Vector3(-_transform.localScale.x, _transform.localScale.y, _transform.localScale.z);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            if (_rigidbody.velocity.x > -_maxHorizontalSpeed) _rigidbody.velocity += Vector2.left * _speed * Time.deltaTime;
            if (_transform.localScale.x > 0) _transform.localScale = new Vector3(-_transform.localScale.x, _transform.localScale.y, _transform.localScale.z);
        }
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        if (Input.GetKey(KeyCode.E) && _cdAttack <= 0)
        {
            _isAttacking.value = true;
            _cdAttack = _initialCDAttack;
        }
    }
    #endregion

    #region MyFunctions
    private void Jump()
    {
        if (_isOnGround.value == true && _rigidbody.velocity.y == 0)
        {
            _isPreparingJump.value = true;
            _actualNumberOfJump++;
            _jumpingSound.Raise();
            StartCoroutine(PrepareJump());
        }
        else if (_isOnGround.value == false && _actualNumberOfJump < _maxJump)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpPower);
            _actualNumberOfJump++;
        }

    }

    private void IsGrounded()
    {
        Vector2 position = _transform.position;
        Vector2 direction = Vector2.down;
        float distance = 0.2f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, _groundLayer);
        if (hit.collider != null)
        {
            _isOnGround.value = true;
            _actualNumberOfJump = 0;
            if (hit.collider.CompareTag("Platform")) _transform.SetParent(hit.collider.GetComponent<Transform>());
        }
        else
        {
            _isOnGround.value = false;
            if (_transform.parent != null) _transform.parent = null;
        }
    }
    #endregion

    #region Coroutine
    IEnumerator PrepareJump()
    {
        yield return new WaitForSeconds(0.3f);
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpPower);
    }
    #endregion
}

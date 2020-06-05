using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DestructiblePillar : MonoBehaviour
{
    #region Serialized
#pragma warning disable CS0649
    [SerializeField]
    private Sprite[] _sprite;
    [SerializeField]
    private int _columnHP;
    [SerializeField]
    private GameEvent _woodHitSound;
#pragma warning restore CS0649
    #endregion

    #region Private
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;
    private Animator _animator;
    private int _isHitId;
    #endregion

    #region Unity Lifecycle
    private void Awake()
    {
        if (_columnHP == 0) _columnHP = _sprite.Length;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _isHitId = Animator.StringToHash("isHit");
    }
    #endregion

    #region Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon") && _columnHP > 0)
        {
            _animator.SetTrigger(_isHitId);
            _spriteRenderer.sprite = _sprite[--_columnHP];
            _woodHitSound.Raise();
        }
        if (_columnHP == 0)
        {
            _spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f);
            _boxCollider.enabled = false;
        }
    }
    #endregion
}

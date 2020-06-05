using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemyHealth : MonoBehaviour
{
    #region Serialized
    [SerializeField]
    private int _health = 3;
    [SerializeField]
    private float _iFrame = 1f;
    #endregion

    #region Private
    private float _initialIFrame;
    #endregion

    #region Unity Lifecycle
    private void Awake()
    {
        _initialIFrame = _iFrame;
    }

    private void Update()
    {
        _iFrame -= Time.deltaTime;
    }
    #endregion

    #region Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( _iFrame > 0 ) return;
        _health -= 1;
        _iFrame = _initialIFrame;
        if (_health == 0) Destroy(gameObject);
    }
    #endregion
}

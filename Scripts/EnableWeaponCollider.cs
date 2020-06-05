using UnityEngine;

public class EnableWeaponCollider : MonoBehaviour
{
    #region Serialized
    [SerializeField]
    private BoolVariable _isAttacking;
    #endregion

    #region Private
    private BoxCollider2D _boxCollider2D;
    #endregion

    #region Unity Lifecycle
    private void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }
    #endregion

    #region MyFunctions
    public void EnableAxe()
    {
        _boxCollider2D.enabled = true;
    }
    public void DisableAxe()
    {
        _boxCollider2D.enabled = false;
    }
    #endregion
}

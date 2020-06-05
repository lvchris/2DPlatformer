using UnityEngine;

public class ManageWeapon : MonoBehaviour
{
    #region Serialized
#pragma warning disable CS0649
    [SerializeField]
    private EnableWeaponCollider _axeFunc;
#pragma warning restore CS0649
    #endregion

    #region MyFunctions
    private void EnableAxe()
    {
        _axeFunc.EnableAxe();
    }
    private void DisableAxe()
    {
        _axeFunc.DisableAxe();
    }
    #endregion
}

using UnityEngine;

public class ManageSound : MonoBehaviour
{
    #region Serialized
#pragma warning disable CS0649
    [SerializeField]
    private GameEvent _footstepSound;
#pragma warning restore CS0649
    #endregion

    #region MyFunctions
    private void RunnningSound()
    {
        _footstepSound.Raise();
    }
    #endregion
}

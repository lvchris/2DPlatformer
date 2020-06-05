using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class AlePickedUp : MonoBehaviour
{
    #region Serialized
#pragma warning disable CS0649
    [SerializeField]
    private IntVariable _aleCount;
    [SerializeField]
    private GameEvent _drinkingSound;
#pragma warning restore CS0649
    #endregion

    #region Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _aleCount.value += 1;
        _drinkingSound.Raise();
        Destroy(gameObject);
    }
    #endregion
}

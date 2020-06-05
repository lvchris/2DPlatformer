using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AleCounter : MonoBehaviour
{
    #region Serialized
#pragma warning disable CS0649
    [SerializeField]
    private TextMeshProUGUI _text;
    [SerializeField]
    private Image _image;
    [SerializeField]
    private IntVariable _aleCount;
#pragma warning restore CS0649
    #endregion

    #region Unity Lifecycle
    private void Start()
    {
        _aleCount.value = 0;
    }

    private void Update()
    {
        _text.text = _aleCount.value.ToString();
    }
    #endregion
}

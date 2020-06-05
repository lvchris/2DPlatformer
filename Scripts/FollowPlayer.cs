using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    #region Serialized
#pragma warning disable CS0649
    [SerializeField]
    private Transform _endMarker;
    [SerializeField]
    private float _speed = 1.0f;
    [SerializeField]
    private float _offsetY = 3f;
#pragma warning restore CS0649
    #endregion

    #region Private
    private Transform _transform;
    private float _initialCameraZ;
    private float _startTime;
    private float _journeyLength;
    #endregion

    #region Unity Lifecycle
    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _initialCameraZ = _transform.position.z;
    }

    private void Start()
    {
        _startTime = Time.time;
        _journeyLength = Vector3.Distance(_transform.position, (_endMarker.position + (Vector3.forward * _initialCameraZ) + (Vector3.up * _offsetY)));
    }

    private void Update()
    {
        float distCovered = (Time.time - _startTime) * _speed;
        float fractionOfJourney = distCovered / _journeyLength;
        Vector3 newPosition = Vector3.Lerp(_transform.position, (_endMarker.position + (Vector3.forward * _initialCameraZ) + (Vector3.up * _offsetY)), fractionOfJourney);
        if (!float.IsNaN(newPosition.x) && !float.IsNaN(newPosition.y))
        {
            _transform.position = newPosition;
        }
    }
    #endregion
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    #region Serialized
#pragma warning disable CS0649
    [SerializeField]
    private Transform _transformStart;
    [SerializeField]
    private Transform _transformEnd;
    [SerializeField]
    private float _speed = 0.2f;
    [SerializeField]
    private float _waitingTime = 3f;
#pragma warning restore CS0649
    #endregion

    #region Private
    private Transform _transform;
    private float _initWaitingTime;
    private bool _moveToStart = false;
    private bool _moveToEnd = true;
    private float fraction = 0f;
    #endregion

    #region Unity Lifecycle
    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _initWaitingTime = _waitingTime;
    }

    private void Update()
    {
        if (_waitingTime > 0) _waitingTime -= Time.deltaTime;
        else
        {
            if (_moveToEnd)
            {
                MoveTowards(_transformEnd);
                if (_transform.position == _transformEnd.position)
                {
                    _moveToStart = true;
                    _moveToEnd = false;
                    _waitingTime = _initWaitingTime;
                    fraction = 0;
                }
                
            }
            else if (_moveToStart)
            {
                MoveTowards(_transformStart);
                if (_transform.position == _transformStart.position)
                {
                    _moveToEnd = true;
                    _moveToStart = false;
                    _waitingTime = _initWaitingTime;
                    fraction = 0;
                }
            }
        }
    }
    #endregion

    #region MyFunctions
    private void MoveTowards(Transform _destTransform)
    {
        if (fraction < 1) {
            fraction += _speed * Time.deltaTime;
            _transform.position = Vector3.Lerp(_transform.position, _destTransform.position, fraction);
        }
    }
    #endregion
}

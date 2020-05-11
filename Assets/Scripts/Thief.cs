using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : MonoBehaviour
{
    // Все ниже только для теста, чтобы вор сам двигался. По умолчанию мне ничего из этого не нужно
    [SerializeField] private Transform _targetPosition;
    [SerializeField] private float _speed;
    [SerializeField] private bool _isMove = false;

    public void SetTargetPosition(Transform target)
    {
        _targetPosition = target;
        _speed = 1f;
    }

    private void Update()
    {
        if (_isMove)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, _targetPosition.localPosition, _speed * Time.deltaTime);
            if (transform.localPosition == _targetPosition.localPosition)
            {
                _isMove = false;
            }
        }
    }
    // Вот прям до сюда
}

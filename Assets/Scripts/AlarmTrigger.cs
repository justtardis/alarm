using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlarmTrigger : MonoBehaviour
{
    [SerializeField] private DisplayUI _displayUI;
    [SerializeField] private bool _isPenetrate = false;
    [SerializeField] private AudioClip _alarmSound;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _maxTime = 3f;
    [SerializeField] private bool _isWorkAlarm = false;
    [SerializeField] private float _currentTime = 0f;
    [SerializeField] private float _duration = 0f;


    private void Start()
    {
        _audioSource.clip = _alarmSound;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Thief thief))
        {
            _isPenetrate = true;
            _displayUI.SetNewText(_isPenetrate);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Thief thief))
        {
            _isPenetrate = false;
            _displayUI.SetNewText(_isPenetrate);
        }
    }

    private void AlarmVolumeChanger(float partTime)
    {
        _currentTime += partTime;
        _duration = _currentTime / _maxTime;
        _audioSource.volume = _duration;
    }

    // Чужой в доме - запуск аларм
    // Чужой был дома, но вышел - отключение сигнализации
    // Чужой вне дома - аларм отключена

    private void Update()
    {
        if (_isPenetrate)
        {
            if (!_isWorkAlarm)
            {
                _isWorkAlarm = true;
                _audioSource.Play();
            }
            else
            {
                if (_currentTime >= _maxTime)
                {
                    _currentTime = _maxTime;
                }
                else
                {
                    AlarmVolumeChanger(Time.deltaTime);
                }
            }
        }
        else
        {
            if (_isWorkAlarm)
            {
                if (_currentTime <= 0f)
                {
                    _currentTime = 0f;
                    _isWorkAlarm = false;
                    _audioSource.Stop();
                }
                else
                {
                    AlarmVolumeChanger(-Time.deltaTime);
                }
            }
        }
    }
}

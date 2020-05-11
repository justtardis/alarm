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
    [SerializeField] private bool isWorkAlarm = false;
    [SerializeField] private float _currentTime = 0f;


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

    private void AlarmPlaySound(bool isAlarm, float partTime)
    {
        _audioSource.volume += !isAlarm ? partTime : -partTime;
    }

    public void AP()
    {
        _audioSource.PlayOneShot(_alarmSound);
    }

    private void Update()
    {
        float duration = 0f;
        if (_isPenetrate)
        {
            if (!isWorkAlarm)
            {
                if (_currentTime >= _maxTime)
                {
                    _currentTime = _maxTime;
                    isWorkAlarm = true;
                }
                else
                {
                    _currentTime += Time.deltaTime;
                    duration = _currentTime / _maxTime;
                    AlarmPlaySound(isWorkAlarm, duration);
                }
            }
            _audioSource.Play();
        }
        else
        {
            if (isWorkAlarm)
            {
                if (_currentTime > 0f)
                {
                    _currentTime -= Time.deltaTime;
                    duration = _currentTime / _maxTime;
                    AlarmPlaySound(isWorkAlarm, duration);
                }
                else
                {
                    _currentTime = 0f;
                    isWorkAlarm = false;
                }
                _audioSource.Play();
            }
            else
            {
                _audioSource.Stop();
            }
        }
    }
}

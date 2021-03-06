﻿using System.Collections;
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
    private Coroutine runCoroutine;


    private void Start()
    {
        _audioSource.clip = _alarmSound;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Thief thief))
        {
            OnCorutineStarter(Armed());
            _displayUI.SetNewText(_isPenetrate = true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Thief thief))
        {
            OnCorutineStarter(Disarmed());
            _displayUI.SetNewText(_isPenetrate = false);
        }
    }

    private void OnCorutineStarter(IEnumerator startCorutine)
    {
        if (runCoroutine != null)
        {
            StopCoroutine(runCoroutine);
        }
        runCoroutine = StartCoroutine(startCorutine);
    }

    private void ChangeVolume(float partTime)
    {
        _currentTime += partTime;
        _duration = _currentTime / _maxTime;
        _audioSource.volume = _duration;
    }

    private IEnumerator Armed()
    {
        bool isFinishStart = false;
        _audioSource.Play();
        while (!isFinishStart)
        {
            if (_currentTime >= _maxTime)
            {
                _currentTime = _maxTime;
                isFinishStart = true;
            }
            else
            {
                ChangeVolume(Time.deltaTime);
            }
            yield return null;
        }
    }

    private IEnumerator Disarmed()
    {
        bool isFinishStop = true;
        while (isFinishStop)
        {
            if (_currentTime > 0f)
            {
                ChangeVolume(-Time.deltaTime);
            }
            else
            {
                _currentTime = 0f;
                _audioSource.Stop();
                isFinishStop = false;
            }
            yield return null;
        }
    }
}

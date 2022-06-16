using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    #region Variables

    private Action _completeCallback;
    private bool _isStarted;
    private float _time;

    #endregion


    #region Unity lifecycle

    private void Update()
    {
        if (!_isStarted)
            return;
        _time -= Time.deltaTime;

        if (_time < 0)
        {
            _isStarted = false;
            _completeCallback?.Invoke();
        }
    }

    #endregion


    #region Public metods

    public void StartTimer(float time, Action completeCallback)
    {
        _completeCallback = completeCallback;
        _isStarted = true;
        _time = time;
    }

    #endregion
}
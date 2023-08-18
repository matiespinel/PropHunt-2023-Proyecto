using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoundManagerScript : MonoBehaviour
{
    #region observador
    public static event Action OnAnySound;
    #endregion observador
    void Update()
    {
        OnAnySound?.Invoke();
    }
}

using System;
using UnityEngine;

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

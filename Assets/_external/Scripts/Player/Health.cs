using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private int lives;

    public event Action OnDead;
    public event Action OnHurt;

    public void TakeDamage()
    {
        lives--;
        HandleDamageTaken();

    }

    public void HandleDamageTaken()
    {
        if (lives <= 0)
        {
            OnDead?.Invoke();
        }
        else
        {
            OnHurt?.Invoke();
        }
    }

    public int GetLives()
    {
        return lives;
    }
  
}

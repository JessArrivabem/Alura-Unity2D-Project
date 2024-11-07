using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class BossFightTrigger : MonoBehaviour
{
    public event Action OnPlayerEnterBossFight;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            OnPlayerEnterBossFight?.Invoke();
        }
    }
}

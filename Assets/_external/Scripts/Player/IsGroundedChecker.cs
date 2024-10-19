using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGroundedChecker : MonoBehaviour
{
    [SerializeField] private Transform checkerPosition;
    [SerializeField] private Vector2 checkerSize;
    [SerializeField] private LayerMask groundLayer;

    public bool isGrounded()
    {
        return Physics2D.OverlapBox // cria uma caixa invisível e verifica quais colliders estão naquela caixa
            (checkerPosition.position, checkerSize, 0f, groundLayer);
    }
    private void OnDrawGizmos()
    {
        if(checkerPosition == null) return;
        if (isGrounded())
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color= Color.green;
        }
        Gizmos.DrawWireCube(checkerPosition.position, checkerSize);
    }
}

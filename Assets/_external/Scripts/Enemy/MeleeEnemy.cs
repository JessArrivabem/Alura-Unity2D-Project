using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : BaseEnemy
{
    [SerializeField] private Transform detectPosition;
    [SerializeField] private Vector2 detectBoxSize;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float attackCooldown;

    private float cooldownTimer;

    [Header("Audio Properties")]
    [SerializeField] private AudioClip[] audioClips;

    protected override void Awake()
    {
        base.Awake();
        base.enemyHealth.OnHurt += PlayHurtAudio;
        base.enemyHealth.OnDead += PlayDeadAudio;
    }

    protected override void Update()
    {
        cooldownTimer += Time.deltaTime; // verifies how many frames showed on the screen in seconds
        VerifyCanAttack();

    }

    private void VerifyCanAttack()
    {
        if (cooldownTimer < attackCooldown) return;
        if (PlayerInSight())
        {
            enemyAnimator.SetTrigger("attack");
            AttackPlayer();
        }
    }
    private void AttackPlayer()
    {
        audioSource.clip = audioClips[0];
        cooldownTimer = 0;
        if (CheckPlayerInDetectArea().TryGetComponent(out Health playerHealth))
        {
            print("Making player take damage!");
            playerHealth.TakeDamage();
        }
    }

    private Collider2D CheckPlayerInDetectArea()
    {
        return Physics2D.OverlapBox(detectPosition.position, detectBoxSize, 0f, playerLayer);
    }

    private bool PlayerInSight()
    {
        Collider2D playerCollider = CheckPlayerInDetectArea();
        return playerCollider != null;
    }

    private void OnDrawGizmos()
    {
        if (detectPosition == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(detectPosition.position, detectBoxSize);
    }

    private void PlayHurtAudio()
    {
        audioSource.clip = audioClips[1];
        audioSource.Play();
    }

    private void PlayDeadAudio()
    {
        audioSource.clip = audioClips[2];
        audioSource.Play();
    }
}

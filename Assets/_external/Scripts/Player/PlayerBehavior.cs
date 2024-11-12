using UnityEngine.InputSystem;
using UnityEngine;
using System.Runtime.CompilerServices;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float jumpForce = 3;

    [SerializeField] private ParticleSystem hitParticle;

    [Header("Atack Properties")]
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private Transform attackPosition;
    [SerializeField] private LayerMask attackLayer;

    private float moveDirection;

    private Rigidbody2D rigibody;
    private IsGroundedChecker isGroundedChecker;
    private Health health;

    private void Awake()
    {
        rigibody = GetComponent<Rigidbody2D>();
        isGroundedChecker = GetComponent<IsGroundedChecker>();
        health = GetComponent<Health>();

        health.OnDead += HandlePlayerDeath;
        health.OnHurt += HandleHurt;

    }
    private void Start()
    {
        GameManager.Instance.InputManager.OnJump += HandleJump;
    }

    private void FixedUpdate()
    {
        MovePlayer();
        FlipSriteAccordingToDirection();
    }
    private void MovePlayer()
    {
         moveDirection = GameManager.Instance.InputManager.Movement;
        Vector2 directionToMove = new Vector2 (moveDirection * moveSpeed, rigibody.velocity.y);
         rigibody.velocity = directionToMove;
    }

    private void FlipSriteAccordingToDirection()
    {
        if (moveDirection < 0)
        {
          transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (moveDirection > 0)
        {
          transform.localScale = Vector3.one;

        }
    }
    private void HandleJump()
    {
        if(isGroundedChecker.isGrounded() == false) return; // se não estiver no chão ele vai retornar
        rigibody.velocity += Vector2.up * jumpForce;
        GameManager.Instance.AudioManager.PlaySFX(SFX.PlayerJump);
    }

    private void HandleHurt()
    {
        UpdateLives(health.GetLives());
        PlayHitParticle();
        GameManager.Instance.AudioManager.PlaySFX(SFX.PlayerHurt);
    }

    private void PlayWalkSound()
    {
        GameManager.Instance.AudioManager.PlaySFX(SFX.PlayerWalk);
    }

    private void HandlePlayerDeath()
    {
        GetComponent<Collider2D>().enabled = false;
        rigibody.constraints = RigidbodyConstraints2D.FreezeAll; // avoid of player falling in the world
        PlayHitParticle();
        GameManager.Instance.AudioManager.PlaySFX(SFX.PlayerDeath);
        GameManager.Instance.InputManager.DisablePlayerInput();

        UpdateLives(health.GetLives());
    }

    private void UpdateLives(int amount)
    {
        GameManager.Instance.UpdateLives(amount);
    }

    private void Attack()
    {
        GameManager.Instance.AudioManager.PlaySFX(SFX.PlayerAttack);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, attackLayer);
        print("Making enemy take damage");
        print(hitEnemies.Length);

        foreach (Collider2D hitEnemy in hitEnemies) 
        {
            print("Checking enemy");
            if (hitEnemy.TryGetComponent(out Health enemyHealth))
            {
                print("Getting damage");
                enemyHealth.TakeDamage();
            }
        }

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "SceneTransition")
        {
            ScreenManager.Instance.sceneToMoveTo();
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }
    private void PlayHitParticle()
    {
        ParticleSystem instatiateParticle = Instantiate(hitParticle, transform.position, transform.rotation);
        instatiateParticle.Play();
    }

}

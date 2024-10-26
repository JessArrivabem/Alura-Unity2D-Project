using UnityEngine.InputSystem;
using UnityEngine;
using System.Runtime.CompilerServices;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpForce = 3;

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

    private void Update()
    {
        MovePlayer();
        FlipSrite();
    }
    private void MovePlayer()
    {
         moveDirection = GameManager.Instance.InputManager.Movement;
         transform.Translate(moveDirection * Time.deltaTime * speed, 0, 0);
    }

    private void FlipSrite()
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
        GameManager.Instance.AudioManager.PlaySFX(SFX.PlayerJump);
        rigibody.velocity += Vector2.up * jumpForce;
    }

    private void HandleHurt()
    {
        UpdateLives(health.GetLives());
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
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }

}

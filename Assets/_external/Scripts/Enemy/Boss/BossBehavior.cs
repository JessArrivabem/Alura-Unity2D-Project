using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    private Rigidbody2D rigidbodyBoss;
    private Transform playerPosition;
    private Health health;
    private Animator animator;

    [SerializeField] private float moveSpeed = 3f;

    [SerializeField] private ParticleSystem hitParticle;

    [Header("Attack properties")]
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float attackSize = 1f;
    [SerializeField] private Vector3 attackOffset;
    [SerializeField] private LayerMask attackMask;

    private Vector3 attackPosition;

    private bool canAttack = false;
    private bool isFlipped = true;

    private void Awake()
    {
        rigidbodyBoss = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();

        health.OnHurt += PlayHurtAnim;
        health.OnDead += HandleDeath;
    }

    private void Start()
    {
        playerPosition = GameManager.Instance.GetPlayer().transform;
    }

    private void PlayHurtAnim()
    {
        animator.SetTrigger("hurt");
        PlayHitParticle();
    }

    private void HandleDeath()
    {
        animator.SetTrigger("dead");
        PlayHitParticle();

    }

    public void FollowPlayer()
   {
        Vector2 target = new Vector2(playerPosition.position.x, transform.position.y); // grabs player position
        Vector2 newPos = Vector2.MoveTowards(rigidbodyBoss.position, target, moveSpeed * Time.fixedDeltaTime); // where the boss is going to move
        rigidbodyBoss.MovePosition(newPos);
        LookAtPlayer();
        CheckPositionFromPlayer();
    }

    private void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > playerPosition.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < playerPosition.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    private void CheckPositionFromPlayer()
    {
        float distanceFromPlayer = Vector2.Distance(playerPosition.position, transform.position);
        if (distanceFromPlayer <= attackRange)
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }
    }

    public void Attack()
    {
        attackPosition = transform.position;
        attackPosition += transform.right * attackOffset.x;
        attackPosition += transform.up * attackOffset.y;

        Collider2D collisionInfo = Physics2D.OverlapCircle(attackPosition, attackSize, attackMask);
        if(collisionInfo != null)
        {
            collisionInfo.GetComponent<Health>().TakeDamage();
        }
    }

    public void StartChasing()
    {
        animator.SetBool("canChase", true);
    }

    public bool GetCanAttack()
    {
        return canAttack;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPosition, attackSize);
    }

    private void PlayHitParticle()
    {
        ParticleSystem instatiateParticle = Instantiate(hitParticle, transform.position, transform.rotation);
        instatiateParticle.Play();
    }
}

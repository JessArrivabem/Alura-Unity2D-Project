using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public abstract class BaseEnemy : MonoBehaviour
{
    protected Animator animator;

    private Animator enemyAnimator;
    public Health enemyHealth;

    protected AudioSource audioSource;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();

        animator = GetComponent<Animator>();
        enemyHealth = GetComponent<Health>();

        enemyHealth.OnHurt += EnemyHurtAnim;
        enemyHealth.OnDead += HandleEnemyDeath;

        audioSource = GetComponent<AudioSource>();
    }
    protected abstract void Update();

    private void EnemyHurtAnim() => animator.SetTrigger("hurt");
    
    private void HandleEnemyDeath()
    {
        animator.SetTrigger("dead");
        this.GetComponent<Collider2D>().enabled = false;
        StartCoroutine(DestroyEnemy(1));
    }

    private IEnumerator DestroyEnemy(int time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }

}

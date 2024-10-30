using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public abstract class BaseEnemy : MonoBehaviour
{
    protected Animator enemyAnimator;
    public Health enemyHealth;
    protected AudioSource audioSource;

    [SerializeField] private ParticleSystem hitParticle;

    protected virtual void Awake()
    {
        enemyAnimator = GetComponent<Animator>();

        enemyAnimator = GetComponent<Animator>();
        enemyHealth = GetComponent<Health>();

        enemyHealth.OnHurt += HandleEnemyHurt;
        enemyHealth.OnDead += HandleEnemyDeath;

        audioSource = GetComponent<AudioSource>();
    }
    protected abstract void Update();

    private void HandleEnemyHurt()
    {
        enemyAnimator.SetTrigger("hurt");
        PlayHitParticle();
    } 
    
    private void HandleEnemyDeath()
    {
        enemyAnimator.SetTrigger("dead");
        this.GetComponent<Collider2D>().enabled = false;
        PlayHitParticle();
        StartCoroutine(DestroyEnemy(1));
    }

    private IEnumerator DestroyEnemy(int time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }

    private void PlayHitParticle()
    {
        ParticleSystem instatiateParticle = Instantiate(hitParticle, transform.position, transform.rotation);
        instatiateParticle.Play();
    }

}

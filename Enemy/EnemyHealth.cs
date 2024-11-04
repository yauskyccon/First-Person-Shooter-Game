using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public Animator animator;
    public GameObject meatPrefab;

    public float attackRange = 2.5f;
    public float minDamage = 15f; 
    public float maxDamage = 25f; 

    private AttributesManager playerAttributes;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null) playerAttributes = player.GetComponent<AttributesManager>();
    }

    private void Update()
    {
        if (IsPlayerInRange() && playerAttributes != null && playerAttributes.playerHP > 0 && health > 0)
        {
            float damageDeal = Random.Range(minDamage, maxDamage);
            playerAttributes.DamageTakenByPlayer(damageDeal);
        }
    }

    private bool IsPlayerInRange()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            return distance <= attackRange;
        }
        return false;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            AudioManager.instance.Play("Enemy Death");
            animator.SetTrigger("die");
            GetComponent<Collider>().enabled = false;
            Instantiate(meatPrefab, transform.position, Quaternion.identity);
            StartCoroutine(HandleDeath());
        }
        else
        {
            AudioManager.instance.Play("Enemy Damage");
            animator.SetTrigger("damage");
        }
    }

    private IEnumerator HandleDeath()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
}

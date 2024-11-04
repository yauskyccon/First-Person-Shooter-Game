using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributesManager : MonoBehaviour
{
    public Slider healthSlider;
    public float maxHealth = 100f;

    public float playerHP;
    public Animator animator;
    private bool canTakeDamage = true;

    public WonLoseUIManager wonLoseUIManager;
    public ScoreManager scoreManager;

    void Start()
    {
        playerHP = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = playerHP;
    }
    private void Update()
    {
        healthSlider.value = playerHP;
    }

    public void DamageTakenByPlayer(float damageDeal)
    {
        if (canTakeDamage)
        {
            playerHP -= damageDeal;
            if (playerHP <= 0)
            {
                Debug.Log("Game Over");
                AudioManager.instance.Play("Player Death");
                animator.SetTrigger("Death");

                AudioManager.instance.Stop("Background");
                AudioManager.instance.Play("Lose");
                wonLoseUIManager.PlayerLose();

            }
            else
            {
                Debug.Log("Injured");
                AudioManager.instance.Play("Player Injured");
            }

            StartCoroutine(ResetDamageTimer());
        } 
    }

    private IEnumerator ResetDamageTimer()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(2f);
        canTakeDamage = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HealthPowerup"))
        {
            playerHP += 10;
            playerHP = Mathf.Clamp(playerHP, 0, maxHealth);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("SafeHouse"))
        {
            int score = ScoreManager.scoreCount;

            AudioManager.instance.Stop("Background");
            AudioManager.instance.Play("Win");
            wonLoseUIManager.PlayerWin(score);
        }
    }
}

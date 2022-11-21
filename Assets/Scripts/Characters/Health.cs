using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private float immunityDuration = 5f;
    [Range(0f, 1f)][SerializeField] private float immuneAlpha = 0.3f;
    [SerializeField] private int coinRewardOnDie = 10;

    private int currentHealth;
    private bool immuneState = false;
    private float immuneTimer;
    private float immuneDuration = 2.0f;
    private SpriteRenderer rend;
    private Boolean immune;

    public static Action<GameObject> onTakeDamage;

    private void Start()
    {
        currentHealth = maxHealth;
        rend = GetComponent<SpriteRenderer>();
    }

    private void Update(){
        if (immuneTimer > Mathf.Epsilon){
            immuneTimer = Mathf.Max(0f, immuneTimer - Time.deltaTime);
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }

    public void SetImmune()
    {
        immune = true;
        var color = rend.color;
        color.a = immuneAlpha;
        rend.color = color;
        // Set immune to false after a duration
        Invoke(nameof(ResetImmune), immunityDuration);
    }

    private void ResetImmune()
    {
        immune = false;
        var color = rend.color;
        color.a = 1f;
        rend.color = color;
    }

    public void TakeDamage(int amount)
    {
        if(immuneState && immuneTimer > Mathf.Epsilon){
            amount = 0;
        } else {
            immuneState = false;
            immuneTimer = 0f;
        }

        currentHealth = Mathf.Max(0, currentHealth - amount);
        onTakeDamage?.Invoke(gameObject);

        if (currentHealth == 0)
        {
            if (GetComponent<PlayerMovement>())
            {
                // Player has died!
                Debug.Log("Lost!");
                Destroy(gameObject);
                FindObjectOfType<GameManager>().RestartLevel();
            }
            else if (GetComponent<KeyCrate>())
            {
                GetComponent<KeyCrate>().DeathSequence();
                Destroy(gameObject);
            }
            else
            {
                // Enemy has died
                FindObjectOfType<GameManager>().DestroyEnemy();
                FindObjectOfType<Inventory>().GainCoins(coinRewardOnDie);
                Destroy(gameObject.transform.parent.gameObject);
            }
        }
    }

    public void setImmuneState()
    {
        immuneState = true;
        immuneTimer = immuneDuration;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
}

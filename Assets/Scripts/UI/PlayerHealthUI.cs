using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private GameObject[] heartSprites;

    private Health playerHealth;
    private int maxHealth;

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerMovement>().GetComponent<Health>();
        maxHealth = playerHealth.GetMaxHealth();
    }

    void Update()
    {
        int diff = maxHealth - playerHealth.GetCurrentHealth();
        for (int i = 0; i < maxHealth; ++i)
        {
            if (i < diff)
            {
                heartSprites[i].SetActive(false);
            }
            else
            {
                heartSprites[i].SetActive(true);
            }
        }
    }
}

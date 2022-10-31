using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int startCoins = 20;
    [SerializeField] private GameObject shopMenu = null;

    private int enemiesLeft;
    private bool shopMenuOn = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            shopMenuOn = !shopMenuOn;
            shopMenu.SetActive(shopMenuOn);
            if (shopMenuOn)
            {
                // pause game
                Time.timeScale = 0f;
            }
            else
            {
                // resume game
                Time.timeScale = 1f;
            }
        }
    }

    public void RegisterEnemy()
    {
        enemiesLeft += 1;
    }

    public void DestroyEnemy()
    {
        enemiesLeft -= 1;
        if (enemiesLeft == 0)
        {
            // Player has won!
            Debug.Log("Won!");
        }
    }

    public int GetCoins()
    {
        return startCoins;
    }

    public void AddCoins(int amount)
    {
        startCoins += amount;
    }
}

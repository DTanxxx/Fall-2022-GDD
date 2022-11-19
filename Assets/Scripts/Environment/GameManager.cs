using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int coins = 20;
    [SerializeField] private GameObject shopMenu = null;

    private int enemiesLeft;
    public bool hasKey = false;
    private bool shopOn = false;
    public int minigunAmmo = 100;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            shopOn = !shopOn;
            shopMenu.SetActive(shopOn);

            if (shopOn)
            {
                Time.timeScale = 0f;
            }
            else
            {
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

    public void RecieveKey()
    {
        hasKey = true;
        Debug.Log("+1 Key");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddCoins(int amount)
    {
        coins += amount;
    }

    public int GetCoins()
    {
        return coins;
    }
}

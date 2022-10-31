using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int enemiesLeft;
    public bool hasKey = false;

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
}

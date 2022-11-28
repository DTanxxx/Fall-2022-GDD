using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] public Sprite openDoor;
    private bool open = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        open = FindObjectOfType<GameManager>().hasKey;
        if (open)
        {   
            // Change sprite
            gameObject.GetComponent<SpriteRenderer>().sprite = openDoor;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && open)
        {
            FindObjectOfType<GameManager>().NextLevel();
        }
    }
}

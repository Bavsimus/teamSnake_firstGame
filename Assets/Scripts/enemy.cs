using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public GameObject BeardedGuy;
    private Rigidbody2D enemyRb;
    private BoxCollider2D enemyBc;
    private SpriteRenderer enemySb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("attackCollider"))
        {
            enemyDie();
        }
    }

    public void enemyDie()
    {
        Destroy(BeardedGuy);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damageOnHit = 1;   // how many lives to remove
    public int scoreOnKill = 1;   // how many points per enemy

    void Update()
    {
        transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * 3f);
        if (transform.position.y < -6.5f)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Bullet hits enemy
        if (other.CompareTag("Bullet"))
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.AddScore(scoreOnKill);
            }

            Destroy(other.gameObject);   // destroy bullet
            Destroy(gameObject);         // destroy enemy
        }
        // Player hits enemy
        else if (other.CompareTag("Player"))
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.AddLife(-damageOnHit);  // lose life
            }

            Destroy(gameObject);         // remove the enemy after the hit
        }
    }
}

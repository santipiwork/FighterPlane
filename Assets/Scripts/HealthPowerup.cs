using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerup : MonoBehaviour
{
    public float lifeTime = 3f;
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.instance != null)
            {
                // If not full health, gain 1 life (max 3)
                if (GameManager.instance.lives < 3)
                {
                    GameManager.instance.AddLife(1);
                }
                else
                {
                    // At full health → gain score instead (nice bonus mechanic)
                    GameManager.instance.AddScore(1);
                }
            }

            Destroy(gameObject);
        }
    }
}

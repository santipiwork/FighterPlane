using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyB : MonoBehaviour
{
    public float speed = 2.5f;      // fall speed
    public float frequency = 3f;    // wiggle speed
    public float amplitude = 1.25f; // wiggle width

    private float startX;

    void Start()
    {
        startX = transform.position.x;
    }

    void Update()
    {
        float x = startX + Mathf.Sin(Time.time * frequency) * amplitude;
        float y = transform.position.y - speed * Time.deltaTime;
        transform.position = new Vector3(x, y, 0);

        if (transform.position.y < -6.5f)
        {
            Destroy(this.gameObject);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Bullet"))
            {
                if (GameManager.instance != null)
                {
                    GameManager.instance.AddScore(1);
                }

                Destroy(other.gameObject);
                Destroy(gameObject);
            }
            else if (other.CompareTag("Player"))
            {
                if (GameManager.instance != null)
                {
                    GameManager.instance.AddLife(-1);
                }

                Destroy(gameObject);
            }
        }
    }
}

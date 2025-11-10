using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //how to define a variable
    //1. access modifier: public or private
    //2. data type: int, float, bool, string
    //3. variable name: camelCase
    //4. value: optional

    private float playerSpeed;
    private float horizontalInput;
    private float verticalInput;

    private float horizontalScreenLimit = 10f;
    private float verticalScreenLimit = 3.5f;

    public GameObject bulletPrefab;

    void Start()
    {
        playerSpeed = 6f;
        //This function is called at the start of the game
        
    }

    void Update()
    {
        //This function is called every frame; 60 frames/second
        Movement();
        Shooting();

    }

    void Shooting()
    {
        //if the player presses the SPACE key, create a projectile
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * playerSpeed);

        Vector3 pos = transform.position;

        // Wrap horizontally (unchanged)
        if (pos.x > horizontalScreenLimit || pos.x <= -horizontalScreenLimit)
        {
            pos.x = pos.x * -1f;
        }

        // --- Clamp vertically to bottom half only ---
        // Bottom edge = -verticalScreenLimit; Middle line = 0
        pos.y = Mathf.Clamp(pos.y, -verticalScreenLimit, 0f);

        transform.position = pos;
    }

}

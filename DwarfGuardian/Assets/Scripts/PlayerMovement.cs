using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed at which the player moves
    public float MaxX = 15;

    Rigidbody2D rb; // Reference to the player's Rigidbody component

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody component attached to the player GameObject
    }

    void Update()
    {
        

        // Calculate movement direction, it had to go to position - 000
        
        Vector2 movement = - rb.position + new Vector2(0, 0);

        // Normalize movement vector to ensure consistent speed in all directions
        movement.Normalize();

        // Move the player
        rb.velocity = movement * moveSpeed;

        // // if the player is further then the limit MaxX, then set the player position to the - limit
        // if(transform.position.x >= MaxX - 1)
        // {
        //     transform.position = new Vector3(0, transform.position.y, transform.position.z);
        // }
        // else if(transform.position.x < 0)
        // {
        //     transform.position = new Vector3(MaxX, transform.position.y, transform.position.z);
        // }

        // if the movement is to the left, then flip the player sprite
        if (movement.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (movement.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

    }
}

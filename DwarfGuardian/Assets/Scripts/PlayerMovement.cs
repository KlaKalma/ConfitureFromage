using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed at which the player moves
    public float MaxX = 2;

    Rigidbody2D rb; // Reference to the player's Rigidbody component

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody component attached to the player GameObject
    }

    void Update()
    {
        var moveHorizontal = 5;
        var moveVertical = 0;

        // Calculate movement direction
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        // Normalize movement vector to ensure consistent speed in all directions
        movement.Normalize();

        // Move the player
        rb.velocity = movement * moveSpeed;

        // if the player is further then the limit MaxX, then set the player position to the - limit
        if(transform.position.x > MaxX)
        {
            transform.position = new Vector3(-MaxX, transform.position.y, transform.position.z);
        }
        else if(transform.position.x < -MaxX)
        {
            transform.position = new Vector3(MaxX, transform.position.y, transform.position.z);
        }
    }
}

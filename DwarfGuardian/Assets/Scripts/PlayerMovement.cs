using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed at which the player moves
    public float MaxX = 15;

    public float TargetMargin = 0.5f;

    private PlantManager plantManager;
    private GameObject GnomeTarget;

    Rigidbody2D rb; // Reference to the player's Rigidbody component

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody component attached to the player GameObject
        plantManager = FindObjectOfType<PlantManager>();
        GnomeTarget = GameObject.Find("GnomeTarget");
    }

    void Update()
    {
        

        // Calculate movement direction, it had to go to position - 000

        // calculate the target, its the closest plant to the player or the 
        Vector2 target = new Vector2(0, 0);
        float distance = Vector2.Distance(GnomeTarget.transform.position, transform.position);
        foreach (var plant in plantManager.plantsOnField)
        {
            float currentDistance = Vector2.Distance(plant.transform.position, transform.position);
            if (currentDistance < distance)
            {
                distance = currentDistance;
                target = plant.transform.position;
            }
        }
        
        Vector2 movement = - rb.position + target;

        // Normalize movement vector to ensure consistent speed in all directions
        movement.Normalize();

        // Move the player

        // if the player is not too close to the target, then move the player
        if (distance > TargetMargin){
            rb.velocity = movement * moveSpeed;
        }
        else{
            rb.velocity = new Vector2(0, 0);
        }

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

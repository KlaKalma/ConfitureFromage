using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gnome : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed at which the player moves
    public float MaxX = 15;

    public float TargetMargin = 0.5f;

    // dying image
    public Sprite deathImage;
    public float fadeDuration = 1f; // Duration of the fade in seconds
    public float upDistance = 2f; // Distance to move the player up when they die
    // public int health = 100; // Gnome's health

    public int MoneyOnDeath { get; set; } = 10; // Money given to the player when the gnome dies

    public bool isDead = false; // Is the gnome dead?

    private SpriteRenderer spriteRenderer; // The SpriteRenderer component

    private GameManager gameManager;

    private PlantManager plantManager;
    private GameObject GnomeTarget;

    Rigidbody2D rb; // Reference to the player's Rigidbody component

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody component attached to the player GameObject
        plantManager = FindObjectOfType<PlantManager>();
        gameManager = FindObjectOfType<GameManager>();
        GnomeTarget = GameObject.Find("GnomeTarget");

    }

    protected virtual void Update()
    {
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

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

        if (distance > TargetMargin && !isDead){
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

    public virtual void TakeDamage(bool Disapiring = false)
    {
        // Delete in the list of the player spawner
        FindObjectOfType<PlayerSpawner>().players.Remove(gameObject);


        // stop the animmation of the gnome
        GetComponent<Animator>().enabled = false;

        // Set the gnome as dead
        isDead = true;

        if (!Disapiring) {
            // Add some money
            gameManager.addMoney(MoneyOnDeath);

            // Change the sprite to the death image
            spriteRenderer.sprite = deathImage;
        }
        // Start the fade out coroutine
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        // Calculate the rate of fading
        float fadeRate = 1f / fadeDuration;

        // While the sprite is not fully transparent
        while (spriteRenderer.color.a > 0)
        {
            // Decrease the alpha value of the sprite
            float newAlpha = spriteRenderer.color.a - fadeRate * 2 / 3 * Time.deltaTime;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);

            // move the player up with a sin function so the hat goes left and right with sin and up constant
            transform.position += new Vector3(Mathf.Sin(Time.time * 8) * upDistance * Time.deltaTime / 3, upDistance * Time.deltaTime, 0);
            
            

            // Wait for the next frame
            yield return null;
        }

        // Destroy the gnome
        Destroy(gameObject);
    }
}

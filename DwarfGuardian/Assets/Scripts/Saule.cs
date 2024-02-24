using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saule : Plant
{
    public int price = 4;
    public float radius = 5;
    public float timeToAttack = 0.5f;
    private float timeOfLastAttack = 0;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        timeOfLastAttack = Time.time;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        // Debug.Log("Ronce update");
        // Check if enough time has passed since the last attack
        if (Time.time - timeOfLastAttack >= timeToAttack)
        {
            
            // If so, perform the attack and update the time of the last attack
            Attack();
            timeOfLastAttack = Time.time;
        }
    }

    void Attack()
    {
        // Your attack code here
        GameObject closestGnome = GetClosestGnome();
        // Debug.Log("Closest gnome: " + closestGnome);
        if (closestGnome != null)
        {
            float distance = Vector3.Distance(transform.position, closestGnome.transform.position);
            if (distance <= radius)
            {
                // Attack the closest gnome
                closestGnome.GetComponent<Gnome>().TakeDamage();
                // Debug.Log("Gnome attacked!");
            }
        }
    }
}

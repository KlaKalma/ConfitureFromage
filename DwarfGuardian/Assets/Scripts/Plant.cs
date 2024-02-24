using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IPrice
{
    int price { get; set; }
}
public class Plant : MonoBehaviour
{

    private PlayerSpawner playerSpawner;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        // get player spawner
        // Debug.Log();
        playerSpawner = GameObject.Find("Player Spawner").GetComponent<PlayerSpawner>();
        // Debug.Log(playerSpawner);
    }

    protected GameObject GetClosestGnome()
    {
        // Debug.Log("playerSpawner: " + playerSpawner.players.Count);
        GameObject closestGnome = null;
        float closestDistance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject gnome in playerSpawner.players)
        {
            // Debug.Log("Gnome: " + gnome);
            Vector3 diff = gnome.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < closestDistance)
            {
                closestGnome = gnome;
                closestDistance = curDistance;
            }
        }
        return closestGnome;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
}

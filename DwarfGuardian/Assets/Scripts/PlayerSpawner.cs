using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{

    // grid manager reference
    public GridManager gridManager;
    // List of gnomes
    public List<GameObject> gnomes = new List<GameObject>();

    // list of all the players not modifiable
    [System.NonSerialized]
    public List<GameObject> players = new List<GameObject>();

    private float _width, _height;


    // Start is called before the first frame update
    void Start()
    {
        // get the width and height inside of the grid manager
        _width = gridManager._width;
        _height = gridManager._height;

        // get a random number form 2 to 5
        int random = Random.Range(6, 20);

        for (int i = 0; i < random; i++)
        {
            SpawnPlayer();
        }
        

    }

    void SpawnPlayer()
    {
        // get a random number from 1 to 3
        // int randomSide = 1;
        int randomSide = Random.Range(1, 4);


        // 1 = left 2 = bottom 3 = right
        Vector3 spawnPosition = Vector3.zero;

        if (randomSide == 1) // left
        {
            spawnPosition = new Vector3(- _width / 2, Random.Range(-_height / 2, 0), 0);
        }
        else if (randomSide == 2) // bottom
        {
            spawnPosition = new Vector3(Random.Range(-_width / 2, _width / 2), -_height / 2, 0);
        }
        else if (randomSide == 3) // right
        {
            spawnPosition = new Vector3(_width / 2, Random.Range(-_height / 2, 0), 0);
        }


        GameObject gnomePrefab = gnomes[Random.Range(0, gnomes.Count)];


        // spawn the player
        GameObject newGnome = Instantiate(gnomePrefab, spawnPosition, Quaternion.identity);

        // make the gnome a child of the player spawner
        newGnome.transform.parent = transform;

        // add the player to the list
        players.Add(newGnome);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

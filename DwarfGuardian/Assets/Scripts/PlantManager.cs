using System.Collections;
using System.Collections.Generic;
// using System.Numerics;
using UnityEngine;

public class PlantManager : MonoBehaviour
{

    // list of all the availiable plant prefabs
    public List<GameObject> plantPrefabs = new List<GameObject>();

    // grid manager reference
    public GameManager gridManager;

    // list of all the plants not modifiable
    [System.NonSerialized]
    public List<GameObject> plantsOnField = new List<GameObject>();

    private float _width, _height;


    // Start is called before the first frame update
    void Start()
    {
        // get the width and height from the grid manager
        _width = gridManager._width;
        _height = gridManager._height;
        
        // spawn a single plant at the start of the game, in the bottom part of the playing field

        // spawn 3 plants at the start of the game
        for (int i = 0; i < 3; i++)
            SpawnPlant();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnPlant(){
        // spawn a plant randomly between all the plants in the list
        int randomPlant = Random.Range(0, plantPrefabs.Count);

        // position of the plant, it has to be an int
        Vector3 spawnPosition = new Vector3(Random.Range(-_width / 2, _width / 2), Random.Range(-_height / 2, _height / 2), 0);
        spawnPosition = new Vector3(Mathf.Round(spawnPosition.x), Mathf.Round(spawnPosition.y), 0);
        

        // spawn the plant
        var spawnedPlant = Instantiate(plantPrefabs[randomPlant], spawnPosition, Quaternion.identity);

        // randomly flip the plant
        if (Random.Range(0, 2) == 1)
            spawnedPlant.transform.localScale = new Vector3(-spawnedPlant.transform.localScale.x, spawnedPlant.transform.localScale.y, spawnedPlant.transform.localScale.z);

        plantsOnField.Add(spawnedPlant);

        // make the parent of the plant the field
        spawnedPlant.transform.parent = transform;
        
    }

    public int Get_price(int item)
    {
        // return the price of the plantprefab[item]
        return plantPrefabs[item].GetComponent<Plant>().price;

    }

}

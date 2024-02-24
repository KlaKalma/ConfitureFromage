using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{

    // list of all the availiable plant prefabs
    public List<GameObject> plantPrefabs = new List<GameObject>();

    // grid manager reference
    public GridManager gridManager;

    // list of all the plants not modifiable
    [System.NonSerialized]
    public List<GameObject> plantsOnField = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

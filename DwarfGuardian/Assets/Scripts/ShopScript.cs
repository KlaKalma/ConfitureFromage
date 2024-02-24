using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    // include gride manager game object in the script
    public GameObject GameManager;
    public GameObject plantManager;
    // include camera game object in the script
    public Camera Camera;
    private int number_of_items;
    private float angle;
    // Start is called before the first frame update
    private bool in_shop;
    private float tile_pos_x, tile_pos_y;
    private Vector3 tile_pos;
    private Dictionary<int, int> _prices;
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        in_shop = false;
        //get prefabs count for plant manager gameobject
        number_of_items = plantManager.GetComponent<PlantManager>().plantPrefabs.Count;
        angle = 360/number_of_items;

        _prices = new Dictionary<int, int>();
        for(int i = 0; i < number_of_items; i++)
        {   
            Debug.Log(plantManager.GetComponent<PlantManager>().Get_price(i));
            _prices[i] = plantManager.GetComponent<PlantManager>().Get_price(i);
            Debug.Log(_prices[i]); 
        } 
    }

    // Update is called once per frame
    void Update()
    {
        // if left mouse button is clicked
        if(Input.GetMouseButtonDown(0))
        {
            transform.position = Camera.ScreenToWorldPoint(Input.mousePosition);
            // if sprite renderer inactive
            if (GetComponent<SpriteRenderer>().enabled == false)
            {   
                tile_pos_x = Mathf.Floor(transform.position.x+0.5f);
                tile_pos_y = Mathf.Floor(transform.position.y+0.5f);
                tile_pos = new Vector3(tile_pos_x, tile_pos_y, 0);
                
                transform.position = new Vector3(tile_pos_x, tile_pos_y, -1);
                GetComponent<SpriteRenderer>().enabled = true;
                GetComponent<Collider2D>().enabled = true;
            }
            else
            {
                if(in_shop)
                {
                    int thunes = GameManager.GetComponent<GameManager>().thunes;
                    int item = Get_item();
                    Debug.Log(item);
                    if (_prices.TryGetValue(item, out var price) && thunes >= _prices[item])
                    {
                        var spawnedPlant = Instantiate(plantManager.GetComponent<PlantManager>().plantPrefabs[item], tile_pos, Quaternion.identity);
                        GameManager.GetComponent<GameManager>().thunes -= price;
                    }
                    else
                    {
                        Debug.Log("Object not found or not enough money");
                    }
                    in_shop = false;
                }
                GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }
    void OnMouseOver()
    {
        in_shop = true;
    }

    void OnMouseExit()
    {
        in_shop = false;
    }

    int Get_item()
    {
        // give a int depending on the sprite's circular fraction devided in number_of_items
        int item = number_of_items/2 + 1 + (int) Mathf.Floor(Mathf.Atan2(transform.position.y-tile_pos_y, transform.position.x-tile_pos_x) * Mathf.Rad2Deg / angle);
        return item;
    }
}

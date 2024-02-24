using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;


public interface IPrice
{
    int price { get; set; }
}

public class ShopScript : MonoBehaviour
{
    public bool _isAccessible;
    // include gride manager game object in the script
    public GameManager gameManager;
    public PlantManager plantManager;
    // include camera game object in the script
    public Camera _camera;
    private int number_of_items;
    private float angle;
    // Start is called before the first frame update
    private bool in_shop;
    private float tile_pos_x, tile_pos_y;
    private Vector3 tile_pos;
    private Dictionary<int, int> _prices;
    void Start()
    {
        _isAccessible = true;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        in_shop = false;
        //get prefabs count for plant manager gameobject
        number_of_items = plantManager.plantPrefabs.Count;
        angle = 360/number_of_items;

        _prices = new Dictionary<int, int>();
        Debug.Log("Number of items: " + number_of_items);
        // iterate over the items of the plant manager and get the price of each item
        for(int i = 0; i < number_of_items; i++)
        {   
            // Debug.Log(i);
            // Debug.Log(plantManager.Get_price(i));
            _prices[i] = plantManager.Get_price(i);
            // Debug.Log(_prices[i]); 

            float phase = 45;

            // setup the price of the item 
            float x = Mathf.Cos(i * angle * Mathf.Deg2Rad + phase);
            float y = Mathf.Sin(i * angle * Mathf.Deg2Rad + phase);
            Vector3 pos = new Vector3(x, y, 0);
            pos = pos * 1;
            pos = pos + new Vector3(0, 0, -3);
            GameObject price = new GameObject();

            price.transform.position = pos;
            price.AddComponent<TextMesh>();
            price.GetComponent<TextMesh>().text = _prices[i].ToString();
            price.GetComponent<TextMesh>().fontSize = 100;
            // if(_prices[i] > gameManager.Money)
            //     price.GetComponent<TextMesh>().color = Color.red;
            // else
            //     price.GetComponent<TextMesh>().color = Color.green;
            price.GetComponent<TextMesh>().characterSize = 0.1f;
            price.GetComponent<TextMesh>().alignment = TextAlignment.Center;
            price.GetComponent<TextMesh>().anchor = TextAnchor.MiddleCenter;
            price.GetComponent<TextMesh>().fontStyle = FontStyle.Bold;
            price.transform.parent = transform;

            // get the sprite renderer of the plant prefab
            SpriteRenderer sr = plantManager.plantPrefabs[i].GetComponent<SpriteRenderer>();
            // get the sprite of the plant prefab
            Sprite sprite = sr.sprite;
            // create a new game object
            GameObject go = new GameObject();
            // add a sprite renderer to the game object
            go.AddComponent<SpriteRenderer>();
            // get the sprite renderer of the game object
            SpriteRenderer go_sr = go.GetComponent<SpriteRenderer>();
            // set the sprite of the game object to the sprite of the plant prefab
            go_sr.sprite = sprite;
            // set the position of the game object to the position of the plant prefab
            go.transform.position = pos * 2;
            // set the scale of the game object to the scale of the plant prefab
            go.transform.localScale = 0.6f * Vector3.one;
            // set the parent of the game object to the shop game object
            go.transform.parent = transform;

        } 
    }

    // Update is called once per frame
    void Update()
    {
        // if left mouse button is clicked
        if(Input.GetMouseButtonDown(0) && _isAccessible)
        {
            transform.position = _camera.ScreenToWorldPoint(Input.mousePosition);

            
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
                    int Money = gameManager.Money;
                    int item = Get_item();
                    // Debug.Log(item);
                    if (_prices.TryGetValue(item, out var price) && Money >= _prices[item])
                    {
                        tile_pos_x = Mathf.Floor(transform.position.x+0.5f);
                        tile_pos_y = Mathf.Floor(transform.position.y+0.5f);
                        tile_pos = new Vector3(tile_pos_x, tile_pos_y, 0);
                        
                        GameObject spawnedPlant = plantManager.SpawnPlant(item, tile_pos);
                        gameManager.addMoney(-price);
                    }
                    else if (Money < _prices[item])
                    {
                        Debug.Log("Not enough money"); 
                    } else {
                        Debug.Log("Object not found");
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
        float mouseAngle = Mathf.Atan2(transform.position.y-tile_pos_y, transform.position.x-tile_pos_x);
        Debug.Log(transform.position + " " + tile_pos + " " + mouseAngle * Mathf.Rad2Deg);
        // make it between 0 and 360 with modulo
        mouseAngle = (mouseAngle + 2*Mathf.PI) % (2*Mathf.PI);

        int item = (int) Mathf.Floor(mouseAngle * Mathf.Rad2Deg / angle);
        Debug.Log("Item: " + item + " Angle: " + mouseAngle * Mathf.Rad2Deg);

        return item;
    }
}

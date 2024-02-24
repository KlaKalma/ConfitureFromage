using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    // include gride manager game object in the script
    public GameObject GameManager;
    // include camera game object in the script
    public Camera Camera;
    public int number_of_items;
    private float angle;
    // Start is called before the first frame update
    private bool in_shop;
    private float tile_pos_x, tile_pos_y;
    private Dictionary<int, int> _prices;
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        in_shop = false;
        angle = 360/number_of_items;

        //_prices[item] = price;
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

                    if (_prices.TryGetValue(item, out var price) && thunes >= price)
                    {
                        // spawn item

                        GameManager.GetComponent<GameManager>().thunes -= price;
                    }
                    else
                    {
                        Debug.Log("Not enough money");
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
        int item = number_of_items - 1 +(int) Mathf.Floor(Mathf.Atan2(transform.position.y-tile_pos_y, transform.position.x-tile_pos_x) * Mathf.Rad2Deg / angle);
        return item;
    }
}

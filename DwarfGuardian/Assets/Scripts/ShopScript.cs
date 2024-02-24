using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    // include gride manager game object in the script
    public GameObject GridManager;
    // include camera game object in the script
    public Camera Camera;
    // Start is called before the first frame update
    private bool in_shop;
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        in_shop = false;
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
                transform.position = new Vector3(Mathf.Floor(transform.position.x+0.5f), Mathf.Floor(transform.position.y+0.5f), 0);
                GetComponent<SpriteRenderer>().enabled = true;
                GetComponent<Collider2D>().enabled = true;
            }
            else
            {
                if(in_shop)
                {
                    // do stuff for the shop here
                    Debug.Log("Shop");
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
}

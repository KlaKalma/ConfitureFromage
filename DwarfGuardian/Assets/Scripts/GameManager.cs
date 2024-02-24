using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


    [SerializeField] public int _width, _height;

    public float _tileSize;
    public int thunes;
    [SerializeField] private Tile _tilePrefab;
 
    [SerializeField] private Transform _cam;
 
    private Dictionary<Vector2, Tile> _tiles;
    public MoneyCount MoneyCountText;
    public int Money = 0;
 
    void Start() {
        // set the width and height of the grid depending of the tile prefab size
        _tileSize = _tilePrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        
        GenerateGrid();
        MoneyCountText.UpdateText(Money.ToString());
    }
 
    void GenerateGrid() {
        GameObject tileParent = GameObject.Find("Tiles");
        if (tileParent == null)
        {
            tileParent = new GameObject("Tiles");
        }

        _tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < _width; x++) {
            for (int y = 0; y < _height; y++) {
                

                var spawnedTile = Instantiate(_tilePrefab, new Vector3((x - _width/2) * _tileSize, (y - _height/2) * _tileSize), Quaternion.identity, tileParent.transform);
                spawnedTile.name = $"Tile {x} {y}";
 
                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Inita(isOffset);
 
                _tiles[new Vector2(x, y)] = spawnedTile;
            }
        }
 
    // _cam.transform.position = new Vector3(_width* _tileSize/2 -0.5f, _height* _tileSize / 2 - 0.5f,-10);
    // _cam.transform.position = new Vector3( 0, 0, -10);
    
    }

    public int addMoney(int money)
    {
        Money += money;
        MoneyCountText.UpdateText(Money.ToString());
        return Money;
    }

 
    public Tile GetTileAtPosition(Vector2 pos) {
        if (_tiles.TryGetValue(pos, out var tile)) return tile;
        return null;
    }
}

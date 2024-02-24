using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {
    [SerializeField] private int _width, _height;
    [SerializeField] private float _tileSize;
 
    [SerializeField] private Tile _tilePrefab;
 
    [SerializeField] private Transform _cam;
 
    private Dictionary<Vector2, Tile> _tiles;
 
    void Start() {
        // set the width and height of the grid depending of the tile prefab size
       _tileSize = _tilePrefab.GetComponent<SpriteRenderer>().bounds.size.x;


        GenerateGrid();
    }
 
    void GenerateGrid() {
        GameObject tileParent = GameObject.Find("Tiles");
        if (tileParent == null)
        {
            tileParent = new GameObject("Tiles");
        }

        //_tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < _width; x++) {
            for (int y = 0; y < _height; y++) {
                

                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x * _tileSize, y * _tileSize), Quaternion.identity, tileParent.transform);
                spawnedTile.name = $"Tile {x} {y}";
 
                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Inita(isOffset);
 
                //_tiles[new Vector2(x, y)] = spawnedTile;
            }
        }
 
    _cam.transform.position = new Vector3(_width* _tileSize/2 -0.5f, _height* _tileSize / 2 - 0.5f,-10);
    }
 
    public Tile GetTileAtPosition(Vector2 pos) {
        if (_tiles.TryGetValue(pos, out var tile)) return tile;
        return null;
    }
}

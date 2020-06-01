using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tilePrefabs;

    Vector3 maxTile = Vector3.zero;

    [SerializeField]
    private CameraMovement cameraMovement;

    public Dictionary<Point, TileScript> Tiles { get; set; }

    //size of the tiles
    public float tileSize
    {
        get { return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }


    // Start is called before the first frame update
    void Start()
    {
        createLevel();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //places a X by Y field of tiles
    private void createLevel()
    {
        Tiles = new Dictionary<Point, TileScript>();
        //string to identify which tile to place
        string[] mapData = readLevelText();

        int mapX = mapData[0].ToCharArray().Length;
        int mapY = mapData.Length;

        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));
        for (int y = 0; y < mapY; y++)
        {
            char[] newTiles = mapData[y].ToCharArray();
            for (int x = 0; x < mapX; x++)
            {
                //places a tile in the world
                placeTile(newTiles[x].ToString(), x, y, worldStart);

            }
        }

        //stores last tile in map to the maxTile variable
        maxTile = Tiles[new Point(mapX - 1, mapY - 1)].transform.position;

        //sets camera limits to max tile position
        cameraMovement.setLimits(new Vector3(maxTile.x + tileSize, maxTile.y - tileSize));
    }

    private void placeTile(string tileType, int x, int y, Vector3 worldStart)
    {
        int tileIndex = int.Parse(tileType);
        TileScript newTile = Instantiate(tilePrefabs[tileIndex]).GetComponent<TileScript>();

        //uses the new tile variable to change the position of the tile
        newTile.Setup(new Point(x, y), new Vector3(worldStart.x + (tileSize * x), worldStart.y - (tileSize * y), 0));

        //adds tile to Dictionary
        Tiles.Add(new Point(x, y), newTile);
    }

    private string[] readLevelText()
    {
        TextAsset bindData = Resources.Load("Level") as TextAsset;
        string data = bindData.text.Replace(Environment.NewLine, string.Empty);

        return data.Split('-');
    }
}


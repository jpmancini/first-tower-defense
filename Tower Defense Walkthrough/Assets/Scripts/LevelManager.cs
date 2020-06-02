using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.Generic;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField]
    private GameObject[] tilePrefabs;

    Vector3 maxTile = Vector3.zero;

    [SerializeField]
    private CameraMovement cameraMovement;

    [SerializeField]
    private Transform map;

    private Point greenSpawn, redSpawn;

    [SerializeField]
    private GameObject greenPortalPrefab;
    [SerializeField]
    private GameObject redPortalPrefab;

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

        int mapX = mapData[0].ToCharArray().Length; //length of tile
        int mapY = mapData.Length; //#of rows to place

        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height)); //top left corner of camera view
        for (int y = 0; y < mapY; y++) //for each row
        {
            char[] newTiles = mapData[y].ToCharArray(); //get each individual tile to place
            for (int x = 0; x < mapX; x++)//for each tile
            {
                //place tile in the world
                placeTile(newTiles[x].ToString(), x, y, worldStart);

            }
        }

        //stores last tile in map to the maxTile variable
        maxTile = Tiles[new Point(mapX - 1, mapY - 1)].transform.position;

        //sets camera limits to max tile position
        cameraMovement.setLimits(new Vector3(maxTile.x + tileSize, maxTile.y - tileSize));

        SpawnPortals();
    }

    private void placeTile(string tileType, int x, int y, Vector3 worldStart)
    {
        int tileIndex = int.Parse(tileType);
        TileScript newTile = Instantiate(tilePrefabs[tileIndex]).GetComponent<TileScript>();

        //uses the new tile variable to change the position of the tile
        newTile.Setup(new Point(x, y), new Vector3(worldStart.x + (tileSize * x), worldStart.y - (tileSize * y), 0), map);
    }

    private string[] readLevelText()
    {
        TextAsset bindData = Resources.Load("Level") as TextAsset; //load in the file
        string data = bindData.text.Replace(Environment.NewLine, string.Empty); //strip newlines

        return data.Split('-'); //split into array of strings based on location of '-'
    }


    private void SpawnPortals()
    {
        greenSpawn = new Point(0, 0);
        redSpawn = new Point(10, 6);

        Instantiate(greenPortalPrefab, Tiles[greenSpawn].GetComponent<TileScript>().WorldPosition, Quaternion.identity);

        Instantiate(redPortalPrefab, Tiles[redSpawn].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
    }
}


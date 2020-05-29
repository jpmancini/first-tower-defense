using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : Singleton<LevelManager>
{
    //serializing  tile object
    [SerializeField]
    private GameObject[] tilePrefabs;

    //serializing camera object
    [SerializeField]
    private CameraMovement cameraMovement;

    //serializing map
    [SerializeField]
    private Transform map;

    //the point location for the blue and red portals
    private Point blueSpawn, redSpawn;

    //prefabs for the blue and red portals
    [SerializeField]
    private GameObject bluePortalPrefab, redPortalPrefab;

    //creating the tiles dicitonary
    public Dictionary<Point, TileScript> Tiles { get; set; }

    //Makes TileSize accessable from anywhere
    public float TileSize
    {
        get { return  tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateLevel();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //used to test point struct
    public void TestValue(Point p)
    {
        Debug.Log("Changing value");
    }

    //fucntion uses the array from ReadLevelText to create the level
    private void CreateLevel()
    {
        //creates a new tiles Dictionary
        Tiles = new Dictionary<Point, TileScript>();

        string[] mapData = ReadLevelText();

        int mapX = mapData[0].ToCharArray().Length;
        int mapY = mapData.Length;

        Vector3 maxTile = Vector3.zero;

        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));

        for (int y = 0; y < mapY; y++) //y position
        {
            char[] newTiles = mapData[y].ToCharArray();

            for (int x = 0; x < mapX; x++) //x position
            {
            //Places tile in the world
            PlaceTile(newTiles[x].ToString(),x,y,worldStart);
            }
        }

        //creates the maxTile using the tiles dictionary
        maxTile = Tiles[new Point(mapX-1, mapY-1)].transform.position;

        //sets the camera limits to the max tile position
        cameraMovement.SetLimits(new Vector3(maxTile.x + TileSize, maxTile.y - TileSize));

        SpawnPortals();
    }

    //Function that places each tile at position x,y
    private void  PlaceTile(string tileType, int x, int y, Vector3 worldStart)
    {
        int tileIndex = int.Parse(tileType);

        //Create a new tile
        TileScript newTile = Instantiate(tilePrefabs[tileIndex]).GetComponent<TileScript>();

        //Sets the world position of the new tileand creates a new point for the created tile
        newTile.Setup(new Point(x,y), new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0), map);

    }

    //Reads Level.txt to generate a string that will be put into the mapData variable
    private string[] ReadLevelText()
    {
        TextAsset bindData = Resources.Load("Level") as TextAsset;

        string data = bindData.text.Replace(Environment.NewLine, string.Empty);

        return data.Split('-');
    }

    //spawns the portals into the game
    private void SpawnPortals()
    {
        //the point position of the blue portal (top left right now)
        blueSpawn = new Point(0, 0);

        //idk what the quaternion part is either
        Instantiate(bluePortalPrefab, Tiles[blueSpawn].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
        
        //the point position of the red portal bottom left right now)    
        redSpawn = new Point(14, 7);

        //copy pasted from above
        Instantiate(redPortalPrefab, Tiles[redSpawn].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
    }

}

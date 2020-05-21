using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    //Creating tile object
    [SerializeField]
    private GameObject[] tilePrefabs;

    [SerializeField]
    private CameraMovement cameraMovement;

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

          maxTile = PlaceTile(newTiles[x].ToString(),x,y,worldStart);
        }
      }

      cameraMovement.SetLimits(new Vector3(maxTile.x + TileSize, maxTile.y - TileSize));
    }

    //Function that places each tile at position x,y
    private Vector3 PlaceTile(string tileType, int x, int y, Vector3 worldStart)
    {
      int tileIndex = int.Parse(tileType);

      //Create a new tile
      GameObject newTile = Instantiate(tilePrefabs[tileIndex]);

      //Sets the position of the new tile
      newTile.transform.position = new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0);
      return newTile.transform.position;
    }

    //Reads Level.txt to generate a string that will be put into the mapData variable
    private string[] ReadLevelText()
    {
      TextAsset bindData = Resources.Load("Level") as TextAsset;

      string data = bindData.text.Replace(Environment.NewLine, string.Empty);

      return data.Split('-');
    }

}

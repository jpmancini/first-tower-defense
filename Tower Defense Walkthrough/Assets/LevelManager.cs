using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tilePrefabs;

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
        //string to identify which tile to place
        string[] mapData = readLevelText();

        int mapX = mapData[0].ToCharArray().Length;
        int mapY = mapData.Length;

        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));
        for(int y = 0; y < mapY; y++)
        {
            char[] newTiles = mapData[y].ToCharArray();
            for (int x = 0; x < mapX; x++)
            { 
                placeTile(newTiles[x].ToString(),x, y, worldStart);
               
            }
        }
    }

    public void placeTile(string tileType, int x, int y, Vector3 worldStart)
    {
        int tileIndex = int.Parse(tileType);
        GameObject newTile = Instantiate(tilePrefabs[tileIndex]);
        newTile.transform.position = new Vector3(worldStart.x + (tileSize*x), worldStart.y - (tileSize * y), 0);
    }

    private string[] readLevelText()
    {
        TextAsset bindData = Resources.Load("Level") as TextAsset;
        string data = bindData.text.Replace(Environment.NewLine, string.Empty);

        return data.Split('-');
    }
}


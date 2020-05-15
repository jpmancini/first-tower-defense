using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //Creating tile object
    [SerializeField]
    private GameObject tile;

    //Makes TileSize accessable from anywhere
    public float TileSize
    {
      get { return  tile.GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
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

    private void CreateLevel()
    {

      Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));

      for (int y = 0; y < 10; y++) //y position
      {
        for (int x = 0; x < 10; x++) //x position
        {
          PlaceTile(x,y,worldStart);
        }
      }
    }

    //Function that places each tile at position x,y
    private void PlaceTile(int x, int y, Vector3 worldStart)
    {
      //Create a new tile
      GameObject newTile = Instantiate(tile);

      //Sets the position of the new tile
      newTile.transform.position = new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0);
    }
}

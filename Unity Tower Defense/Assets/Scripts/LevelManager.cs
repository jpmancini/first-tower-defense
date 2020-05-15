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

      for (int y = 0; y < 5; y++) //y position
      {
        for (int x = 0; x < 5; x++) //x position
        {
          //Create a new tile
          GameObject newTile = Instantiate(tile);

          //Sets the position of the new tile
          newTile.transform.position = new Vector3(TileSize*x, TileSize*y, 0);
        }
      }
    }
}

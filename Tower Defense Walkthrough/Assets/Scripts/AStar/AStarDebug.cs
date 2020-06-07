using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarDebug : MonoBehaviour
{
    private TileScript start, goal;

    
     [SerializeField]
     private Sprite blankTile;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ClickTile();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AStar.GetPath(start.GridPosition);
        }
    }


    private void ClickTile()
    {
        if (Input.GetMouseButtonDown(1)) //right click
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero); //get the place hit

            if(hit.collider != null) //if you hit something
            {
                TileScript tmp = hit.collider.GetComponent<TileScript>(); //get a reference to the Tile clicked

                if(tmp != null) //if you actually hit a Tile
                {
                    if(start == null) {
                        start = tmp;
                        start.SpriteRenderer.color = new Color32(255, 132, 0, 255);
                        start.Debugging = true;
                        goal.SpriteRenderer.sprite = blankTile;
                    }
                    else if(goal == null)
                    {
                        goal = tmp;
                        goal.SpriteRenderer.color = new Color32(255,0,0,255);
                        goal.Debugging = true;
                        goal.SpriteRenderer.sprite = blankTile;
                    }
                }
            }
        }
    }

    public void DebugPath(HashSet<Node> openList)
    {
        foreach(Node node in openList)
        {
            node.TileRef.SpriteRenderer.color = Color.cyan;
            node.TileRef.SpriteRenderer.sprite = blankTile;
        }
    }
}

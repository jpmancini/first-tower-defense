using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AStar
{
   private static Dictionary<Point, Node> nodes;

    private static void CreateNodes()
    {
        nodes = new Dictionary<Point, Node>();

        foreach(TileScript tile in LevelManager.Instance.Values)
        {
            nodes.Add(tile.GridPosition, new Node(tile));
        }
    }


    public static void GetPath(Point start)
    {
        if (nodes == null)
        {
            CreateNodes();
        }

        HashSet<Node> openList = new HashSet<Node>();

        Node currentNode = nodes[start];

        openList.Add(currentNode);


        /*FOR DEBUG ONLY, REMOVE LATER*/

        GameObject.Find("AStarDebug").GetComponent<AStarDebug>().DebugPath(openList); //color every tile in the openList
    }
}

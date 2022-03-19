using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool occupied;
    public int x, y;

    public Node parent;
    public List<Node> children;
    public Vector2 root;

    public Node(int x, int y)
    {
        this.x = x;
        this.y = y;

        occupied = false;

        children = new List<Node>();
    }

    /// <summary>
    /// Get what directions the Node is connected to other nodes 
    /// 0 - up
    /// 1 - down
    /// 2 - right
    /// 3 - left
    /// </summary>
    /// <returns></returns>
    public bool[] GetOpenings()
    {
        bool[] dir = new bool[4];

        if (parent != null)
        {
            Point parentDir = GetDirection(parent);
            UpdateBoolDirs(dir, parentDir);
        }

        for (int i = 0; i < children.Count; i++)
        {
            UpdateBoolDirs(dir, GetDirection(children[i]));
        }

        return dir;
    }

    private void UpdateBoolDirs(bool[] directions, Point dir)
    {
        if (dir.X != 0) // Is left or right
        {
            if (dir.X == 1) // Right
            {
                directions[2] = true;
            }
            else // Left
            {
                directions[3] = true;
            }
        }
        else // Is up or down 
        {
            if (dir.Y == 1) // Up
            {
                directions[0] = true;
            }
            else // Down
            {
                directions[1] = true;
            }
        }
    }

    private Point GetDirection(Node node)
    {
        return new Point(node.x - x, node.y - y);
    }
}

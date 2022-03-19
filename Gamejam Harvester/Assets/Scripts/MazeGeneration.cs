using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGeneration : MonoBehaviour
{
    private Node[,] map;

    private int width;
    private int height;

    private float tileSideLength;

    public Node[] nodes;
    public int counter = 0;

    public void Init(int width, int height, float tileSideLength)
    {

        this.width = width;
        this.height = height;

        this.tileSideLength = tileSideLength;

        map = new Node[width, height];
        nodes = new Node[width * height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                map[x, y] = new Node(x, y);

            }
        }
    }

    public void GenerateMaze()
    {
        Stack<Node> nodeStack = new Stack<Node>();

        // Initial
        nodeStack.Push(map[0, 0]);
        map[0, 0].occupied = true;

        counter = 0;
        // Continue till tree is complete. ie no more items in the map to fill/nothing else on the stack
        do
        {
            // Check stack if emtpy
            if(nodeStack.Count == 0)
            {
                break;
            }

            Node current = nodeStack.Peek();

            // Get its neighbors (Not occupied)
            List<Node> neighbors = GetNeighbors(current);

            // Make sure neighbors not empty 
            if(neighbors.Count > 0)
            {
                //  Choose random child 
                Node neighbor = neighbors[Random.Range(0, neighbors.Count)];

                //  Set it to occupied (Can no longer be found)
                neighbor.occupied = true;

                current.children.Add(neighbor);
                neighbor.parent = current;

                //  Add to stack
                nodeStack.Push(neighbor);

                counter++;

            }
            else // If empty pop current from stack
            {
                nodeStack.Pop();
            }

        } while (counter < (width * height));

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                nodes[y * width + x] = map[x, y];
            }
        }
    }

    private List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();
        // Up 
        if ((node.y + 1) < height && map[node.x, node.y + 1].occupied == false)
        {
            neighbors.Add(map[node.x, node.y + 1]);
        }

        // Down 
        if ((node.y - 1 >= 0) && map[node.x, node.y - 1].occupied == false)
        {
            neighbors.Add(map[node.x, node.y - 1]);
        }

        // Right
        if ((node.x + 1) < width && map[node.x + 1, node.y].occupied == false)
        {
            neighbors.Add(map[node.x + 1, node.y]);
        }

        // Left 
        if ((node.x - 1) >= 0 && map[node.x - 1, node.y].occupied == false)
        {
            neighbors.Add(map[node.x - 1, node.y]);
        }

        return neighbors;
    }

    public void VisualizeMaze(GameObject chunk, Transform parent = null)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Node node = map[x, y];

                Vector2 nodeRoot = new Vector2(
                        node.x * tileSideLength - ((width * tileSideLength) / 2),
                        node.y * tileSideLength - ((width * tileSideLength) / 2)
                        );
                node.root = nodeRoot;

                Instantiate(chunk, nodeRoot, Quaternion.identity);
                
                bool[] openings = node.GetOpenings();

                if (openings[0]) // up
                {
                    Instantiate(chunk, nodeRoot + Vector2.up, Quaternion.identity);
                    Instantiate(chunk, nodeRoot + Vector2.up + Vector2.up, Quaternion.identity);
                }
                if (openings[1]) // Down
                {
                    Instantiate(chunk, nodeRoot - Vector2.up, Quaternion.identity);
                    Instantiate(chunk, nodeRoot - Vector2.up - Vector2.up, Quaternion.identity);
                }
                if (openings[2]) // Right
                {
                    Instantiate(chunk, nodeRoot + Vector2.right, Quaternion.identity);
                    Instantiate(chunk, nodeRoot + Vector2.right + Vector2.right, Quaternion.identity);
                }
                if (openings[3]) // Left
                {
                    Instantiate(chunk, nodeRoot - Vector2.right, Quaternion.identity);
                    Instantiate(chunk, nodeRoot - Vector2.right - Vector2.right, Quaternion.identity);
                }
                
            }
        }
    }
}

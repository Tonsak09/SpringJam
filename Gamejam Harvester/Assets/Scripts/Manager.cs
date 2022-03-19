using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private MazeGeneration mg;
    public int mapWidth;
    public int mapHeight;

    public float tileLength;

    public GameObject trailPrefeb;
    public GameObject harvestSpot;

    public Node[] nodes;

    // Start is called before the first frame update
    void Start()
    {

        mg = this.gameObject.AddComponent<MazeGeneration>() as MazeGeneration;
        mg.Init(mapWidth, mapHeight, tileLength);

        mg.GenerateMaze();
        
        nodes = mg.nodes;
        mg.VisualizeMaze(trailPrefeb);


        for (int i = 0; i < nodes.Length; i++)
        {
            int rng = Random.Range(0, 100);

            if(rng < 5)
            {
                Instantiate(harvestSpot, nodes[i].root, Quaternion.identity);
            }
        }


    }

}

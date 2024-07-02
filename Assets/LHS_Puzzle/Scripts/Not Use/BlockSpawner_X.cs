using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner_X : MonoBehaviour
{
    public GameObject blockPrefab;
    public int rows = 5;
    public int columns = 5;
    public float blockSpacing = 1.1f;

    private GameObject[,] blocks;

    void Start()
    {
        SpawnBlocks();
    }

    void SpawnBlocks()
    {
        blocks = new GameObject[rows, columns];

        for(int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector3 blockPosition = new Vector3(col * blockSpacing, row * blockSpacing, 0);

                GameObject block = Instantiate(blockPrefab, blockPosition, Quaternion.identity);

                block.transform.parent = this.transform;

                blocks[row, col] = block;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

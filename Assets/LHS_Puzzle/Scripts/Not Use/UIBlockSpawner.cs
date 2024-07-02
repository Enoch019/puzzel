using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBlockSpawner : MonoBehaviour
{
    public GameObject[] blockPrefabs; // **여러 블록 프리팹**
    public int rows = 5;
    public int columns = 5;
    public float blockSpacing = 5f;

    private RectTransform rectTransform;
    private List<GameObject> blocks = new List<GameObject>();

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        SpawnInitialBlocks();
    }

    void SpawnInitialBlocks()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Vector2 position = new Vector2(j * (rectTransform.sizeDelta.x / columns + blockSpacing),
                                               -i * (rectTransform.sizeDelta.y / rows + blockSpacing));
                SpawnRandomBlockAtPosition(position);
            }
        }
    }

    public void SpawnRandomBlockAtPosition(Vector2 position)
    {
        GameObject blockPrefab = blockPrefabs[Random.Range(0, blockPrefabs.Length)];
        GameObject block = Instantiate(blockPrefab, transform);
        block.GetComponent<RectTransform>().anchoredPosition = position;
        block.GetComponent<RectTransform>().sizeDelta = new Vector2(rectTransform.sizeDelta.x / columns, rectTransform.sizeDelta.y / rows); // **크기 조정**
        blocks.Add(block);
    }
}
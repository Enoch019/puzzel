using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public List<GameObject> blockPrefabs = new List<GameObject>(); // ��� ������
    public Transform board; // ��ϵ��� ��ġ�� �θ� ������Ʈ

    public int rows = 5; // ���� ��
    public int columns = 5; // ���� ��
    public float blockSpacing = 0f; // ��� ����

    void Start()
    {
        SpawnBlocks();
    }
    void SpawnBlocks()
    {
        RectTransform boardRectTransform = board.GetComponent<RectTransform>();
        float blockSize = blockPrefabs[0].GetComponent<RectTransform>().sizeDelta.x;

        // board�� RectTransform�� �������� ���� ������ ����Ѵ�.
        float startX = -boardRectTransform.rect.width / 2 + blockSize / 2;
        float startY = boardRectTransform.rect.height / 2 - blockSize / 2;

        // ��� ������ ����Ʈ�� ���´�
        //Shuffle(blockPrefabs);

        //int prefabIndex = 0; // �ε��� ����

        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                int prefabIndex = Random.Range(0, blockPrefabs.Count);
                GameObject blockGO = Instantiate(blockPrefabs[prefabIndex], board);

                float xPosition = startX + column * (blockSize + blockSpacing);
                float yPosition = startY - row * (blockSize + blockSpacing);
                blockGO.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPosition, yPosition);

                PuzzleBlock blockScript = blockGO.GetComponent<PuzzleBlock>();
                blockScript.row = row;
                blockScript.column = column;

                // ����� ������ ��, �������� �ִϸ��̼� ���� (���÷� ù �ุ ���������� ����)
                if (row == 0)
                    blockScript.FallDown();

                //Shuffle(blockPrefabs);
                //// ��� ������ �ν��Ͻ�ȭ
                //GameObject block = Instantiate(blockPrefabs[prefabIndex], board);

                //// ������ �ν��Ͻ� ��� ���� ����
                //block.name = "( " + row + ", " + column + " )";

                //// ��ġ ���� (���� ��� ����)
                //float xPosition = startX + column * (blockSize + blockSpacing);
                //float yPosition = startY - row * (blockSize + blockSpacing);
                //block.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPosition, yPosition);

                //// ��� ��ġ ���� ����
                //PuzzleBlock blockScript = block.GetComponent<PuzzleBlock>();
                //blockScript.SetPosition(row, column);

                //// ���� ������ �ε��� ���� �� �������� ����
                //prefabIndex = (prefabIndex + 1) % blockPrefabs.Count;
            }
        }
    }

    // ����Ʈ�� ���� �Լ��̴�
    void Shuffle(List<GameObject> list)
    {
        for(int i = 0; i < list.Count; i++)
        {
            GameObject temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}

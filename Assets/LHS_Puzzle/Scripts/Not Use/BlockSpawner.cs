using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public List<GameObject> blockPrefabs = new List<GameObject>(); // 블록 프리팹
    public Transform board; // 블록들이 배치될 부모 오브젝트

    public int rows = 5; // 행의 수
    public int columns = 5; // 열의 수
    public float blockSpacing = 0f; // 블록 간격

    void Start()
    {
        SpawnBlocks();
    }
    void SpawnBlocks()
    {
        RectTransform boardRectTransform = board.GetComponent<RectTransform>();
        float blockSize = blockPrefabs[0].GetComponent<RectTransform>().sizeDelta.x;

        // board의 RectTransform을 기준으로 시작 지점을 계산한다.
        float startX = -boardRectTransform.rect.width / 2 + blockSize / 2;
        float startY = boardRectTransform.rect.height / 2 - blockSize / 2;

        // 블록 프리팹 리스트를 섞는다
        //Shuffle(blockPrefabs);

        //int prefabIndex = 0; // 인덱스 변수

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

                // 블록이 생성된 후, 떨어지는 애니메이션 적용 (예시로 첫 행만 떨어지도록 설정)
                if (row == 0)
                    blockScript.FallDown();

                //Shuffle(blockPrefabs);
                //// 블록 프리팹 인스턴스화
                //GameObject block = Instantiate(blockPrefabs[prefabIndex], board);

                //// 스폰된 인스턴스 블록 정보 수정
                //block.name = "( " + row + ", " + column + " )";

                //// 위치 설정 (좌측 상단 기준)
                //float xPosition = startX + column * (blockSize + blockSpacing);
                //float yPosition = startY - row * (blockSize + blockSpacing);
                //block.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPosition, yPosition);

                //// 블록 위치 정보 설정
                //PuzzleBlock blockScript = block.GetComponent<PuzzleBlock>();
                //blockScript.SetPosition(row, column);

                //// 다음 프리팹 인덱스 설정 및 랜덤으로 섞기
                //prefabIndex = (prefabIndex + 1) % blockPrefabs.Count;
            }
        }
    }

    // 리스트를 섞는 함수이다
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

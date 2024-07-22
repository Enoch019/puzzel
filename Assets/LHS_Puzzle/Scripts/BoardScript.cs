using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BoardScript : MonoBehaviour
{
    public Dictionary<Vector2, GameObject> GemDict = new Dictionary<Vector2, GameObject>(); // ��ŷ��ʷ� ���� ��ǥ�� ����
    public List<GameObject> GemLists = new List<GameObject>(); // Gem 리스트 선언
    public List<Vector2> TileList = new List<Vector2>(); // 타일 리스트 선언
    public GameObject GemPrefab;
    //public Transform SpawnStart;
    public Transform board; // 잼을 스폰시키는 위치(Canvas의 밑배경 부분)
    public List<Vector2> vec; 
    public int Rows = 5;
    public int Cols = 5;
    public float blockSpacing = 0f; // 블록 간격

    void Start()
    {
        SpawnGems();
    }

    void Update()
    {
         CheakGem();
    }

    public void CheakGem()
    {
        List<Vector2> emptyPositions = new List<Vector2>();
        List<int> fl = new List<int>(5);
        int A = 0; 
        
        foreach (var gem in GemDict)
        {
            
            if (gem.Key.y == 4 && gem.Value == null)
            {
                emptyPositions.Add(gem.Key);
                fl.Add(A);
            }

            A++; 
        }

        A = 0; 

        foreach (var pos in emptyPositions)
        {
            GameObject spawnGem = Instantiate(GemPrefab, board);
            spawnGem.GetComponent<GemScript>().GemLand();
            spawnGem.name = "new"; 
            spawnGem.GetComponent<RectTransform>().anchoredPosition = TileList[fl[A]];
            GemDict[pos] = spawnGem;
            spawnGem.GetComponent<GemScript>().gemPos = pos; 
            A++; 
        }

        emptyPositions.Clear(); 
    }

    void SpawnGems()
    {
        // 스폰시키는 위치인 Board의 RectTransform을 사용한다.
        RectTransform boardRectTransform = board.GetComponent<RectTransform>();
        float blockSize = GemPrefab.GetComponent<RectTransform>().sizeDelta.x;

        // board의 RectTransform의 width와 height을 기준으로 시작 지점을 계산한다.
        float startX = boardRectTransform.rect.width / 10;
        float startY = boardRectTransform.rect.height / 10;

        for(int i = 0; i< Rows; i++)
        {
            for(int j = 0; j < Cols; j++)
            {
                float posX = 5 + (30 * i);
                float posY = 5 + (30 * j);
                TileList.Add(new Vector2(posX, posY));
            }
        }

        // 가상 좌표 체계를 정하고 dictionary의 키 값에 집어 넣는다
        for (int i = 0; i < Rows; i++)
        {
            for(int j = 0; j < Cols; j++)
            {
                // 잼 프리팹에 있는 GemScript 컴포넌트를 가져온다
                GemScript gemInfo = GemPrefab.GetComponent<GemScript>();
                
                // ���� ���� ��ǥ ü�� �Ҵ�
                //vec.Add(new Vector2(i, j));
                //GemPrefab.GetComponent<GemScript>().gemPos = gemInfo.gemPos;

                // ��ǥ�� �� �� ������Ʈ ��ųʸ��� ����
                vec.Add(new Vector2(i, j));
                //Debug.Log(GemDict[new Vector2(i,j)].GetComponent<GemScript>().gemPos);
            }
        }

        Debug.Log(boardRectTransform.rect);

        int tileIndex = 0;

        // ��ųʸ��� �Ҵ�� Ű�� ���� ���� ������Ʈ ����
        foreach (var spawnpoint in vec) 
        {            
            GameObject spawnGem = Instantiate(GemPrefab, board);
            spawnGem.GetComponent<GemScript>().GemLand();
            spawnGem.GetComponent<RectTransform>().anchoredPosition = TileList[tileIndex];
            spawnGem.GetComponent<GemScript>().gemPos = spawnpoint; 
            GemDict.Add(spawnpoint , spawnGem);
            tileIndex++;

        }
    }
    
}

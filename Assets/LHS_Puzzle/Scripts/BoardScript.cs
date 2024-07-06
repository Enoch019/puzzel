using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardScript : MonoBehaviour
{
    public Dictionary<Vector2, GameObject> GemDict = new Dictionary<Vector2, GameObject>(); // 딕셔러너로 가상 좌표계 생성
    public List<GameObject> GemLists = new List<GameObject>();
    public GameObject GemPrefab;
    public Transform board; // 블록들이 배치될 부모 오브젝트

    public int Rows = 5;
    public int Cols = 5;

    void Start()
    {
        SpawnGems();
    }

    void Update()
    {

    }

    void SpawnGems()
    {
        //RectTransform boardRectTransform = board.GetComponent<RectTransform>();
        //float blockSize = GemPrefab.GetComponent<RectTransform>().sizeDelta.x;

        //// board의 RectTransform을 기준으로 시작 지점을 계산한다.
        //float startX = -boardRectTransform.rect.width / 2 + blockSize / 2;
        //float startY = boardRectTransform.rect.height / 2 - blockSize / 2;

        // 임의 좌표 체계 생성 및 딕셔너리 게임 오브젝트 속성 설정
        for (int i = Rows - 1; i > -1; i--)
        {
            for(int j = 0; j < Cols; j++)
            {
                // 잼 타입 랜덤 할당
                GemScript gemInfo = GemPrefab.GetComponent<GemScript>();
                gemInfo.gemType = (GemScript.GemType)Random.Range(0, 5);
                //Debug.Log((int)gemInfo.gemType);
                GemPrefab.GetComponent<GemScript>().gemType = gemInfo.gemType;
                //Debug.Log((int)GemPrefab.GetComponent<GemScript>().gemType);

                // 잼의 가상 좌표 체계 할당
                gemInfo.gemPos = new Vector2(i, j);
                GemPrefab.GetComponent<GemScript>().gemPos = gemInfo.gemPos;

                // 좌표값 및 잼 오브젝트 딕셔너리에 지정
                GemDict.Add(new Vector2(i, j), GemPrefab);
                Debug.Log(GemDict[new Vector2(i,j)].GetComponent<GemScript>().gemType);
            }
        }

        

        // 딕셔너리에 할당된 키와 값에 따른 오브젝트 생성
        foreach (var spawnpoint in GemDict) 
        {
            //Debug.Log(spawnpoint.Key);
            //GemScript Info = spawnpoint.Value.GetComponent<GemScript>();
            //Debug.Log((int)Info.gemType);
            Vector2 objectPoint = spawnpoint.Key;

            GameObject spawnGem = Instantiate(spawnpoint.Value, board);
            spawnGem.GetComponent<RectTransform>().anchoredPosition = objectPoint;

        }
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardScript : MonoBehaviour
{
    public Dictionary<Vector2, GameObject> GemDict = new Dictionary<Vector2, GameObject>(); // ��ŷ��ʷ� ���� ��ǥ�� ����
    public List<GameObject> GemLists = new List<GameObject>();
    public GameObject GemPrefab;
    public Transform board; // ��ϵ��� ��ġ�� �θ� ������Ʈ

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

        //// board�� RectTransform�� �������� ���� ������ ����Ѵ�.
        //float startX = -boardRectTransform.rect.width / 2 + blockSize / 2;
        //float startY = boardRectTransform.rect.height / 2 - blockSize / 2;

        // ���� ��ǥ ü�� ���� �� ��ųʸ� ���� ������Ʈ �Ӽ� ����
        for (int i = Rows - 1; i > -1; i--)
        {
            for(int j = 0; j < Cols; j++)
            {
                // �� Ÿ�� ���� �Ҵ�
                GemScript gemInfo = GemPrefab.GetComponent<GemScript>();
                //gemInfo.gemType = (GemScript.GemType)Random.Range(0, 5);
                //Debug.Log((int)gemInfo.gemType);
                //Debug.Log((int)GemPrefab.GetComponent<GemScript>().gemType);

                // ���� ���� ��ǥ ü�� �Ҵ�
                gemInfo.gemPos = new Vector2(i, j);
                GemPrefab.GetComponent<GemScript>().gemPos = gemInfo.gemPos;

                // ��ǥ�� �� �� ������Ʈ ��ųʸ��� ����
                GemDict.Add(new Vector2(i, j), GemPrefab);
                Debug.Log(GemDict[new Vector2(i,j)].GetComponent<GemScript>().gemType);
            }
        }

        

        // ��ųʸ��� �Ҵ�� Ű�� ���� ���� ������Ʈ ����
        foreach (var spawnpoint in GemDict) 
        {
            //Debug.Log(spawnpoint.Key);
            //GemScript Info = spawnpoint.Value.GetComponent<GemScript>();
            //Debug.Log((int)Info.gemType);
            Vector2 objectPoint = spawnpoint.Key;

            GameObject spawnGem = Instantiate(spawnpoint.Value, board);
            spawnGem.GetComponent<GemScript>().GemLand(); 
            spawnGem.GetComponent<RectTransform>().anchoredPosition = objectPoint;

        }
    }
    
}

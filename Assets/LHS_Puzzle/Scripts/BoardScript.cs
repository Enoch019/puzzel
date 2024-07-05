using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardScript : MonoBehaviour
{
    public Dictionary<Vector2, GameObject> GemDict = new Dictionary<Vector2, GameObject>();
    public List<GameObject> GemLists = new List<GameObject>();
    public Transform Board;

    public int Rows = 5;
    public int Cols = 5;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void SpawnGems()
    {
        for(int i = 0; i < Rows; i++)
        {
            for(int j = 0; j < Cols; j++)
            {
                GemDict.Add(new Vector2(i, j), null);
            }
        }

    }
    
}

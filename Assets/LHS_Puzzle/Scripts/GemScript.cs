using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GemScript : MonoBehaviour
{
    [Header("정보")] public string gemName;
    public BoardScript bs;
    public int times=0; 

    public enum GemType
    {
        Fire = 0,
        Water = 1,
        Land = 2,
        Grass = 3,
        Elec = 4
    }

    [SerializeField] private List<Sprite> _sprites;
    [SerializeField] private List<Sprite> _spritesWhenClick;
    public Sprite whenClick;
    public Sprite whenClickOver; 
    private Queue<Vector2> pastGemVec = new Queue<Vector2>(5); 

    //랜덤값으로 할당 필요 
    public GemType gemType;
    //가상 좌표 체계 할당 필요 
    public Vector2 gemPos; 
    
    
    public void Awake()
    {
        //TextMeshProUGUI te = GetComponent<TextMeshProUGUI>();
        //GetComponent<Image>().sprite = _sprites[(int)gemType];
        bs = transform.parent.GetComponent<BoardScript>();
    }

    public void Click()
    {
        Debug.Log("Gem " + gameObject.name);
        transform.parent.GetComponent<GemManager>().OnClickGem(this.gameObject);
    }

    public void GemLand()
    {
        gemType = (GemScript.GemType)Random.Range(0, 5); 
        GetComponent<Image>().sprite = _sprites[(int)gemType];
        whenClickOver = _sprites[(int)gemType]; 
        gameObject.name = $"{gemType} Gem";
        whenClick = _spritesWhenClick[(int)gemType]; 
        
    }

    public void Update()
    {
        Down(); 
    }

    private void Down()
    {
        Vector2 vec2 = new Vector2(0,0);
        bool vecS = false;
        foreach (var gem in bs.GemDict)
        {
            if (gem.Key.x == gemPos.x && gem.Key.y < gemPos.y)
            {
                if (gem.Value == null && gemPos.y != 0)
                {   
                    Debug.Log(gameObject.name);
                    times++; 
                    //GameObject A = Instantiate(gameObject);
                    //A.GetComponent<RectTransform>().anchoredPosition = vec;
                    //gemPos = new Vector2();
                    pastGemVec.Enqueue(gem.Key);
                }
            }
            
        }

        if (times != 0)
        {
            while (times > 0)
            {
                bs.GemDict[gemPos] = null;
                Vector2 vec = gameObject.GetComponent<RectTransform>().anchoredPosition;

                if (vec.y - 30 >= 0)
                {
                    vec.y -= 30;
                }

                if (gemPos.y - 1 > -1)
                {
                    if (bs.GemDict[new Vector2(gemPos.x, gemPos.y - 1)] == null)
                    {
                        //Debug.Log(gemPos.y);
                        gemPos.y -= 1; 
                        //Debug.Log(gemPos.y);
                        gameObject.GetComponent<RectTransform>().anchoredPosition = vec; 
                        bs.GemDict[pastGemVec.Dequeue()] = null;
                        bs.GemDict[gemPos] = gameObject; 
                    }
                    else
                    {
                        bs.GemDict[gemPos] = gameObject; 
                    }
                }
                
                times--;
            }

            times = 0;
        }
    }
}

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

    public enum GemType
    {
        Fire = 0,
        Water = 1,
        Land = 2,
        Grass = 3,
        Elec = 4
    }

    [SerializeField] private List<Sprite> _sprites;

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
        gameObject.name = $"{gemType} Gem"; 
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
                if (gem.Value == null)
                {   
                    Debug.Log(gameObject.name);
                    Vector2 vec = gameObject.GetComponent<RectTransform>().anchoredPosition;
                    if(vec.y - 30 >= 0)
                        vec.y -= 30;
                    
                    gameObject.GetComponent<RectTransform>().anchoredPosition = vec;
                    gemPos.y = gemPos.y - 1; 
                    //GameObject A = Instantiate(gameObject);
                    //A.GetComponent<RectTransform>().anchoredPosition = vec;
                    //gemPos = new Vector2();
                    vecS = true;
                    vec2 = gem.Key; 
                }
            }
            
        }
        
        if (vecS)
        {
            bs.GemDict.Remove(vec2);
            //bs.GemDict.Remove(gemPos); 
            bs.GemDict[gemPos] = null;
            if (gemPos.y == 0)
            {
                
            }
            bs.GemDict.Add(vec2 , gameObject);
            //Destroy(gameObject);
            vecS = false; 
        }
    }
}

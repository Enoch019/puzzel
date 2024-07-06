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
    }

    public void Click()
    {
        transform.parent.GetComponent<GemManager>().OnClickGem(this.gameObject);
    }

    public void GemLand()
    {
        gemType = (GemScript.GemType)Random.Range(0, 5); 
        GetComponent<Image>().sprite = _sprites[(int)gemType];
        gameObject.name = $"{gemType} Gem"; 
    }
}

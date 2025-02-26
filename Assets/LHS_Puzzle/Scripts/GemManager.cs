using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GemManager : MonoBehaviour
{
    private BoardScript Board;
    private List<GameObject> roat = new List<GameObject>(5);
    public List<List<GemScript>> GemQ = new List<List<GemScript>>(); 
    
    public void Awake()
    {
        //Board = GameObject.Find("Board").GetComponent<BoardScript>();
        
    }

    public void OnClickGem(GameObject go)
    {
        switch (roat.Count)
        {
            //첫번째 클릭시
            case 0:
                roat.Add(go);
                go.GetComponent<Image>().sprite = go.GetComponent<GemScript>().whenClick; 
                break;
            //두번째 세번째 네번째 
            case int n when (n == 2 || n == 3 || n == 1):
                if (CheckOut(go.GetComponent<GemScript>()))
                {
                    //한칸 차이나는걸 확인후 추가 
                    go.GetComponent<Image>().sprite = go.GetComponent<GemScript>().whenClick; 
                    roat.Add(go);
                }
                else
                {
                    //첫번째 클릭으로 간주
                    ClearTheGems();
                    roat.Clear(); 
                    go.GetComponent<Image>().sprite = go.GetComponent<GemScript>().whenClick; 
                    roat.Add(go);
                }
                break;
            //다섯번째
            case 4:
                if (CheckOut(go.GetComponent<GemScript>()))
                {
                    roat.Add(go);
                    go.GetComponent<Image>().sprite = go.GetComponent<GemScript>().whenClick; 
                    GemClear();
                }
                else
                {
                    ClearTheGems();
                    roat.Clear(); 
                    roat.Add(go);
                    go.GetComponent<Image>().sprite = go.GetComponent<GemScript>().whenClick; 
                } 
                break;
            
        }   
    }

    public void ClearTheGems()
    {
        foreach (var gems in roat)
        {
            Debug.Log("Clear");
            gems.GetComponent<Image>().sprite = gems.GetComponent<GemScript>().whenClickOver; 
        }
    }

    public bool CheckOut(GemScript go)
    {
        GemScript go2 = roat[^1].gameObject.GetComponent<GemScript>(); 
        // 'ㅡ' 과 'ㅣ' 모양이 나올 수 있게 처음 선택된 잼과 다음 선택된 잼의 pox가 x나 y 값이 같게 하는 조건이 있어야 한다.
        if(ComparInt(go.gemPos , go2.gemPos) && ((go.gemPos.x == go2.gemPos.x) && (go.gemPos.y != go2.gemPos.y) || (go.gemPos.x != go2.gemPos.x) && go.gemPos.y == go2.gemPos.y))
        {
            return true; 
        }
        else
        {
            Debug.Log("not Match Pos X Or Y");
            return false; 
        }
    }

    public void GemClear()
    {
        //잼 터트리기
        //gameObject.GetComponent<BoardScript>().CheakGem();
        Debug.Log($"GemClear {roat[0].GetComponent<GemScript>().gemType} , {roat[1].GetComponent<GemScript>().gemType} , {roat[2].GetComponent<GemScript>().gemType} , {roat[3].GetComponent<GemScript>().gemType} , {roat[4].GetComponent<GemScript>().gemType}");
        DestroyGem();
        roat.Clear();
    }

    public bool ComparInt(Vector2 A , Vector2 B)
    {
        // 왼쪽 오른쪽 왼쪽 위 오른쪽 위 
        if (A.x + 1 == B.x || A.x - 1 == B.x)
        {
            return true;
        }
        // 위 아래 
        else if (A.y + 1 == B.y || A.y - 1 == B.y)
        {
            return true;
        }
        else
        {
            return false; 
        }
    }

    public void DestroyGem()
    {
        List<GameObject> vec = new List<GameObject>(5); 
        bool[] A = new []{ true , true , true , true , true }; 
        foreach (var gam in roat)
        {
            Destroy(gam);
        }
        
        foreach (var gaem in vec)
        {
            //gaem.GetComponent<GemScript>().Trigger();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum BlockTypes
{
    NONE = 0,
    RED,
    BLUE,
    BROWN,
    GREEN,
    YELLOW
}

public class PuzzleBlock : MonoBehaviour
{
    public int row;
    public int column;
    public float moveDuration = 0.1f; // 블록이 이동하는 시간
    public float fallDistance = 10f; // 블록이 떨어지는 거리
    public bool BottomCheck = false;

    private RectTransform rectTransform;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    //void Start()
    //{
    //    rectTransform = GetComponent<RectTransform>();
    //    //Debug.Log(rectTransform.anchoredPosition);
    //}

    // 아래 블록이 있는지 체크
    public bool HasBlockBelow()
    {
        // 체크할 위치는 현재 위치에서 아래로 한 칸
        int checkRow = row - 1;

        if (checkRow < 0)
            return false; // 체크할 아래 행이 없으면 false 반환

        // 체크할 위치의 블록 오브젝트 가져오기
        PuzzleBlock belowBlock = GetBlockAtPosition(checkRow, column);

        // 아래 블록이 있으면 true 반환, 없으면 false 반환
        return (belowBlock != null);
    }

    // 아래로 떨어지는 애니메이션
    public void FallDown()
    {
        if (HasBlockBelow())
            return; // 이미 아래에 블록이 있으면 떨어지지 않음

        // 블록의 새로운 위치 계산
        Debug.Log(rectTransform);
        Vector3 newPosition = rectTransform.anchoredPosition - new Vector2(0f, fallDistance);

        // Dotween을 사용하여 애니메이션 적용
        rectTransform.DOAnchorPos(newPosition, moveDuration);
    }

    // 주어진 행과 열 위치에 있는 블록 오브젝트 가져오기
    private PuzzleBlock GetBlockAtPosition(int row, int column)
    {
        // 예시로 단순히 같은 부모에서 찾는 예시 코드
        Transform parent = transform.parent;
        foreach (Transform child in parent)
        {
            PuzzleBlock block = child.GetComponent<PuzzleBlock>();
            if (block != null && block.row == row && block.column == column)
                return block;
        }
        return null;
    }
}

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
    public float moveDuration = 0.1f; // ����� �̵��ϴ� �ð�
    public float fallDistance = 10f; // ����� �������� �Ÿ�
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

    // �Ʒ� ����� �ִ��� üũ
    public bool HasBlockBelow()
    {
        // üũ�� ��ġ�� ���� ��ġ���� �Ʒ��� �� ĭ
        int checkRow = row - 1;

        if (checkRow < 0)
            return false; // üũ�� �Ʒ� ���� ������ false ��ȯ

        // üũ�� ��ġ�� ��� ������Ʈ ��������
        PuzzleBlock belowBlock = GetBlockAtPosition(checkRow, column);

        // �Ʒ� ����� ������ true ��ȯ, ������ false ��ȯ
        return (belowBlock != null);
    }

    // �Ʒ��� �������� �ִϸ��̼�
    public void FallDown()
    {
        if (HasBlockBelow())
            return; // �̹� �Ʒ��� ����� ������ �������� ����

        // ����� ���ο� ��ġ ���
        Debug.Log(rectTransform);
        Vector3 newPosition = rectTransform.anchoredPosition - new Vector2(0f, fallDistance);

        // Dotween�� ����Ͽ� �ִϸ��̼� ����
        rectTransform.DOAnchorPos(newPosition, moveDuration);
    }

    // �־��� ��� �� ��ġ�� �ִ� ��� ������Ʈ ��������
    private PuzzleBlock GetBlockAtPosition(int row, int column)
    {
        // ���÷� �ܼ��� ���� �θ𿡼� ã�� ���� �ڵ�
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

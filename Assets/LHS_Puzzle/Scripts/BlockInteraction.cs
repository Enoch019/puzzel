using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockInteraction : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private List<GameObject> selectedBlocks = new List<GameObject>(); // ���õ� ��ϵ��� ������ ����Ʈ
    private bool isDragging = false; // ���� �巡�� ������ ����

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isDragging)
        {
            GameObject block = eventData.pointerPress.gameObject;
            SelectBlock(block);
            isDragging = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            GameObject block = eventData.pointerCurrentRaycast.gameObject;
            if (block != null && !selectedBlocks.Contains(block))
            {
                SelectBlock(block);
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isDragging)
        {
            if (selectedBlocks.Count == 5)
            {
                // ����� ��ϵ��� ����
                foreach (GameObject block in selectedBlocks)
                {
                    Destroy(block);
                }
                // �߰����� ���� ���� ����
            }
            else
            {
                // ���� ���
                DeselectAllBlocks();
            }
            isDragging = false;
        }
    }

    private void SelectBlock(GameObject block)
    {
        // ���õ� ��� ����Ʈ�� �߰�
        selectedBlocks.Add(block);
        // ����� ��������Ʈ ���� �Ǵ� ȿ�� ���� ��
    }

    private void DeselectAllBlocks()
    {
        // ��� ���õ� ��ϵ� ���� ����
        foreach (GameObject block in selectedBlocks)
        {
            // ���� ������ �ʿ��� ó�� (��: ��������Ʈ ������� ����)
        }
        selectedBlocks.Clear();
    }
}

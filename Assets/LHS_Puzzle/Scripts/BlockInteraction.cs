using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockInteraction : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private List<GameObject> selectedBlocks = new List<GameObject>(); // 선택된 블록들을 저장할 리스트
    private bool isDragging = false; // 현재 드래그 중인지 여부

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
                // 연결된 블록들을 삭제
                foreach (GameObject block in selectedBlocks)
                {
                    Destroy(block);
                }
                // 추가적인 게임 로직 삽입
            }
            else
            {
                // 선택 취소
                DeselectAllBlocks();
            }
            isDragging = false;
        }
    }

    private void SelectBlock(GameObject block)
    {
        // 선택된 블록 리스트에 추가
        selectedBlocks.Add(block);
        // 블록의 스프라이트 변경 또는 효과 적용 등
    }

    private void DeselectAllBlocks()
    {
        // 모든 선택된 블록들 선택 해제
        foreach (GameObject block in selectedBlocks)
        {
            // 선택 해제에 필요한 처리 (예: 스프라이트 원래대로 복구)
        }
        selectedBlocks.Clear();
    }
}

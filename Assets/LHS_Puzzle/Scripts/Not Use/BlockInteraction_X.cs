using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BlockInteraction_X : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Image blockImage; // 블록의 이미지를 표시하는 UI 이미지
    public Sprite pressedSprite; // 눌렸을 때의 스프라이트
    public Sprite connectedSprite; // 연결된 상태의 스프라이트
    public int maxConnections = 5; // 최대 연결 개수

    private UIBlockSpawner blockSpawner;
    private RectTransform rectTransform;
    private bool isDragging = false;
    private List<GameObject> selectedBlocks = new List<GameObject>(); // 연결된 블록들
    private List<Sprite> originalSprites = new List<Sprite>(); // 원래 스프라이트 저장 리스트

    void Start()
    {
        blockSpawner = GetComponentInParent<UIBlockSpawner>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        SelectBlock(gameObject, pressedSprite);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging && selectedBlocks.Count < maxConnections)
        {
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out localPoint);
            foreach (Transform child in rectTransform.parent)
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(child.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera))
                {
                    if (!selectedBlocks.Contains(child.gameObject))
                    {
                        SelectBlock(child.gameObject, connectedSprite);
                        break;
                    }
                }
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;

        // **블록 제거 및 빈 공간 채우기**
        List<Vector2> removedPositions = new List<Vector2>();
        foreach (GameObject block in selectedBlocks)
        {
            removedPositions.Add(block.GetComponent<RectTransform>().anchoredPosition);
            Destroy(block);
        }

        StartCoroutine(FillEmptySpaces(removedPositions));

        // **원래 스프라이트로 되돌림**
        for (int i = 0; i < selectedBlocks.Count; i++)
        {
            selectedBlocks[i].GetComponent<Image>().sprite = originalSprites[i];
        }

        selectedBlocks.Clear();
        originalSprites.Clear();
    }

    private void SelectBlock(GameObject block, Sprite newSprite)
    {
        selectedBlocks.Add(block);
        Image blockImage = block.GetComponent<Image>();
        originalSprites.Add(blockImage.sprite); // **원래 스프라이트 저장**
        blockImage.sprite = newSprite;
    }

    private IEnumerator FillEmptySpaces(List<Vector2> removedPositions)
    {
        // **위치 기준 정렬**
        removedPositions.Sort((a, b) => b.y.CompareTo(a.y));

        foreach (Vector2 pos in removedPositions)
        {
            List<Transform> blocksAbove = new List<Transform>();

            foreach (Transform child in rectTransform.parent)
            {
                if (child.GetComponent<RectTransform>().anchoredPosition.y > pos.y &&
                    Mathf.Approximately(child.GetComponent<RectTransform>().anchoredPosition.x, pos.x))
                {
                    blocksAbove.Add(child);
                }
            }

            blocksAbove.Sort((a, b) => b.GetComponent<RectTransform>().anchoredPosition.y.CompareTo(a.GetComponent<RectTransform>().anchoredPosition.y));

            foreach (Transform block in blocksAbove)
            {
                yield return StartCoroutine(MoveBlockDown(block, rectTransform.sizeDelta.y));
            }

            // **최상단에 새 블록 스폰**
            Vector2 newPos = new Vector2(pos.x, pos.y + rectTransform.sizeDelta.y * blocksAbove.Count);
            blockSpawner.SpawnRandomBlockAtPosition(newPos);
        }
    }

    private IEnumerator MoveBlockDown(Transform block, float distance)
    {
        float elapsedTime = 0f;
        float duration = 0.5f; // **애니메이션 시간**
        Vector2 startPos = block.GetComponent<RectTransform>().anchoredPosition;
        Vector2 endPos = startPos - new Vector2(0, distance);

        while (elapsedTime < duration)
        {
            block.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(startPos, endPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        block.GetComponent<RectTransform>().anchoredPosition = endPos;
    }
}
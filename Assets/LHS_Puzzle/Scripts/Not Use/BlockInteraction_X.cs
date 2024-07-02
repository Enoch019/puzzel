using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BlockInteraction_X : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Image blockImage; // ����� �̹����� ǥ���ϴ� UI �̹���
    public Sprite pressedSprite; // ������ ���� ��������Ʈ
    public Sprite connectedSprite; // ����� ������ ��������Ʈ
    public int maxConnections = 5; // �ִ� ���� ����

    private UIBlockSpawner blockSpawner;
    private RectTransform rectTransform;
    private bool isDragging = false;
    private List<GameObject> selectedBlocks = new List<GameObject>(); // ����� ��ϵ�
    private List<Sprite> originalSprites = new List<Sprite>(); // ���� ��������Ʈ ���� ����Ʈ

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

        // **��� ���� �� �� ���� ä���**
        List<Vector2> removedPositions = new List<Vector2>();
        foreach (GameObject block in selectedBlocks)
        {
            removedPositions.Add(block.GetComponent<RectTransform>().anchoredPosition);
            Destroy(block);
        }

        StartCoroutine(FillEmptySpaces(removedPositions));

        // **���� ��������Ʈ�� �ǵ���**
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
        originalSprites.Add(blockImage.sprite); // **���� ��������Ʈ ����**
        blockImage.sprite = newSprite;
    }

    private IEnumerator FillEmptySpaces(List<Vector2> removedPositions)
    {
        // **��ġ ���� ����**
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

            // **�ֻ�ܿ� �� ��� ����**
            Vector2 newPos = new Vector2(pos.x, pos.y + rectTransform.sizeDelta.y * blocksAbove.Count);
            blockSpawner.SpawnRandomBlockAtPosition(newPos);
        }
    }

    private IEnumerator MoveBlockDown(Transform block, float distance)
    {
        float elapsedTime = 0f;
        float duration = 0.5f; // **�ִϸ��̼� �ð�**
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
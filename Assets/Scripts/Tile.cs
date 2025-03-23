using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerClickHandler
{
    Vector2Int gridPosition;
    public Vector2Int GridPosition => gridPosition;
   
    RectTransform rectTransform;
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void Init(Vector2Int position)
    {
        gridPosition = position;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.instance.TryMoveTile(this);
    }
    public IEnumerator AnimateMove(Vector2 targetPosition, float duration = 0.2f)
    {
        Vector2 startPosition = rectTransform.anchoredPosition;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, elapsedTime/duration);
            yield return null;
        }
        rectTransform.anchoredPosition = targetPosition; ;
    }
    public void SetGridPos(Vector2Int newpos)
    {
        gridPosition = newpos;
    }
}

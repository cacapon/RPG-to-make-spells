using UnityEngine;
using UnityEngine.EventSystems;

public class HoldEreaDragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private PieceCommand pieceCommand;
    [SerializeField] private float distance = 40.0f;
    private Vector2 prevPos;
    public void OnBeginDrag(PointerEventData eventData)
    {
        prevPos = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.position.x - prevPos.x > distance)
        {
            prevPos.x = eventData.position.x;
            pieceCommand.MoveRight();
        }
        if (eventData.position.x - prevPos.x < -distance)
        {
            prevPos.x = eventData.position.x;
            pieceCommand.MoveLeft();

        }
        if (eventData.position.y - prevPos.y > distance)
        {
            prevPos.y = eventData.position.y;
            pieceCommand.MoveUp();

        }
        if (eventData.position.y - prevPos.y < -distance)
        {
            prevPos.y = eventData.position.y;
            pieceCommand.MoveDown();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }
}
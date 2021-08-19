using UnityEngine;
using UnityEngine.EventSystems;

public class HoldEreaTouchEvent : MonoBehaviour, IDragHandler, IBeginDragHandler, IPointerDownHandler,IPointerUpHandler
{
    [SerializeField] private PieceCommand pieceCommand;
    [SerializeField] private float distance = 40.0f;
    [SerializeField] private float taptime;
    private Vector2 prevPos;

    private float timer = 0f;
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

    public void OnPointerDown(PointerEventData eventData)
    {
        timer = Time.time;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(Time.time - timer < taptime)
        {
            Debug.Log("taped");
            pieceCommand.Put();
        }
        timer = 0f;
    }
}
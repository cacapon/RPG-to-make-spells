using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class StageEreaTouchEvent : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    [SerializeField] private PieceCommand pieceCommand;
    [SerializeField] private float taptime;
    private Vector2Int pos;

    private float begintaptime = 0f;

    private bool istaped = false;

    private void Awake() {
        pos = new Vector2Int();
        pos.y = Convert.ToInt32(gameObject.name.Substring(0,1));
        pos.x = Convert.ToInt32(gameObject.name.Substring(1,1));
    }

    private void Update() {
        if( istaped && Time.time - begintaptime >= taptime)
        {
            pieceCommand.Holdon(pos);
            istaped = false;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        istaped =true;
        begintaptime = Time.time;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        istaped = false;
    }
}
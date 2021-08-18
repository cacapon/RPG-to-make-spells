using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RuneData : MonoBehaviour,IPointerClickHandler
{
    // Start is called before the first frame update

    Rune Runedata;
    Piece Piecedata;

    private PieceCommand pieceCommand;

    public void Init(Rune runeInstance, Piece pieceInstance)
    {
        Runedata = runeInstance;
        Piecedata = pieceInstance;
        pieceCommand = GetComponentInParent<PieceCommand>();
    }

    public Piece GetPieceData()
    {
        return Piecedata;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        pieceCommand.BringFromInventory(Runedata,Piecedata);
    }
}

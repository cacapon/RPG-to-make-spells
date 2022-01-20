using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TestPieceEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private eTile myTile;
    private Sprite[] tiles;

    private Image myImage;

    public void OnPointerEnter(PointerEventData eventData)
    {
        //myTile = eTile.YELLOW_PLUS;
        //myImage.sprite = tiles[((int)myTile)];

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // myTile = eTile.None;
        // myImage.sprite = tiles[((int)myTile)];
    }

    // Start is called before the first frame update
    void Start()
    {
        myTile = eTile.None;
        myImage = GetComponent<Image>();
        tiles = Resources.LoadAll<Sprite>("TestData/test_bookedit_inbentory/Tiles16x16");
    }

}

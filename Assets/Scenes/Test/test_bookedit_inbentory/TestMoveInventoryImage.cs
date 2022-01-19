using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestMoveInventoryImage : MonoBehaviour
{
    private Image myImage;
    private eTile Mytyle;
    private Vector2Int[] myShape;

    private void Awake() {
        myImage = this.GetComponent<Image>();
    }

    public void SetData(Sprite sprite, InventoryItem item)
    {
        myImage.sprite = sprite;
        Mytyle = item.tile;
        myShape = item.shape;
    }

}

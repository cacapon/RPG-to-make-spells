using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestMoveInventoryImage : MonoBehaviour
{
    private InventoryItem myItem;
    private Image myImage;
    public InventoryItem MyItem { get => myItem; }

    private void Awake()
    {
        myImage = this.GetComponent<Image>();
    }

    public void SetData(Sprite sprite, InventoryItem item)
    {
        myImage.sprite = sprite;
        myItem = item;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestShowMultiSprite : MonoBehaviour
{
    private Sprite[] MiniParts;

    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        MiniParts =  Resources.LoadAll<Sprite>("TestData/test_bookedit_inbentory/miniparts");
        image = GetComponent<Image>();


        image.sprite = MiniParts[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

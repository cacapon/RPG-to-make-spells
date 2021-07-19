using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdventureScript : MonoBehaviour
{
    [Multiline(8)] [SerializeField] private List<string> Quotes;
    [SerializeField] private List<Sprite> PortraitImages;
    [Multiline(2)] [SerializeField] private List<string> Names;

    [SerializeField] private List<Sprite> BackGrounds;
    [SerializeField] private List<AudioClip> BGMList;
    [SerializeField] private List<AudioClip> SEList;

    [SerializeField] private AudioSource BGM;
    [SerializeField] private AudioSource SE;

    [SerializeField] private Image PortraitImage;
    [SerializeField] private Text Name;
    [SerializeField] private Text Quote;

    private List<(Sprite, string, string)> data;
    private int count = 0;

    private void Start()
    {
        //　自分　  こんにちは
        //　先生　  おはようじゃの
        //　アニキ  おっす
        //　じぶん　終わり
        //とやる場合には？
        SetData();
        Next();
    }

    private void SetData()
    {
        data = new List<(Sprite, string, string)>();

        data.Add((PortraitImages[0], Names[0], Quotes[0]));
        data.Add((PortraitImages[1], Names[1], Quotes[1]));
        data.Add((PortraitImages[2], Names[2], Quotes[2]));
        data.Add((PortraitImages[0], Names[0], Quotes[3]));
    }

    public void Next()
    {
        if(count >= data.Count)
        {
            return;
        }

        PortraitImage.sprite    = data[count].Item1;
        Name.text               = data[count].Item2;
        Quote.text              = data[count].Item3;
        count++;
    }
}

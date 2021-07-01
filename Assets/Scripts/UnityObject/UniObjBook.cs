using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniObjBook: MonoBehaviour,ITap,IFlick
{
    [SerializeField]
    private PlayerMng PMng;

    [SerializeField]
    private int NowPage = 0;

    private Magic[] book;

    private void Awake()
    {
        book = PMng.PData.book;
    }

    private void UseMagic()
    {
        Debug.Log(book[NowPage].name + "を唱えた");
    }

    public void Turn(int n)
    {
        if (n < 0 && NowPage >= 1){
            NowPage += n;
        }
        else if (n > 0 && NowPage <= book.Length)
        {
            NowPage += n;
        }
    }

    public void Tap()
    {
        UseMagic();
    }

    public void LeftFlick()
    {
        Turn(1);
    }

    public void RightFlick()
    {
        Turn(-1);
    }
}
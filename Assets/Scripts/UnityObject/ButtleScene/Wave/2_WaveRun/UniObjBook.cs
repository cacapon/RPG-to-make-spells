using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniObjBook: MonoBehaviour,ITap,IFlick
{
    [SerializeField]
    private Animator PageTurn;

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
        PMng.Attack(book[NowPage]);
    }

    public void Turn(int n)
    {
        if (n < 0 && NowPage >= 1){
            PageTurn.SetTrigger("BookPrev");
            NowPage += n;
        }
        else if (n > 0 && NowPage < book.Length -1)
        {
            PageTurn.SetTrigger("BookNext");
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

    private void OnAnimatorMove()
    {
        PageTurn.ResetTrigger("BookNext");
        PageTurn.ResetTrigger("BookPrev");
    }
}
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

    [SerializeField]
    private SoundEffect SE;

    [SerializeField]
    private List<AudioClip> SEList;

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
            PageTurnSE();
            PageTurn.SetTrigger("BookPrev");
            NowPage += n;
        }
        else if (n > 0 && NowPage < book.Length -1)
        {
            PageTurnSE();
            PageTurn.SetTrigger("BookNext");
            NowPage += n;
        }
    }

    private void PageTurnSE()
    {
        SE.PlayOneShot(SEList[0]); //ページめくり
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
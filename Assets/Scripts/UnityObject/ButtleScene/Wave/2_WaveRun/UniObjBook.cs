using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UniObjBook: MonoBehaviour,ITap,IFlick
{
    [SerializeField] private Animator PageTurn;
    [SerializeField] Dataset dataset;
    [SerializeField] private PlayerMng PMng;
    [SerializeField] private int NowPage = 0;
    [SerializeField] private Text TMagicName;
    [SerializeField] private Text TSpendMP;
    [SerializeField] private SoundEffect SE;

    private List<Magic> book;

    private void Awake()
    {
        book = dataset.Book;
        ShowMagicData();
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
        else if (n > 0 && NowPage < book.Count -1)
        {
            PageTurnSE();
            PageTurn.SetTrigger("BookNext");
            NowPage += n;
        }
        ShowMagicData();
    }

    private void ShowMagicData() {
        TMagicName.text = book[NowPage].name;
        TSpendMP.text = "● " + book[NowPage].SpendMP.ToString();
    }

    private void PageTurnSE()
    {
        SE.PlayOneShot(SoundEffect.eSEName.PAGETURN);
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
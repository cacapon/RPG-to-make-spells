using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniObjBook: MonoBehaviour
{
    //一階層下にページオブジェクトがあるBookObjectを想定したオブジェクトに対し、操作するクラスになります。
    //ページの切替、表示ページの魔法の使用を役割としています。

    [SerializeField]
    private GameObject TargetBook = default;

    [SerializeField]
    private int NowPage = 0;

    private Book book;


    private void Awake()
    {
        book = new Book(maxPage:TargetBook.transform.childCount - 1, firstPage:NowPage);

        //最初のページだけ表示します。
        for (int i = 0; i < TargetBook.transform.childCount; i++)
        {
            TargetBook.transform.GetChild(i).gameObject.SetActive(false);
        }

        TargetBook.transform.GetChild(NowPage).gameObject.SetActive(true);
    }

    public void UseMagic()
    {
        //表示中のページの魔法を使います。
        book.UseMagic(name:TargetBook.transform.GetChild(book.NowPage).gameObject.name);
    }

    public void Turn(bool isNext) 
    {
        //ページを切り替えます。
        TargetBook.transform.GetChild(book.NowPage).gameObject.SetActive(false);
        book.Turn(isNext: isNext);
        TargetBook.transform.GetChild(book.NowPage).gameObject.SetActive(true);
    }
}

public class Book 
{
    public int NowPage { get => _nowPage; set => _nowPage = value; }
 
    private int _nowPage;
    private readonly int MaxPage;


    public Book(int maxPage,int firstPage)
    {
        MaxPage = maxPage;
        _nowPage = PageRangeCheck(val:firstPage, min:0, max:MaxPage); //範囲: 0 <= x <= maxPage
    }

    private int PageRangeCheck(int val,int min,int max)
    {
        if (min <= val && val <= max)
        {
            return val;
        }
        else
        {
            //最初のページが範囲外の場合は例外を投げます。
            throw new ArgumentOutOfRangeException($"firstPage range : {min} <= val <= {max}. But firstPage is {val}");
        }
    }

    public int Turn(bool isNext)
    {
        //ページ移動した場合に範囲外になる場合は現在のページを返す
        if (( isNext && _nowPage == MaxPage) ||
            (!isNext && _nowPage == 0      ))
        {
            return _nowPage;
        }

        if (isNext) 
        {
            return _nowPage++;
        }
        else
        {
            return _nowPage--;
        }
    }

    public void UseMagic(string name)
    {
        // TODO:今はログを出すだけ、後で実装する
        Debug.Log($"{name}の魔法を使用します。");
    }

}
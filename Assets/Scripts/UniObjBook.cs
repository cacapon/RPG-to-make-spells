using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniObjBook: MonoBehaviour
{
    //��K�w���Ƀy�[�W�I�u�W�F�N�g������BookObject��z�肵���I�u�W�F�N�g�ɑ΂��A���삷��N���X�ɂȂ�܂��B
    //�y�[�W�̐ؑցA�\���y�[�W�̖��@�̎g�p������Ƃ��Ă��܂��B

    [SerializeField]
    private GameObject TargetBook = default;

    [SerializeField]
    private int NowPage = 0;

    private Book book;


    private void Awake()
    {
        book = new Book(maxPage:TargetBook.transform.childCount - 1, firstPage:NowPage);

        //�ŏ��̃y�[�W�����\�����܂��B
        for (int i = 0; i < TargetBook.transform.childCount; i++)
        {
            TargetBook.transform.GetChild(i).gameObject.SetActive(false);
        }

        TargetBook.transform.GetChild(NowPage).gameObject.SetActive(true);
    }

    public void UseMagic()
    {
        //�\�����̃y�[�W�̖��@���g���܂��B
        book.UseMagic(name:TargetBook.transform.GetChild(book.NowPage).gameObject.name);
    }

    public void Turn(bool isNext) 
    {
        //�y�[�W��؂�ւ��܂��B
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
        _nowPage = PageRangeCheck(val:firstPage, min:0, max:MaxPage); //�͈�: 0 <= x <= maxPage
    }

    private int PageRangeCheck(int val,int min,int max)
    {
        if (min <= val && val <= max)
        {
            return val;
        }
        else
        {
            //�ŏ��̃y�[�W���͈͊O�̏ꍇ�͗�O�𓊂��܂��B
            throw new ArgumentOutOfRangeException($"firstPage range : {min} <= val <= {max}. But firstPage is {val}");
        }
    }

    public int Turn(bool isNext)
    {
        //�y�[�W�ړ������ꍇ�ɔ͈͊O�ɂȂ�ꍇ�͌��݂̃y�[�W��Ԃ�
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
        // TODO:���̓��O���o�������A��Ŏ�������
        Debug.Log($"{name}�̖��@���g�p���܂��B");
    }

}
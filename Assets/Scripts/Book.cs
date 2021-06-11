using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book: MonoBehaviour
{
    //�Ώۂ̖{�̃y�[�W��؂�ւ���N���X�ɂȂ�܂��B
    //�q�I�u�W�F�N�g�Ƃ��Ē�`�����y�[�W��؂�ւ��܂��B

    [SerializeField]
    private GameObject TargetBook = default;

    private int NowPage = 0;


    private void Awake()
    {
        //�C���X�y�N�^�[�Ŏw�肵�����݂̃y�[�W���y�[�W�S�̂𒴂����l�̏ꍇ
        if (NowPage >= TargetBook.transform.childCount - 1) 
        {
            Debug.LogError("�y�[�W���̎w�肪�q�I�u�W�F�N�g�𒴂��Ă��܂��B Now Page �̃C���X�y�N�^�[�l���m�F���Ă��������B");
            AppSystemManager.Quit();
        }


        //�ŏ��̃y�[�W�����\�����܂��B
        for (int i = 0; i < TargetBook.transform.childCount; i++)
        {
            TargetBook.transform.GetChild(i).gameObject.SetActive(false);
        }

        TargetBook.transform.GetChild(NowPage).gameObject.SetActive(true);
    }

    public void UseMagic()
    {
        // TODO:��Ŏ�������
        Debug.Log(TargetBook.transform.GetChild(NowPage).gameObject.name + "�y�[�W�̖��@���g�p���܂��B");
    }

    public void Turn(bool isNext) 
    {
        int afterPage = 0;
        if (isNext)
        {
            if (NowPage == TargetBook.transform.childCount - 1) { return; }
            afterPage++;
        }
        else
        {
            if (NowPage == 0) { return; }
            afterPage--;
        }

        TargetBook.transform.GetChild(NowPage).gameObject.SetActive(false);
        NowPage += afterPage;
        TargetBook.transform.GetChild(NowPage).gameObject.SetActive(true);
    }
}

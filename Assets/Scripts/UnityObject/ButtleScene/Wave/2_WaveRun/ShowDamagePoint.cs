using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDamagePoint : MonoBehaviour
{
    public List<Sprite> Numbers;

    [SerializeField]
    private Animator Animator;

    [SerializeField]
    private List<GameObject> DamageNumbers;

    private List<Image> DamageNumbersImage;
    void Awake()
    {
        DamageNumbersImage = new List<Image>();
        foreach (GameObject DamageNumber in DamageNumbers)
        {
            DamageNumbersImage.Add(DamageNumber.GetComponent<Image>());
        }
    }

    private void ActivatedDamageNumber(){
        // 全ての桁のイメージを有効にします。
        DamageNumbers[0].SetActive(true);
        DamageNumbers[1].SetActive(true);
        DamageNumbers[2].SetActive(true);
        DamageNumbers[3].SetActive(true);
    }

    public void SetDamagePoint(int damagepoint)
    {
        ActivatedDamageNumber();
        //数字を配列に変換
        char[] chnums = damagepoint.ToString().ToCharArray();

        if (chnums.Length == 4)
        {
            //数字を変える
            DamageNumbersImage[0].sprite = Numbers[(int)Char.GetNumericValue(chnums[0])];
            DamageNumbersImage[1].sprite = Numbers[(int)Char.GetNumericValue(chnums[1])];
            DamageNumbersImage[2].sprite = Numbers[(int)Char.GetNumericValue(chnums[2])];
            DamageNumbersImage[3].sprite = Numbers[(int)Char.GetNumericValue(chnums[3])];
        }

        else if (chnums.Length == 3)
        {
            //4桁目を非表示
            DamageNumbers[0].SetActive(false);

            //3,2,1桁目の数字を変える
            DamageNumbersImage[1].sprite = Numbers[(int)Char.GetNumericValue(chnums[0])];
            DamageNumbersImage[2].sprite = Numbers[(int)Char.GetNumericValue(chnums[1])];
            DamageNumbersImage[3].sprite = Numbers[(int)Char.GetNumericValue(chnums[2])];
        }
        else if (chnums.Length == 2)
        {
            //4,3桁目を非表示
            DamageNumbers[0].SetActive(false);
            DamageNumbers[1].SetActive(false);

            //2,1桁目の数字を変える
            DamageNumbersImage[2].sprite = Numbers[(int)Char.GetNumericValue(chnums[0])];
            DamageNumbersImage[3].sprite = Numbers[(int)Char.GetNumericValue(chnums[1])];
        }
        else if (chnums.Length == 1)
        {
            //4,3,2桁目を非表示
            DamageNumbers[0].SetActive(false);
            DamageNumbers[1].SetActive(false);
            DamageNumbers[2].SetActive(false);

            //1桁目を表示
            DamageNumbersImage[3].sprite = Numbers[(int)Char.GetNumericValue(chnums[0])];
        }

        Animator.SetTrigger("Damaged"); //ポップするアニメーションを実行
    }
}

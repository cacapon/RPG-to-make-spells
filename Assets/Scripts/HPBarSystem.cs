using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarSystem : MonoBehaviour
{
    [SerializeField]
    private Slider CurrentHPBar;
    [SerializeField]
    private Slider FutureHPBar;
    [SerializeField]
    private GameObject BackGround;


    public float MaxHP = 100.0f;

    public float HPChangeSpeed = 10.0f;

    private HitPoint HP;

    private void Awake()
    {
        HP = new HitPoint(MaxHP);
        CurrentHPBar.maxValue = MaxHP;
        FutureHPBar.maxValue = MaxHP;
    }

    private void Update()
    {
        if (HP.CurrentHP > HP.FutureHP)
        {
            //  É_ÉÅÅ[ÉWéû
            FutureHPBar.transform.SetAsFirstSibling();
            CurrentHPBar.transform.SetAsFirstSibling();

            FutureHPBar.GetComponentInChildren<Image>().color = new Color(0f, 255f, 0f);
            CurrentHPBar.GetComponentInChildren<Image>().color = new Color(255f, 0f, 0f);

        }
        else if (HP.CurrentHP < HP.FutureHP)
        {
            //  âÒïúéû
            CurrentHPBar.transform.SetAsFirstSibling();
            FutureHPBar.transform.SetAsFirstSibling();

            FutureHPBar.GetComponentInChildren<Image>().color = new Color(255f, 255f, 255f);
            CurrentHPBar.GetComponentInChildren<Image>().color = new Color(0f, 255f, 0f);
        }
        BackGround.transform.SetAsFirstSibling();


        HP.PersistentHP(Time.deltaTime * HPChangeSpeed);
        CurrentHPBar.value  = HP.CurrentHP;
        FutureHPBar.value = HP.FutureHP;

    }

    public void Damage(int damagePoint)
    {
        HP.ChangeHP(-damagePoint);
    }

    public void Heal(int healPoint)
    {
        HP.ChangeHP(healPoint);
    }

    public void Won()
    {
        HP.HPWhenWinning();
    }
}


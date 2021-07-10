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
    public float HPChangeSpeed = 10.0f;

    public PlayerData P;

    private HP HP;

    private void Awake()
    {
        HP = new HP(P);
        CurrentHPBar.maxValue = P.MaxHP;
        FutureHPBar.maxValue = P.MaxHP;
    }

    private void Update()
    {
        if (P.CurrentHP > P.FutureHP)
        {
            //  �_���[�W��
            FutureHPBar.transform.SetAsFirstSibling();
            CurrentHPBar.transform.SetAsFirstSibling();

            FutureHPBar.GetComponentInChildren<Image>().color = new Color(0f, 255f, 0f);
            CurrentHPBar.GetComponentInChildren<Image>().color = new Color(255f, 0f, 0f);

        }
        else if (P.CurrentHP < P.FutureHP)
        {
            //  �񕜎�
            CurrentHPBar.transform.SetAsFirstSibling();
            FutureHPBar.transform.SetAsFirstSibling();

            FutureHPBar.GetComponentInChildren<Image>().color = new Color(255f, 255f, 255f);
            CurrentHPBar.GetComponentInChildren<Image>().color = new Color(0f, 255f, 0f);
        }
        BackGround.transform.SetAsFirstSibling();


        HP.PersistentHP(Time.deltaTime * HPChangeSpeed);
        CurrentHPBar.value  = P.CurrentHP;
        FutureHPBar.value = P.FutureHP;

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


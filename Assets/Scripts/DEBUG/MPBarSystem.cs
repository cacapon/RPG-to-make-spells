using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MPBarSystem : MonoBehaviour
{
    [SerializeField]
    private Slider SliderCurrentMP;

    public PlayerData P;


    public float MPChangeSpeed = 5.0f;

    private MP MP;

    private void Awake()
    {
        MP = new MP(P);
        SliderCurrentMP.maxValue = P.MaxMP;
    }

    private void Update()
    {
        MP.ChangeMP(Time.deltaTime * MPChangeSpeed);
        SliderCurrentMP.value = P.CurrentMP;
    }

    public void SpendMP(int damagePoint)
    {
        MP.ChangeMP(-damagePoint);
    }
}

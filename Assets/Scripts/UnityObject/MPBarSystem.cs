using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MPBarSystem : MonoBehaviour
{
    [SerializeField]
    private Slider SliderCurrentMP;


    public float MaxMP = 100.0f;

    public float MPChangeSpeed = 5.0f;

    private MP MP;

    private void Awake()
    {
        MP = new MP(MaxMP);
        SliderCurrentMP.maxValue = MaxMP;
    }

    private void Update()
    {
        MP.ChangeMP(Time.deltaTime * MPChangeSpeed);
        SliderCurrentMP.value = MP.CurrentMP;
    }

    public void SpendMP(int damagePoint)
    {
        MP.ChangeMP(-damagePoint);
    }
}

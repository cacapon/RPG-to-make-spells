using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MPSystem : MonoBehaviour
{
    [SerializeField]
    private Text TextMaxMP;
    [SerializeField]
    private Text TextCurrentMP;

    public PlayerData P;

    public float MaxMP = 100.0f;

    public float MPChangeSpeed = 5.0f;

    private MP MP;

    private void Awake()
    {
        MP = new MP(P);
    }

    private void Update()
    {
        MP.ChangeMP(Time.deltaTime * MPChangeSpeed);
        TextMaxMP.text = P.MaxMP.ToString("N1");
        TextCurrentMP.text = P.CurrentMP.ToString("N1");
    }

    public void SpendMP(int damagePoint)
    {
        MP.ChangeMP(-damagePoint);
    }
}
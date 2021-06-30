using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPSystem : MonoBehaviour
{
    [SerializeField]
    private Text TextMaxHP;
    [SerializeField]
    private Text TextCurrentHP;
    [SerializeField]
    private Text TextFutureHP;

    [SerializeField]
    private PlayerMng PMng;

    private void Update()
    {
        TextMaxHP.text     = PMng.PData.MaxHP.ToString("N1");
        TextFutureHP.text  = PMng.PData.FutureHP.ToString("N1");
        TextCurrentHP.text = PMng.PData.CurrentHP.ToString("N1");
    }
}
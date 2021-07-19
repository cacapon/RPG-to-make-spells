using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPSystem : MonoBehaviour
{
    [SerializeField] private Text TextMaxHP;
    [SerializeField] private Text TextCurrentHP;
    [SerializeField] private Text TextFutureHP;

    [SerializeField] private Dataset dataset;

    private void Update()
    {
        TextMaxHP.text     = dataset.MaxHP.ToString("N1");
        TextFutureHP.text  = dataset.FutureHP.ToString("N1");
        TextCurrentHP.text = dataset.CurrentHP.ToString("N1");
    }
}
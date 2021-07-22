using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowWaveCount : MonoBehaviour
{
    [SerializeField] private Dataset dataset;
    private Text text;
    // Start is called before the first frame update
    private void OnEnable()
    {
        text = gameObject.GetComponent<Text>();
        text.text = $"WAVE {dataset.CurrentWaveCount} / {dataset.WaveCount}";
    }

}

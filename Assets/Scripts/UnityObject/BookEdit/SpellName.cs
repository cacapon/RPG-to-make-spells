using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellName : MonoBehaviour
{
    [SerializeField] Text TargetLabel;
    [SerializeField] Text PreviewTextTop;
    [SerializeField] Text PreviewTextBottom;
    [SerializeField] Text InputTextTop;
    [SerializeField] Text InputTextBottom;

    public void SetTop()
    {
        PreviewTextTop.text = InputTextTop.text;
    }
    public void SetBottom()
    {
        PreviewTextBottom.text = InputTextBottom.text;
    }

    public void Save()
    {
        TargetLabel.text = InputTextTop.text + InputTextBottom.text;
    }
}

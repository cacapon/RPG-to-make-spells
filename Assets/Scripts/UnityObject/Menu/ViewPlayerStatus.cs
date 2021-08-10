using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewPlayerStatus : MonoBehaviour
{
    [SerializeField] LoadPlayerData loadPlayerData;
    [SerializeField] private Text TxtStatus;

    // Update is called once per frame
    void Update()
    {
        TxtStatus.text =
            $"HP: {loadPlayerData.PData.InitHP.ToString()}\n" +
            $"MP: {loadPlayerData.PData.InitMP.ToString()}\n" +
            $"Gold: {loadPlayerData.PData.Gold.ToString()}";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewShop : MonoBehaviour
{
    [SerializeField] LoadPlayerData loadPlayerData;
    [SerializeField] private Text TxtStatus;

    private void OnEnable() {
        //TxtStatus = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        TxtStatus.text =
            $"Gold: {loadPlayerData.PData.Gold.ToString()}";
    }
}

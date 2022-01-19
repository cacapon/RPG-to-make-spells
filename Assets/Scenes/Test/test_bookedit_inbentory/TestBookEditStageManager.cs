using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBookEditStageManager : MonoBehaviour
{
    // TODO: 配置ステージを準備     7x7
    // TODO: 持ち上げステージを準備 7x7

    private int STAGE_SIZE = 7;
    private List<List<eTile>> SetStage;
    private List<List<eTile>> HoldStage;

    private void Awake()
    {
        InitStage();
    }
    private void InitStage()
    {
        SetStage = new List<List<eTile>>();
        HoldStage = new List<List<eTile>>();
        for (int height = 0; height < STAGE_SIZE; height++)
        {
            List<eTile> Row = new List<eTile>();
            for (int width = 0; width < STAGE_SIZE; width++)
            {
                Row.Add(eTile.None);
            }
            SetStage.Add(new List<eTile>(Row));
            HoldStage.Add(new List<eTile>(Row));
        }
    }

    private void TestShow(List<List<eTile>> stage)
    {
        foreach(var row in stage)
        {
            string show_str = "";
            foreach(var cell in row)
            {
                show_str += cell.ToString();
            }
            Debug.Log(show_str);
        }
        Debug.Log("");
    }

}

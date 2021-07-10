using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtleFlowMng : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> Flows;

    private Dictionary<eFlowName,GameObject> DictFlows;
    private GameObject NowFlow;

    public enum eFlowName
    {
        STAGESTART,
        WAVE,
        RESULT,
    }

    private void OnEnable()
    {
        MakeDictFlows();
        GoToFlow(eFlowName.STAGESTART);
    }

    private void MakeDictFlows()
    {
        DictFlows = new Dictionary<eFlowName, GameObject>();
        int i = 0;
        foreach(eFlowName key in Enum.GetValues(typeof(eFlowName)))
        {
            DictFlows.Add(key,Flows[i]);
            i++;
        }
    }

    public void GoToFlow(eFlowName name)
    {
        if (NowFlow != null)
        {
            NowFlow.SetActive(false);
        }
        NowFlow = DictFlows[name];
        NowFlow.SetActive(true);
    }

    public void GoToMenuScene()
    {
        //TODO
    }
}

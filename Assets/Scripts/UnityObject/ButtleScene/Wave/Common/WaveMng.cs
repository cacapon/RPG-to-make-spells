using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMng : MonoBehaviour
{
    private float gameSpeed = 1.0f;

    public float GameSpeed { get => gameSpeed;}

    public ButtleSceneData BSData;
    public List<GameObject> View;

    private Dictionary<eViewName,GameObject> DictView;

    public enum eViewName
    {
        STARTVIEW,
        MAINVIEW,
        PAUSEVIEW, // TODO
        CONTINUEVIEW, //TODO
        CLEARVIEW,
    }

    internal void GameOver()
    {
        DictView[eViewName.CONTINUEVIEW].SetActive(true); //現在CONTINUEVIEWはゲームオーバー画面をアタッチしています。
        gameSpeed = 0f;
    }

    internal void GameClear()
    {
        DictView[eViewName.CLEARVIEW].SetActive(true);
        gameSpeed = 0f;
    }

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        InitWaveCount();
        MakeDictView();
        WaveStart();
    }

    public void NextWave()
    {
        WaveStart();
    }

    private void InitWaveCount()
    {
        BSData.MaxWaveCount     = BSData.WaveCount;
        BSData.CurrentWaveCount = 1;
    }

    private void MakeDictView()
    {
        DictView = new Dictionary<eViewName, GameObject>();

        int i = 0;
        foreach (eViewName key in Enum.GetValues(typeof(eViewName)))
        {
            DictView.Add(key, View[i]);
            i++;
        }
    }

    public void WaveStart()
    {
        StartCoroutine(WaveStartCoroutine());
    }

    private IEnumerator WaveStartCoroutine()
    {
        //ゲームスピードを0にする
        float _ = gameSpeed;
        gameSpeed = 0.0f;

        //START画面、MAIN画面をONにする

        DictView[eViewName.STARTVIEW].SetActive(true);
        DictView[eViewName.MAINVIEW].SetActive(true);

        //三秒待機　WANT:本当はAnimationにしたい
        yield return new WaitForSeconds(3f);

        DictView[eViewName.STARTVIEW].SetActive(false);

        //ゲームスピードを元に戻す。
        gameSpeed = _;
    }

}

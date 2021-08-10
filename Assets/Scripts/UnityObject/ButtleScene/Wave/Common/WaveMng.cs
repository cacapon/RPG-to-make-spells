using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMng : MonoBehaviour
{
    private float gameSpeed = 1.0f;

    public float GameSpeed { get => gameSpeed;}

    private bool isPause = false;

    [SerializeField] private AnimationStartViewScript Animation;

    [SerializeField] Dataset dataset;
    public List<GameObject> View;

    private Dictionary<eViewName,GameObject> DictView;

    public enum eViewName
    {
        STARTVIEW,
        MAINVIEW,
        PAUSEVIEW,
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
        DictView[eViewName.CONTINUEVIEW].SetActive(false); //自分、敵の同士討ちなら勝利にする為、gameOver画面を無効にする。
        DictView[eViewName.CLEARVIEW].SetActive(true);
        gameSpeed = 0f;
    }

    internal bool Pause()
    {
        isPause = !isPause;
        if(isPause)
        {
            DictView[eViewName.PAUSEVIEW].SetActive(true);
            gameSpeed = 0f;
        }
        else
        {
            DictView[eViewName.PAUSEVIEW].SetActive(false);
            gameSpeed = 1f;
        }
        return isPause;
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

    public void IncrementWaveCount()
    {
        dataset.CurrentWaveCount++;
    }

    public bool IsButtle()
    {
        if(dataset.CurrentWaveCount <= dataset.WaveCount)
        {
            return true;
        }
        return false;
    }

    private void InitWaveCount()
    {
        dataset.CurrentWaveCount = 1;
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

        if(dataset.CurrentWaveCount != dataset.WaveCount)
        {
            Animation.NormalButtleAnimation();
        }
        else
        {
            Animation.BossButtleAnimation();
        }


        //三秒待機　WANT:本当はAnimationにしたい
        yield return new WaitForSeconds(3f);

        DictView[eViewName.STARTVIEW].SetActive(false);

        //ゲームスピードを元に戻す。
        gameSpeed = _;
    }

}

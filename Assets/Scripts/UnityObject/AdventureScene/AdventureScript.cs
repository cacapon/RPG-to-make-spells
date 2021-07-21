using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AdventureScript : MonoBehaviour
{
    [Multiline(8)] [SerializeField] private List<string> Quotes;
    [SerializeField] private List<Sprite> PortraitImages;
    [Multiline(2)] [SerializeField] private List<string> Names;

    [SerializeField] private List<Sprite> BackGrounds;
    [SerializeField] private List<AudioClip> BGMList;
    [SerializeField] private List<AudioClip> SEList;

    [SerializeField] private GameObject AnimationTile;
    [SerializeField] private Animator Animator;
    [SerializeField] private AudioSource BGM;
    [SerializeField] private AudioSource SE;


    [SerializeField] GameObject YesNoDialog;
    [SerializeField] private Image PortraitImage;
    [SerializeField] private Text Name;
    [SerializeField] private Text Quote;

    private List<(Sprite, string, string)> data;
    private int count = 0;

    [SerializeField] ButtleSceneData buttleSceneData;
    private PlayerData playerData;

    private enum BookType
    {
        Fire,
        Ice,
        Thunder,
    }

    private void Start()
    {
        SetData();
        MakePlayerData();
        Next();
    }
    private void SetData()
    {
        data = new List<(Sprite, string, string)>();

        data.Add((PortraitImages[0], Names[0], Quotes[0]));
        data.Add((PortraitImages[1], Names[1], Quotes[1]));
        data.Add((PortraitImages[0], Names[0], Quotes[2]));
    }

    public void Next()
    {
        if (count >= data.Count)
        {
            ShowYesNoDialog();
            return;
        }

        SetConvesation(data[count].Item1, data[count].Item2, data[count].Item3);
        count++;

    }

    private void SetConvesation(Sprite portraitImage, string name, string quote)
    {
        PortraitImage.sprite = portraitImage;
        Name.text = name;
        Quote.text = quote;
    }

    public void MakePlayerData()
    {
        playerData = new PlayerData();

        //ここで設定しているのは仮のデータです　後ほど変更予定。
        SetDefaultBook(BookType.Ice);

        playerData.InitHP = 100;
        playerData.InitMP = 50;
        playerData.MPSpeed = 3;
    }

    private void SetDefaultBook(BookType type)
    {
        switch (type)
        {
            case BookType.Thunder:
                playerData.book = new List<Magic>(){
                    new Magic(){
                        name = "雷おこし",
                        Type = Magic.eMagicType.DAMAGE,
                        Target = Magic.eMagicTarget.SINGLE_ENEMY,
                        Power = 30,
                        SpendMP = 20,
                        Effect = Magic.eMagicEffect.Thunder
                    },
                    new Magic(){
                        name = "裁きの雷",
                        Type = Magic.eMagicType.DAMAGE,
                        Target = Magic.eMagicTarget.ALL_ENEMY,
                        Power = 80,
                        SpendMP = 50,
                        Effect = Magic.eMagicEffect.Thunder
                    },
                    new Magic(){
                        name = "ヒール",
                        Type = Magic.eMagicType.HEAL,
                        Target = Magic.eMagicTarget.SELF,
                        Power = 50,
                        SpendMP = 10,
                        Effect = Magic.eMagicEffect.HEAL
                    },
                };
                break;
            case BookType.Fire:
                playerData.book = new List<Magic>(){
                    new Magic(){
                        name = "ファイアバレット",
                        Type = Magic.eMagicType.DAMAGE,
                        Target = Magic.eMagicTarget.SINGLE_ENEMY,
                        Power = 15,
                        SpendMP = 10,
                        Effect = Magic.eMagicEffect.FIRE_1
                    },
                    new Magic(){
                        name = "バーン　ライン",
                        Type = Magic.eMagicType.DAMAGE,
                        Target = Magic.eMagicTarget.ALL_ENEMY,
                        Power = 35,
                        SpendMP = 30,
                        Effect = Magic.eMagicEffect.FIRE_1
                    },
                    new Magic(){
                        name = "ヒール",
                        Type = Magic.eMagicType.HEAL,
                        Target = Magic.eMagicTarget.SELF,
                        Power = 50,
                        SpendMP = 10,
                        Effect = Magic.eMagicEffect.HEAL
                    },
                };
                break;
            case BookType.Ice:
                playerData.book = new List<Magic>(){
                    new Magic(){
                        name = "つらら　くずし",
                        Type = Magic.eMagicType.DAMAGE,
                        Target = Magic.eMagicTarget.SINGLE_ENEMY,
                        Power = 7,
                        SpendMP = 5,
                        Effect = Magic.eMagicEffect.Ice
                    },
                    new Magic(){
                        name = "ふぶき",
                        Type = Magic.eMagicType.DAMAGE,
                        Target = Magic.eMagicTarget.ALL_ENEMY,
                        Power = 10,
                        SpendMP = 15,
                        Effect = Magic.eMagicEffect.Ice
                    },
                    new Magic(){
                        name = "ヒール",
                        Type = Magic.eMagicType.HEAL,
                        Target = Magic.eMagicTarget.SELF,
                        Power = 50,
                        SpendMP = 10,
                        Effect = Magic.eMagicEffect.HEAL
                    },
                };
                break;
        }
    }

    public void SendNextScene()
    {
        StartCoroutine(Encounter());
    }

    private IEnumerator Encounter()
    {
        AnimationTile.SetActive(true);
        Animator.SetTrigger("Encounter");
        yield return new WaitForAnimation(Animator, 0);
        SceneManager.sceneLoaded += DataSet;
        SceneManager.LoadScene("Wave");
    }

    private void DataSet(Scene next, LoadSceneMode mode)
    {
        var dataSet = GameObject.FindWithTag("DataSet").GetComponent<Dataset>();
        dataSet.Initialize(playerData, buttleSceneData);
    }

    private void StopBGM()
    {
        BGM.Stop();
    }

    private void SEPlay()
    {
        SE.PlayOneShot(SEList[0]);
    }


    private void ShowYesNoDialog()
    {
        YesNoDialog.SetActive(true);
    }
    public void YesClick()
    {
        SendNextScene();
        YesNoDialog.SetActive(false);
    }

    public void NoClick()
    {
        count=0;
        Next();
        YesNoDialog.SetActive(false);
    }
}

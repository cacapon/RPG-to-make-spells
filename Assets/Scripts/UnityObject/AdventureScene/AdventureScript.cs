using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AdventureScript : MonoBehaviour,IFlick,ITap
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


    [SerializeField] private GameObject YesNoDialog;
    private UnityAction OkAction { get; set; }
    private UnityAction NoAction { get; set; }

    [SerializeField] private GameObject SelectDialog;

    [SerializeField] private Image BackGround;
    [SerializeField] private Image PortraitImage;
    [SerializeField] private Text Name;
    [SerializeField] private Text Quote;

    private List<(Sprite, string, string)> data;
    private int count = 0;

    private int flickcount = 0;

    [SerializeField] ButtleSceneData buttleSceneData;
    private PlayerData playerData;

    private enum BookType
    {
        Fire,
        Ice,
        Thunder,
    }

    private enum eBackGround
    {
        Classroom,
        Ground,
        Ice,
        Fire,
        Thunder,
    }

    private enum eCharactor
    {
        Narrator,
        Teacher,
        Aniki,
        AnikinoOtomo,
    }

    private Dictionary<eBackGround,Sprite> DBackGround;
    private Dictionary<eCharactor, (Sprite,string)> DCharactor;

    private void Start()
    {
        SetDictionary();
        Next();
    }

    private void SetDictionary()
    {
        DBackGround = new Dictionary<eBackGround, Sprite>()
        {
            {eBackGround.Classroom, BackGrounds[0]},
            {eBackGround.Ground,    BackGrounds[1]},
            {eBackGround.Ice,       BackGrounds[2]},
            {eBackGround.Fire,      BackGrounds[3]},
            {eBackGround.Thunder,   BackGrounds[4]},
        };

        DCharactor = new Dictionary<eCharactor, (Sprite, string)>()
        {
            {eCharactor.Narrator,       (PortraitImages[0],Names[0])},
            {eCharactor.Teacher,        (PortraitImages[1],Names[1])},
            {eCharactor.Aniki,          (PortraitImages[2],Names[2])},
            {eCharactor.AnikinoOtomo,   (PortraitImages[3],Names[3])},
        };
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
        switch(count)
        {
            case 0:
                BackGround.sprite = DBackGround[eBackGround.Ground];
                SetConvesation( DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[count]);
                break;
            case 1:
                SetConvesation( DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[count]);
                break;
            case 2:
                SetConvesation( DCharactor[eCharactor.Teacher].Item1,
                                DCharactor[eCharactor.Teacher].Item2,
                                Quotes[count]);
                break;
            case 3:
                SetConvesation( DCharactor[eCharactor.Teacher].Item1,
                                DCharactor[eCharactor.Teacher].Item2,
                                Quotes[count]);
                break;
            case 4:
                SetConvesation( DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[count]);
                break;
            case 5:
                BackGround.sprite = DBackGround[eBackGround.Ice];
                SetConvesation( DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[count]);
                ShowSelectDialog();
                break;
            case 6:
                BackGround.sprite = DBackGround[eBackGround.Fire];
                SetConvesation( DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[count]);
                break;
            case 7:
                BackGround.sprite = DBackGround[eBackGround.Thunder];
                SetConvesation( DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[count]);
                break;
            case 8:
                SetConvesation( DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[count]);
                YesNoSetup(() => {count++; Next();},()=>{count = 4; Next();});
                ShowYesNoDialog();
                break;
            case 9:
                BackGround.sprite = DBackGround[eBackGround.Ground];
                SetConvesation( DCharactor[eCharactor.Teacher].Item1,
                                DCharactor[eCharactor.Teacher].Item2,
                                Quotes[count]);
                break;
            case 10:
                SetConvesation( DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[count]);
                break;
            case 11:
                SetConvesation( DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[count]);
                break;
            case 12:
                SetConvesation( DCharactor[eCharactor.Aniki].Item1,
                                DCharactor[eCharactor.Aniki].Item2,
                                Quotes[count]);
                break;
            case 13:
                SetConvesation( DCharactor[eCharactor.Teacher].Item1,
                                DCharactor[eCharactor.Teacher].Item2,
                                Quotes[count]);
                break;
            case 14:
                SetConvesation( DCharactor[eCharactor.Aniki].Item1,
                                DCharactor[eCharactor.Aniki].Item2,
                                Quotes[count]);
                break;
            case 15:
                SetConvesation( DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[count]);
                break;
            case 16:
                SetConvesation( DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[count]);
                break;
            case 17:
                SetConvesation( DCharactor[eCharactor.Aniki].Item1,
                                DCharactor[eCharactor.Aniki].Item2,
                                Quotes[count]);
                break;
            case 18:
                SetConvesation( DCharactor[eCharactor.Aniki].Item1,
                                DCharactor[eCharactor.Aniki].Item2,
                                Quotes[count]);
                break;
            case 19:
                SetConvesation( DCharactor[eCharactor.AnikinoOtomo].Item1,
                                DCharactor[eCharactor.AnikinoOtomo].Item2,
                                Quotes[count]);
                break;
            case 20:
                SetConvesation( DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[count]);
                break;
            case 21:
                SetConvesation( DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[count]);
                break;
            case 22:
                SetConvesation( DCharactor[eCharactor.Aniki].Item1,
                                DCharactor[eCharactor.Aniki].Item2,
                                Quotes[count]);
                break;
            case 23:
                SetConvesation( DCharactor[eCharactor.Aniki].Item1,
                                DCharactor[eCharactor.Aniki].Item2,
                                Quotes[count]);
                break;
            case 24:
                SetConvesation( DCharactor[eCharactor.Teacher].Item1,
                                DCharactor[eCharactor.Teacher].Item2,
                                Quotes[count]);
                break;
            case 25:
                SetConvesation( DCharactor[eCharactor.Teacher].Item1,
                                DCharactor[eCharactor.Teacher].Item2,
                                Quotes[count]);
                break;
            case 26:
            //  BackGroundにバトル画面を表示
                SetConvesation( DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[count]);
                break;
            case 27:
                SetConvesation( DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[count]);
                break;
            case 28:
                SetConvesation( DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[count]);
                break;
            case 29:
                SetConvesation( DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[count]);
                break;
            case 30:
                SetConvesation( DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[count]);
                break;
            case 31:
                SetConvesation( DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[count]);
                break;
            case 32:
                SetConvesation( DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[count]);
                break;
            case 33:
                SetConvesation( DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[count]);
                break;
            case 34:
                SetConvesation( DCharactor[eCharactor.Teacher].Item1,
                                DCharactor[eCharactor.Teacher].Item2,
                                Quotes[count]);
                YesNoSetup(() => {count = 36; Next();},()=>
                    {
                        SetConvesation( DCharactor[eCharactor.Teacher].Item1,
                                DCharactor[eCharactor.Teacher].Item2,
                                Quotes[35]);
                    }
                );
                ShowYesNoDialog();
                break;
            case 35:
                // 説明をし直す
                count = 26;
                Next();
                break;

            case 36:
                SetConvesation( DCharactor[eCharactor.Teacher].Item1,
                                DCharactor[eCharactor.Teacher].Item2,
                                Quotes[count]);
                break;

            case 37:
                SetConvesation( DCharactor[eCharactor.Aniki].Item1,
                                DCharactor[eCharactor.Aniki].Item2,
                                Quotes[count]);
                break;
            case 38:
                SetConvesation( DCharactor[eCharactor.Aniki].Item1,
                                DCharactor[eCharactor.Aniki].Item2,
                                Quotes[count]);
                break;
            case 39:
                SetConvesation( DCharactor[eCharactor.Aniki].Item1,
                                DCharactor[eCharactor.Aniki].Item2,
                                Quotes[count]);
                break;
            case 40:
                SendNextScene();
                break;
            default:
                throw new ArgumentException();
        }
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

    private void ShowSelectDialog()
    {
        SelectDialog.SetActive(true);
    }

    private void YesNoSetup(UnityAction okAction = null, UnityAction noAction = null)
    {
        OkAction = okAction;
        NoAction = noAction;
    }

    public void IncrimentCount()
    {
        count++;
    }
    public void YesClick()
    {
        OkAction?.Invoke();
        YesNoDialog.SetActive(false);
    }

    public void NoClick()
    {
        NoAction?.Invoke();
        YesNoDialog.SetActive(false);
    }

    public void LeftFlick()
    {
        flickcount++;
        count = 5 + (flickcount % 3);
        Next();
    }

    public void RightFlick()
    {
        flickcount--;
        count = 5 + (flickcount % 3);
        Next();
    }

    public void Tap()
    {
        SelectDialog.SetActive(false);
        count = 8;
        Next();
    }
}

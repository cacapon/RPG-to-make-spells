using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AdventureScript : MonoBehaviour, IFlick, ITap
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
    public int Count { get => count; set => count = value; }

    [SerializeField] private GameObject SelectDialog;

    [SerializeField] private Image BackGround;
    [SerializeField] private Image PortraitImage;
    [SerializeField] private Text Name;
    [SerializeField] private Text Quote;

    private List<(Sprite, string, string)> data;
    private int count = 0;

    private int flickcount = 0;

    [SerializeField] ButtleSceneData buttleSceneData;

    [SerializeField] GameObject TapErea;
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
        ButtleViewAll,
        ButtleViewUI,
        ButtleViewEnemy,
        ButtleViewBook,
        None,
    }

    private enum eCharactor
    {
        Narrator,
        Teacher,
        Aniki,
        AnikinoOtomo,
    }

    private Dictionary<eBackGround, Sprite> DBackGround;
    private Dictionary<eCharactor, (Sprite, string)> DCharactor;

    private void Start()
    {
        SetDictionary();
        Next();
    }

    private void SetDictionary()
    {
        DBackGround = new Dictionary<eBackGround, Sprite>()
        {
            {eBackGround.Classroom,         BackGrounds[0]},
            {eBackGround.Ground,            BackGrounds[1]},
            {eBackGround.Ice,               BackGrounds[2]},
            {eBackGround.Fire,              BackGrounds[3]},
            {eBackGround.Thunder,           BackGrounds[4]},
            {eBackGround.ButtleViewAll,     BackGrounds[5]},
            {eBackGround.ButtleViewUI,      BackGrounds[6]},
            {eBackGround.ButtleViewEnemy,   BackGrounds[7]},
            {eBackGround.ButtleViewBook,    BackGrounds[8]},
            {eBackGround.None,              BackGrounds[9]},
        };

        DCharactor = new Dictionary<eCharactor, (Sprite, string)>()
        {
            {eCharactor.Narrator,       (PortraitImages[0],Names[0])},
            {eCharactor.Teacher,        (PortraitImages[1],Names[1])},
            {eCharactor.Aniki,          (PortraitImages[2],Names[2])},
            {eCharactor.AnikinoOtomo,   (PortraitImages[3],Names[3])},
        };
    }

    public void Next()
    {
        switch (Count)
        {
            case 0:
                SetConvesation(DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[Count]);
                break;
            case 1:
                SetConvesation(DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[Count]);
                break;

            case 2:
                BackGround.sprite = DBackGround[eBackGround.Ground];
                SetConvesation(DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[Count]);
                break;
            case 3:
                SetConvesation(DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[Count]);
                break;
            case 4:
                SetConvesation(DCharactor[eCharactor.Teacher].Item1,
                                DCharactor[eCharactor.Teacher].Item2,
                                Quotes[Count]);
                break;
            case 5:
                SetConvesation(DCharactor[eCharactor.Teacher].Item1,
                                DCharactor[eCharactor.Teacher].Item2,
                                Quotes[Count]);
                break;
            case 6:
                SetConvesation(DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[Count]);
                break;
            case 7:
                BackGround.sprite = DBackGround[eBackGround.Ice];
                SetConvesation(DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[Count]);
                ShowSelectDialog();
                break;
            case 8:
                BackGround.sprite = DBackGround[eBackGround.Fire];
                SetConvesation(DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[Count]);
                break;
            case 9:
                BackGround.sprite = DBackGround[eBackGround.Thunder];
                SetConvesation(DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[Count]);
                break;
            case 10:
                SetConvesation(DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[Count]);
                YesNoSetup(
                    () =>
                    {
                        Count++;
                        MakePlayerData();
                        Next();
                    },
                    () =>
                    {
                        Count = 6;
                        Next();
                    }
                );
                ShowYesNoDialog();
                break;
            case 11:
                BackGround.sprite = DBackGround[eBackGround.Ground];
                SetConvesation(DCharactor[eCharactor.Teacher].Item1,
                                DCharactor[eCharactor.Teacher].Item2,
                                Quotes[Count]);
                break;
            case 12:
                SetConvesation(DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[Count]);
                break;
            case 13:
                SetConvesation(DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[Count]);
                break;
            case 14:
                SetConvesation(DCharactor[eCharactor.Aniki].Item1,
                                DCharactor[eCharactor.Aniki].Item2,
                                Quotes[Count]);
                break;
            case 15:
                SetConvesation(DCharactor[eCharactor.Teacher].Item1,
                                DCharactor[eCharactor.Teacher].Item2,
                                Quotes[Count]);
                break;
            case 16:
                SetConvesation(DCharactor[eCharactor.Aniki].Item1,
                                DCharactor[eCharactor.Aniki].Item2,
                                Quotes[Count]);
                break;
            case 17:
                SetConvesation(DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[Count]);
                break;
            case 18:
                SetConvesation(DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[Count]);
                break;
            case 19:
                SetConvesation(DCharactor[eCharactor.Aniki].Item1,
                                DCharactor[eCharactor.Aniki].Item2,
                                Quotes[Count]);
                break;
            case 20:
                SetConvesation(DCharactor[eCharactor.Aniki].Item1,
                                DCharactor[eCharactor.Aniki].Item2,
                                Quotes[Count]);
                break;
            case 21:
                SetConvesation(DCharactor[eCharactor.AnikinoOtomo].Item1,
                                DCharactor[eCharactor.AnikinoOtomo].Item2,
                                Quotes[Count]);
                break;
            case 22:
                SetConvesation(DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[Count]);
                break;
            case 23:
                SetConvesation(DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[Count]);
                break;
            case 24:
                SetConvesation(DCharactor[eCharactor.Aniki].Item1,
                                DCharactor[eCharactor.Aniki].Item2,
                                Quotes[Count]);
                break;
            case 25:
                SetConvesation(DCharactor[eCharactor.Aniki].Item1,
                                DCharactor[eCharactor.Aniki].Item2,
                                Quotes[Count]);
                break;
            case 26:
                SetConvesation(DCharactor[eCharactor.Teacher].Item1,
                                DCharactor[eCharactor.Teacher].Item2,
                                Quotes[Count]);
                break;
            case 27:
                SetConvesation(DCharactor[eCharactor.Teacher].Item1,
                                DCharactor[eCharactor.Teacher].Item2,
                                Quotes[Count]);
                break;
            case 28:
                BackGround.sprite = DBackGround[eBackGround.ButtleViewAll];
                SetConvesation(DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[Count]);
                break;
            case 29:
                BackGround.sprite = DBackGround[eBackGround.ButtleViewUI];
                SetConvesation(DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[Count]);
                break;
            case 30:
                SetConvesation(DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[Count]);
                break;
            case 31:
                BackGround.sprite = DBackGround[eBackGround.ButtleViewEnemy];
                SetConvesation(DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[Count]);
                break;
            case 32:
                SetConvesation(DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[Count]);
                break;
            case 33:
                BackGround.sprite = DBackGround[eBackGround.ButtleViewBook];
                SetConvesation(DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[Count]);
                break;
            case 34:
                SetConvesation(DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[Count]);
                break;
            case 35:
                SetConvesation(DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[Count]);
                break;
            case 36:
                BackGround.sprite = DBackGround[eBackGround.Ground];
                SetConvesation(DCharactor[eCharactor.Teacher].Item1,
                                DCharactor[eCharactor.Teacher].Item2,
                                Quotes[Count]);
                YesNoSetup(() => { Count = 38; Next(); }, () =>
                       {
                           SetConvesation(DCharactor[eCharactor.Teacher].Item1,
                                   DCharactor[eCharactor.Teacher].Item2,
                                   Quotes[37]);
                       }
                );
                ShowYesNoDialog();
                break;
            case 37:
                // 説明をし直す
                Count = 28;
                Next();
                break;

            case 38:
                SetConvesation(DCharactor[eCharactor.Teacher].Item1,
                                DCharactor[eCharactor.Teacher].Item2,
                                Quotes[Count]);
                break;

            case 39:
                SetConvesation(DCharactor[eCharactor.Aniki].Item1,
                                DCharactor[eCharactor.Aniki].Item2,
                                Quotes[Count]);
                break;
            case 40:
                SetConvesation(DCharactor[eCharactor.Aniki].Item1,
                                DCharactor[eCharactor.Aniki].Item2,
                                Quotes[Count]);
                break;
            case 41:
                SetConvesation(DCharactor[eCharactor.Aniki].Item1,
                                DCharactor[eCharactor.Aniki].Item2,
                                Quotes[Count]);
                TapSwitch(false);
                SendNextScene();
                break;
            case 42:
                //勝利時のセリフ
                BackGround.sprite = DBackGround[eBackGround.Ground];
                SetConvesation(DCharactor[eCharactor.Aniki].Item1,
                                DCharactor[eCharactor.Aniki].Item2,
                                Quotes[Count]);
                Count = 43;
                break;
            case 43:
                //敗北時のセリフ
                BackGround.sprite = DBackGround[eBackGround.Ground];
                SetConvesation(DCharactor[eCharactor.Aniki].Item1,
                                DCharactor[eCharactor.Aniki].Item2,
                                Quotes[Count]);
                break;
            case 44:
                SetConvesation(DCharactor[eCharactor.Teacher].Item1,
                                DCharactor[eCharactor.Teacher].Item2,
                                Quotes[Count]);
                break;
            case 45:
                SetConvesation(DCharactor[eCharactor.Teacher].Item1,
                                DCharactor[eCharactor.Teacher].Item2,
                                Quotes[Count]);
                break;
            case 46:
                SetConvesation(DCharactor[eCharactor.Aniki].Item1,
                                DCharactor[eCharactor.Aniki].Item2,
                                Quotes[Count]);
                break;
            case 47:
                SetConvesation(DCharactor[eCharactor.Teacher].Item1,
                                DCharactor[eCharactor.Teacher].Item2,
                                Quotes[Count]);
                break;
            case 48:
                SetConvesation(DCharactor[eCharactor.Teacher].Item1,
                                DCharactor[eCharactor.Teacher].Item2,
                                Quotes[Count]);
                break;
            case 49:
                SetConvesation(DCharactor[eCharactor.Teacher].Item1,
                                DCharactor[eCharactor.Teacher].Item2,
                                Quotes[Count]);
                break;
            case 50:
                BackGround.sprite = DBackGround[eBackGround.None];
                SetConvesation(DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[Count]);
                break;
            case 51:
                SetConvesation(DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[Count]);
                break;
            case 52:
                SetConvesation(DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[Count]);
                break;
            case 53:
                SetConvesation(DCharactor[eCharactor.Narrator].Item1,
                                DCharactor[eCharactor.Narrator].Item2,
                                Quotes[Count]);
                break;
            default:
                SceneManager.LoadScene("Title");
                break;
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

        switch (flickcount % 3)
        {
            case 0:
                SetDefaultBook(BookType.Ice);
                break;
            case 1:
                SetDefaultBook(BookType.Fire);
                break;
            case 2:
                SetDefaultBook(BookType.Thunder);
                break;
        }

        //ここで設定しているのは仮のデータです　後ほど変更予定。

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

    public void TapSwitch(bool sw)
    {
        TapErea.SetActive(sw);
    }
    public void SendNextScene()
    {
        StartCoroutine(Encounter());
    }

    private IEnumerator Encounter()
    {
        yield return new WaitForSeconds(1.0f);
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
        SceneManager.sceneLoaded -= DataSet;
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
        Count++;
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
        Count = 7 + (flickcount % 3);
        Next();
    }

    public void RightFlick()
    {
        flickcount--;
        Count = 7 + (flickcount % 3);
        Next();
    }

    public void Tap()
    {
        SelectDialog.SetActive(false);
        Count = 10;
        Next();
    }
}

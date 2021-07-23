using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialButtleScript : MonoBehaviour,IFlick,ITap
{
    [SerializeField] private AdventureScript adventureScript;
    [SerializeField] private TouchEvent touchEvent;
    [SerializeField] private GameObject TopMsgWindow;
    [SerializeField] private GameObject TopMsgTextObj;
    [SerializeField] private GameObject BottomMsgWindow;
    [SerializeField] private GameObject BottomMsgTextObj;

    [SerializeField] private Text TopMsg;
    [SerializeField] private Text BottomMsg;
    [SerializeField] private Text BookText;

    [SerializeField] private GameObject BookAllow;
    [SerializeField] private GameObject Enemy2Allow;

    [SerializeField] private GameObject BookLeftFlickAllow;
    [SerializeField] private GameObject MaskOtherThanBooks;
    [SerializeField] private GameObject MaskOtherThanEnemy2;
    [SerializeField] private GameObject TapErea;

    [SerializeField] private GameObject FireAnimationObj;
    [SerializeField] private GameObject HealAnimationObj;
    [SerializeField] private GameObject NumAnimationObj;

    [SerializeField] private GameObject HP100;
    [SerializeField] private GameObject HP90;

    [SerializeField] private GameObject MP50;
    [SerializeField] private GameObject MP40;
    [SerializeField] private Animator FireAnimator;
    [SerializeField] private Animator HealAnimator;

    [SerializeField] private Animator HPMPAnimator;
    [SerializeField] private Animator EnemyCountAnimator;
    [SerializeField] private Animator BookAnimator;

    [SerializeField] private GameObject Enemy1TargetIcon;
    [SerializeField] private GameObject Enemy2TargetIcon;
    [SerializeField] private GameObject Enemy3TargetIcon;

    [SerializeField] private GameObject Enemy1NumObj;
    [SerializeField] private GameObject Enemy2NumObj;
    [SerializeField] private GameObject Enemy3NumObj;
    [SerializeField] private GameObject Enemy1NumAfterObj;
    [SerializeField] private GameObject Enemy2NumAfterObj;
    [SerializeField] private GameObject Enemy3NumAfterObj;



    private List<string> Message;

    private List<Action> Flow;

    private int count = -1;

    private void OnEnable()
    {
        UseTap();
        HowtoMagic();
        Next();
    }

    public void Next()
    {
        count++;
        Flow[count]();
        Debug.Log(count);
    }

    private void HowtoMagic()
    {
        Flow = new List<Action>(){
            () =>   {
                        ShowTopMsg("ナレーターです！\n\nタップや　フリックとか\n\nでてきちゃうので");
                        Enemy1NumObj.SetActive(true);
                        Enemy1NumAfterObj.SetActive(false);
                        Enemy2NumObj.SetActive(true);
                        Enemy2NumAfterObj.SetActive(false);
                        Enemy3NumObj.SetActive(true);
                        Enemy3NumAfterObj.SetActive(false);
                        SetTarget(1);
                        BookText.text = "ファイア\nバレット\n\n●　10";
                    },
            () => ShowTopMsg("センセイの　かわりに\n\nわたしが\n\nせつめいします！"),
            () => ShowTopMsg("センセイから\n\n炎のキョーカショを\n\nかりましたので\n\nこの本で　なれてみましょう"),
            () => ShowTopMsg("【まほう　の　つかいかた】"),
            () => ShowTopMsg("さっそく\n\nまほうを使ってみましょう！"),
            () =>   {
                        ShowTopMsg("キョーカショを\n\nタップしてください");
                        ImageActivate(BookAllow, true);
                        ImageActivate(MaskOtherThanBooks, true);
                    },
            () =>   {
                        SetTarget(1);
                        ImageActivate(BookAllow, false);
                        HideMsg(TopMsgWindow,TopMsgTextObj);
                        MP(isSpend:true);
                        ImageActivate(MaskOtherThanBooks, false);
                        StartCoroutine(PlayAnimation(FireAnimationObj,FireAnimator,"Left"));
                    },
            () =>   {
                        ShowTopMsg("うまく　まほうが　\n\nつかえましたね！\n\n今のまほうは\n\nファイアバレットです");
                    },
            () => ShowTopMsg("まほうを　つかうと\n\n本にかいてある　かずだけ\n\nＭＰが　へります"),
            () => ShowBottomMsg("ＭＰは　青いまる\n\nに書いてある　かずです\n\nＭＰがないと\n\nまほうが　つかえません"),
            () => ShowBottomMsg("ところで　ＭＰは\n\nどうやって\n\n回復　するのでしょう？"),
            () => ShowBottomMsg("いちど画面を　タップしたら\n\n少し　まって　みましょう"),
            () =>   {
                        HideMsg(BottomMsgWindow,BottomMsgTextObj);
                        StartCoroutine(PlayAnimation(NumAnimationObj,HPMPAnimator,"Recovery"));
                        MP(isSpend:false);
                    },
            () => ShowTopMsg("回復しましたね！\n\nＭＰは時間で\n\n回復するのです"),
            () => ShowTopMsg("ＭＰが　回復する時間と\n\nつかうＭＰに　きをつけて\n\nまほうを　使いましょう"),
            () =>   {
                        count=-1;
                        HowtoSetTarget();
                        Next();
                    }
        };
    }

    private void HowtoSetTarget()
    {
        Flow = new List<Action>(){
            () => ShowTopMsg("【ターゲット　せってい】"),
            () => ShowTopMsg("ところで　先ほど\n\nまほうを　つかったとき\n\n左の　スライムに\n\nあたりました"),
            () => ShowTopMsg("まんなかの　スライムに\n\nまほうを　つかいたい時は\n\nどうするのでしょう？"),
            () =>   {
                        ImageActivate(Enemy2Allow, true);
                        ImageActivate(MaskOtherThanEnemy2, true);
                        ShowBottomMsg("まんなかの　スライムを\n\nタップ　してみましょう");
                    },
            () =>   {
                        ImageActivate(Enemy2Allow, false);
                        SetTarget(2);
                        ImageActivate(MaskOtherThanEnemy2, false);
                        ShowBottomMsg("みどりの　♦　が\n\nまんなかに　なりました！");
                    },
            () =>   {
                        ShowTopMsg("これで　先ほどの\n\nファイアバレットを\n\nつかって　みましょう");
                        ImageActivate(BookAllow, true);
                        ImageActivate(MaskOtherThanBooks, true);
                    },
            () =>   {
                        ImageActivate(BookAllow, false);
                        HideMsg(TopMsgWindow,TopMsgTextObj);
                        ImageActivate(MaskOtherThanBooks, false);
                        StartCoroutine(PlayAnimation(FireAnimationObj,FireAnimator,"Center"));
                        StartCoroutine(PlayAnimation(NumAnimationObj,HPMPAnimator,"Recovery"));
                        MP(isSpend:false);
                    },
            () => ShowTopMsg("まんなかの　スライムに\n\nまほうが　いきましたね！"),
            () => ShowTopMsg("このように　ねらいたい\n\n相手　がいるなら\n\n先に相手を　タップして\n\nまほうを　つかいましょう"),
            () => ShowTopMsg("ちなみに　\n\nだれも　ねらっていない\n\nばあいは　かってに\n\n左の相手　になります"),
            () =>   {
                        count=-1;
                        WhatEnemyAttack();
                        Next();
                    }

        };

    }

    private void WhatEnemyAttack()
    {
        Flow = new List<Action>(){
            () => ShowTopMsg("【あいての　こうげき】"),
            () => ShowTopMsg("あいての　スライムは\n\n時間が　たつと\n\nこうげきを　してきます"),
            () => ShowTopMsg("どのように\n\nこうげきを　してくるか\n\nみてみましょう"),
            () =>   {
                        HideMsg(TopMsgWindow,TopMsgTextObj);
                        StartCoroutine(PlayNumAnimation(EnemyCountAnimator,"Countdown"));
                        ShowBottomMsg("カウントが\n\n０になったとき");

                    },
            () =>   {
                        Enemy2NumObj.SetActive(true);
                        Enemy2NumAfterObj.SetActive(false);
                        StartCoroutine(PlayAnimation(NumAnimationObj,HPMPAnimator,"Damaged"));
                        HP(isDamaged:true);
                        ShowBottomMsg("こうげきされ\n\nあなたの　赤丸のかずが\n\nへらされます");
                    },
            () => ShowBottomMsg("この赤丸はＨＰ\n\nあなたの体力です\n\nＨＰが０　になると\n\nゲームオーバーです"),
            () => ShowBottomMsg("ＨＰが　すくなくなったら\n\n回復まほうを　つかって\n\nＨＰが　０にならないよう\n\nきをつけましょう"),
            () =>   {
                        count=-1;
                        HowtoChangeMagic();
                        Next();
                    }

        };
    }

    private void HowtoChangeMagic()
    {
        Flow = new List<Action>(){
            () => ShowTopMsg("【まほうの　かえかた】"),
            () => ShowTopMsg("さきほど\n\n回復まほうを　つかって\n\nといいましたが"),
            () => ShowTopMsg("いまは　ファイアバレット\n\nになっています"),
            () => ShowTopMsg("どうやって\n\nまほうを　かえれば\n\nよいのでしょうか？"),
            () =>   {
                        ShowTopMsg("キョーカショを\n\n左から右に\n\n素早く動かして\n\nみてください");
                        UseFlick();
                        ImageActivate(BookLeftFlickAllow, true);
                        ImageActivate(MaskOtherThanBooks, true);
                    },
            () =>   {
                        ImageActivate(BookLeftFlickAllow, false);
                        ImageActivate(MaskOtherThanBooks, false);
                        UseTap();
                        ShowTopMsg("ぺーじが　めくれ\n\nべつの　まほうに\n\nなりました");
                        StartCoroutine(PlayBookAnimation(BookAnimator,"BookNext"));
                        ImageActivate(TapErea, true);
                        BookText.text = "バーン　\nライン\n\n●　30";
                    },
            () =>   {
                        ShowTopMsg("このまほうは　バーンライン\n\nぜんたいに　こうげきできる\n\nまほうです");
                    },

            () =>   {
                        ShowTopMsg("回復まほう　ではないので\n\nもう１ページ\n\nめくってみましょう");
                        UseFlick();
                        ImageActivate(BookLeftFlickAllow, true);
                        ImageActivate(MaskOtherThanBooks, true);
                    },

            () =>   {
                        ImageActivate(BookLeftFlickAllow, false);
                        ImageActivate(MaskOtherThanBooks, false);
                        UseTap();
                        ShowTopMsg("回復まほう　ヒールに\n\nなりました");
                        StartCoroutine(PlayBookAnimation(BookAnimator,"BookNext"));
                        ImageActivate(TapErea, true);
                        BookText.text = "ヒール\n\n\n●　10";
                    },
            () =>   {
                        ShowTopMsg("ＨＰも　へってますし\n\nつかって　みましょうか");
                        ImageActivate(BookAllow, true);
                        ImageActivate(MaskOtherThanBooks, true);
                    },

            () =>   {
                        ShowBottomMsg("ＨＰを回復\n\nできました");
                        StartCoroutine(PlayAnimation(HealAnimationObj,HealAnimator,"Heal"));
                        StartCoroutine(PlayAnimation(NumAnimationObj,HPMPAnimator,"HPRecovery"));
                        HP(isDamaged:false);
                        MP(isSpend:true);
                        ImageActivate(TapErea, true);
                        ImageActivate(BookAllow, false);
                        ImageActivate(MaskOtherThanBooks, false);
                    },
            () =>   {
                        MP(isSpend:false);
                        StartCoroutine(PlayAnimation(NumAnimationObj,HPMPAnimator,"Recovery"));
                        ShowTopMsg("まえのぺーじに　もどる時は\n\n右から左に\n\n素早く\n\nうごかすと　もどせます");
                    },


            () =>   {
                        ShowTopMsg("こんかい　かりる\n\nキョーカショは　すべて\n\nひとり⇔ぜんたい⇔回復\n\nのじゅんに　なっています");
                    },
            () =>   {
                        ShowTopMsg("じょうきょうに　おうじて\n\nまほうを　つかいわけて\n\nみましょう");
                    },

            () =>   {
                        adventureScript.Count = 28;
                        adventureScript.Next();
                        count=-1;
                        this.gameObject.SetActive(false);
                    },
        };
    }

    private void UseTap()
    {
        touchEvent.UseTap = true;
        touchEvent.UseFlick = false;
    }

    private void UseFlick()
    {
        touchEvent.UseTap = false;
        touchEvent.UseFlick = true;

    }

    private IEnumerator PlayAnimation(GameObject animationObj, Animator animator, string trigger)
    {
        ImageActivate(TapErea, false);
        ImageActivate(animationObj, true);
        animator.SetTrigger(trigger);
        yield return new WaitForAnimation(animator, 0);
        ImageActivate(animationObj, false);
        ImageActivate(TapErea, true);
    }

    private IEnumerator PlayBookAnimation(Animator animator, string trigger)
    {
        ImageActivate(TapErea, false);
        animator.SetTrigger(trigger);
        yield return new WaitForAnimation(animator, 0);
        ImageActivate(TapErea, true);
    }


    private IEnumerator PlayNumAnimation(Animator animator, string trigger)
    {
        ImageActivate(TapErea, false);
        animator.SetTrigger(trigger);
        yield return new WaitForAnimation(animator, 0);
        ImageActivate(TapErea, true);
        Enemy1NumObj.SetActive(false);
        Enemy2NumObj.SetActive(false);
        Enemy3NumObj.SetActive(false);
        Enemy1NumAfterObj.SetActive(true);
        Enemy2NumAfterObj.SetActive(true);
        Enemy3NumAfterObj.SetActive(true);

    }

    private void SetTarget(int targetNum)
    {
        switch (targetNum)
        {
            case 1:
                Enemy1TargetIcon.SetActive(true);
                Enemy2TargetIcon.SetActive(false);
                Enemy3TargetIcon.SetActive(false);
                break;
            case 2:
                Enemy1TargetIcon.SetActive(false);
                Enemy2TargetIcon.SetActive(true);
                Enemy3TargetIcon.SetActive(false);
                break;
            case 3:
                Enemy1TargetIcon.SetActive(false);
                Enemy2TargetIcon.SetActive(false);
                Enemy3TargetIcon.SetActive(true);
                break;
            default:
                break;
        }
    }

    private void MP(bool isSpend)
    {
        MP50.SetActive(!isSpend);
        MP40.SetActive(isSpend);
    }

    private void HP(bool isDamaged)
    {
        HP100.SetActive(!isDamaged);
        HP90.SetActive(isDamaged);
    }


    private void ImageActivate(GameObject image, bool isShow)
    {
        image.SetActive(isShow);
    }

    private void ShowTopMsg(string text)
    {
        TopMsgWindowActivate(true);
        BottomMsgWindowActivate(false);
        TopMsg.text = text;
    }

    private void ShowBottomMsg(string text)
    {
        BottomMsgWindowActivate(true);
        TopMsgWindowActivate(false);
        BottomMsg.text = text;
    }

    private void HideMsg(GameObject MsgWindow, GameObject MsgText)
    {
        MsgText.SetActive(false);
        MsgWindow.SetActive(false);
    }

    private void TopMsgWindowActivate(bool isShow)
    {
        TopMsgWindow.SetActive(isShow);
        TopMsgTextObj.SetActive(isShow);
    }
    private void BottomMsgWindowActivate(bool isShow)
    {
        BottomMsgWindow.SetActive(isShow);
        BottomMsgTextObj.SetActive(isShow);
    }

    public void LeftFlick()
    {
        Next();
    }

    public void RightFlick()
    {
    }

    public void Tap()
    {
        Next();
    }
}

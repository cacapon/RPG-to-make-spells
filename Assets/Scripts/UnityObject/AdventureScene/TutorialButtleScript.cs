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
                        ShowTopMsg("????????????????????????\n\n?????????????????????????????????\n\n????????????????????????");
                        Enemy1NumObj.SetActive(true);
                        Enemy1NumAfterObj.SetActive(false);
                        Enemy2NumObj.SetActive(true);
                        Enemy2NumAfterObj.SetActive(false);
                        Enemy3NumObj.SetActive(true);
                        Enemy3NumAfterObj.SetActive(false);
                        SetTarget(1);
                        BookText.text = "????????????\n????????????\n\n??????10";
                    },
            () => ShowTopMsg("??????????????????????????????\n\n????????????\n\n????????????????????????"),
            () => ShowTopMsg("??????????????????\n\n???????????????????????????\n\n?????????????????????\n\n???????????????????????????????????????"),
            () => ShowTopMsg("???????????????????????????????????????"),
            () => ShowTopMsg("????????????\n\n???????????????????????????????????????"),
            () =>   {
                        ShowTopMsg("?????????????????????\n\n???????????????????????????");
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
                        ShowTopMsg("???????????????????????????\n\n????????????????????????\n\n??????????????????\n\n??????????????????????????????");
                    },
            () => ShowTopMsg("???????????????????????????\n\n????????????????????????????????????\n\n????????????????????????"),
            () => ShowBottomMsg("????????????????????????\n\n?????????????????????????????????\n\n??????????????????\n\n?????????????????????????????????"),
            () => ShowBottomMsg("????????????????????????\n\n???????????????\n\n?????????????????????????????????"),
            () => ShowBottomMsg("???????????????????????????????????????\n\n????????????????????????????????????"),
            () =>   {
                        HideMsg(BottomMsgWindow,BottomMsgTextObj);
                        StartCoroutine(PlayAnimation(NumAnimationObj,HPMPAnimator,"Recovery"));
                        MP(isSpend:false);
                    },
            () => ShowTopMsg("????????????????????????\n\n??????????????????\n\n?????????????????????"),
            () => ShowTopMsg("?????????????????????????????????\n\n????????????????????????????????????\n\n?????????????????????????????????"),
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
            () => ShowTopMsg("????????????????????????????????????"),
            () => ShowTopMsg("????????????????????????\n\n?????????????????????????????????\n\n????????????????????????\n\n??????????????????"),
            () => ShowTopMsg("?????????????????????????????????\n\n????????????????????????????????????\n\n??????????????????????????????"),
            () =>   {
                        ImageActivate(Enemy2Allow, true);
                        ImageActivate(MaskOtherThanEnemy2, true);
                        ShowBottomMsg("?????????????????????????????????\n\n?????????????????????????????????");
                    },
            () =>   {
                        ImageActivate(Enemy2Allow, false);
                        SetTarget(2);
                        ImageActivate(MaskOtherThanEnemy2, false);
                        ShowBottomMsg("????????????????????????\n\n????????????????????????????????????");
                    },
            () =>   {
                        ShowTopMsg("????????????????????????\n\n???????????????????????????\n\n??????????????????????????????");
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
            () => ShowTopMsg("?????????????????????????????????\n\n????????????????????????????????????"),
            () => ShowTopMsg("?????????????????????????????????\n\n????????????????????????\n\n?????????????????????????????????\n\n????????????????????????????????????"),
            () => ShowTopMsg("???????????????\n\n?????????????????????????????????\n\n???????????????????????????\n\n??????????????????????????????"),
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
            () => ShowTopMsg("?????????????????????????????????"),
            () => ShowTopMsg("??????????????????????????????\n\n?????????????????????\n\n?????????????????????????????????"),
            () => ShowTopMsg("???????????????\n\n?????????????????????????????????\n\n?????????????????????"),
            () =>   {
                        HideMsg(TopMsgWindow,TopMsgTextObj);
                        StartCoroutine(PlayNumAnimation(EnemyCountAnimator,"Countdown"));
                        ShowBottomMsg("???????????????\n\n?????????????????????");

                    },
            () =>   {
                        Enemy2NumObj.SetActive(true);
                        Enemy2NumAfterObj.SetActive(false);
                        StartCoroutine(PlayAnimation(NumAnimationObj,HPMPAnimator,"Damaged"));
                        HP(isDamaged:true);
                        ShowBottomMsg("??????????????????\n\n?????????????????????????????????\n\n??????????????????");
                    },
            () => ShowBottomMsg("?????????????????????\n\n????????????????????????\n\n???????????????????????????\n\n???????????????????????????"),
            () => ShowBottomMsg("????????????????????????????????????\n\n?????????????????????????????????\n\n????????????????????????????????????\n\n????????????????????????"),
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
            () => ShowTopMsg("?????????????????????????????????"),
            () => ShowTopMsg("????????????\n\n?????????????????????????????????\n\n?????????????????????"),
            () => ShowTopMsg("????????????????????????????????????\n\n?????????????????????"),
            () => ShowTopMsg("???????????????\n\n???????????????????????????\n\n???????????????????????????"),
            () =>   {
                        ShowTopMsg("?????????????????????\n\n???????????????\n\n?????????????????????\n\n??????????????????");
                        UseFlick();
                        ImageActivate(BookLeftFlickAllow, true);
                        ImageActivate(MaskOtherThanBooks, true);
                    },
            () =>   {
                        ImageActivate(BookLeftFlickAllow, false);
                        ImageActivate(MaskOtherThanBooks, false);
                        UseTap();
                        ShowTopMsg("????????????????????????\n\n????????????????????????\n\n???????????????");
                        StartCoroutine(PlayBookAnimation(BookAnimator,"BookNext"));
                        ImageActivate(TapErea, true);
                        BookText.text = "????????????\n?????????\n\n??????30";
                    },
            () =>   {
                        ShowTopMsg("???????????????????????????????????????\n\n???????????????????????????????????????\n\n???????????????");
                    },

            () =>   {
                        ShowTopMsg("????????????????????????????????????\n\n??????????????????\n\n???????????????????????????");
                        UseFlick();
                        ImageActivate(BookLeftFlickAllow, true);
                        ImageActivate(MaskOtherThanBooks, true);
                    },

            () =>   {
                        ImageActivate(BookLeftFlickAllow, false);
                        ImageActivate(MaskOtherThanBooks, false);
                        UseTap();
                        ShowTopMsg("??????????????????????????????\n\n???????????????");
                        StartCoroutine(PlayBookAnimation(BookAnimator,"BookNext"));
                        ImageActivate(TapErea, true);
                        BookText.text = "?????????\n\n\n??????10";
                    },
            () =>   {
                        ShowTopMsg("??????????????????????????????\n\n?????????????????????????????????");
                        ImageActivate(BookAllow, true);
                        ImageActivate(MaskOtherThanBooks, true);
                    },

            () =>   {
                        ShowBottomMsg("???????????????\n\n???????????????");
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
                        ShowTopMsg("???????????????????????????????????????\n\n???????????????\n\n?????????\n\n?????????????????????????????????");
                    },


            () =>   {
                        ShowTopMsg("????????????????????????\n\n?????????????????????????????????\n\n?????????????????????????????????\n\n????????????????????????????????????");
                    },
            () =>   {
                        ShowTopMsg("????????????????????????????????????\n\n?????????????????????????????????\n\n???????????????");
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

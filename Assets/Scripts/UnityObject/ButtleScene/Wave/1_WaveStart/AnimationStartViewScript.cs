using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStartViewScript : MonoBehaviour
{
    [SerializeField]
    private TouchEvent TouchTarget;

    [SerializeField]
    private BGM TargetBGM;

    [SerializeField]
    private Animator StartViewHideAnimation;

    public void NormalButtleAnimation()
    {
        StartViewHideAnimation.SetTrigger("NormalButtle");
    }

    public void BossButtleAnimation()
    {
        StartViewHideAnimation.SetTrigger("BossButtle");
    }

    public void EnableUseTap()
    {
        TouchTarget.SwitchUseTap(true);
    }

    public void DisableUseTap()
    {
        TouchTarget.SwitchUseTap(false);
    }

    public void StartBGM(int i)
    {
        TargetBGM.SetBGM(i);
    }

    public void StopBGM()
    {
        TargetBGM.StopBGM();
    }

}

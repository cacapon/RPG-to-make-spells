using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStartViewScript : MonoBehaviour
{
    [SerializeField]
    private TouchEvent Target;

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

    public void EnableUseTap(){
        Target.SwitchUseTap(true);
    }

    public void DisableUseTap(){
        Target.SwitchUseTap(false);
    }

}

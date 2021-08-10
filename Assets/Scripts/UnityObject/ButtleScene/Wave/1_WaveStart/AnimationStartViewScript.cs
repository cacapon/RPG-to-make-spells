using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStartViewScript : MonoBehaviour
{
    [SerializeField] private TouchEvent TouchTarget;

    [SerializeField] private GameObject PauseButtonMask;

    [SerializeField] private BGM BGM;

    [SerializeField] private SoundEffect SE;
    [SerializeField] private Animator StartViewHideAnimation;

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

    public void EnablePauseButton()
    {
        PauseButtonMask.SetActive(false);
    }
    public void DisablePauseButton()
    {
        PauseButtonMask.SetActive(true);
    }

    public void StartBGM(BGM.eBGMName bgmName)
    {
        BGM.Play(bgmName);
    }

    public void StopBGM()
    {
        BGM.Stop();
    }

    public void PlaySE(SoundEffect.eSEName seName)
    {
        SE.PlayOneShot(seName);
    }

}

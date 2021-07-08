using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDeadAnimation : MonoBehaviour
{
    [SerializeField] private Animator Animator;
    [SerializeField] private AudioSource audioSource;
    public void PlayAnimation()
    {
        Animator.SetTrigger("Dead"); //ポップするアニメーションを実行
    }

    public void PlaySE(){
        audioSource.Stop();
        audioSource.Play();
    }
}
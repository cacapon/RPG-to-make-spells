using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDeadAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;

    public Animator Animator { get => animator; }

    public void PlayAnimation()
    {
        animator.SetTrigger("Dead"); //ポップするアニメーションを実行
    }

    public void PlaySE(){
        audioSource.Stop();
        audioSource.Play();
    }
}
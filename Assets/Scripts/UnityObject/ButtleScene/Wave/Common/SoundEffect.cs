using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private List<AudioClip> SEList;



    public void PlayOneShot(int i)
    {
        audioSource.Stop();
        audioSource.clip = SEList[i];
        audioSource.Play();
    }
}

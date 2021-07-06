using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private List<AudioClip> BGMList;

    // Start is called before the first frame update
    void Start()
    {
        SetBGM(0);
    }

    public void StopBGM()
    {
        audioSource.Stop();
    }


    public void SetBGM(int i)
    {
        audioSource.Stop();
        audioSource.clip = BGMList[i];
        audioSource.Play();
    }

}

using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 登録しているJingleの再生、停止を担当します。
/// </summary>
public class Jingle : MonoBehaviour
{
    [SerializeField] private List<AudioClip> JingleList;
    private Dictionary<eJingleName, AudioClip> JingleDict;
    private AudioSource audioSource;

    public enum eJingleName
    {
        GAMESTART,
        GAMECLEAR,
        GAMEOVER,
    }

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        JingleDict = new Dictionary<eJingleName, AudioClip>();
        int ite = 0;
        foreach (eJingleName key in Enum.GetValues(typeof(eJingleName)))
        {
            JingleDict.Add(key, JingleList[ite]);
            ite++;
        }
    }

    public void Stop()
    {
        audioSource.Stop();
    }

    public void Play(eJingleName jingleName)
    {
        audioSource.Stop();
        audioSource.clip = JingleDict[jingleName];
        audioSource.Play();
    }
}

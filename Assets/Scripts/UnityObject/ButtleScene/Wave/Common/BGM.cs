using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 登録しているBGMの再生、停止を担当します。
/// </summary>
public class BGM : MonoBehaviour
{
    [SerializeField] private List<AudioClip> MusicList;
    private Dictionary<eBGMName, AudioClip> MusicDict;
    private AudioSource audioSource;

    public enum eBGMName
    {
        SELECT,
        BUTTLE1,
        BUTTLE2,
    }

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        MusicDict = new Dictionary<eBGMName, AudioClip>();
        int ite = 0;
        foreach (eBGMName key in Enum.GetValues(typeof(eBGMName)))
        {
            MusicDict.Add(key, MusicList[ite]);
            ite++;
        }
    }

    public void Stop()
    {
        audioSource.Stop();
    }

    public void Play(eBGMName bgmName)
    {
        audioSource.Stop();
        audioSource.clip = MusicDict[bgmName];
        audioSource.Play();
    }
}

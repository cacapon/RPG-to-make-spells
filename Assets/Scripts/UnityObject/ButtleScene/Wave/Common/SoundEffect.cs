using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 登録しているSEの再生、停止を担当します。
/// </summary>
public class SoundEffect : MonoBehaviour
{
    [SerializeField] private List<AudioClip> SEList;

    private AudioSource audioSource;
    private Dictionary<eSEName,AudioClip> SEDict;

    public enum eSEName
    {
        DAMAGE_SMALL,
        DAMAGE_LARGE,
        ALEAT,
        PAGETURN,
        SPELL,
        SPELL_FAILED,
        WALK,
        ENEMY_DEAD,
    }

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        SEDict = new Dictionary<eSEName, AudioClip>();
        int ite = 0;
        foreach (eSEName key in Enum.GetValues(typeof(eSEName)))
        {
            SEDict.Add(key, SEList[ite]);
            ite++;
        }
    }

    public void PlayOneShot(eSEName seName)
    {
        audioSource.Stop();
        audioSource.clip = SEDict[seName];
        audioSource.Play();
    }
}

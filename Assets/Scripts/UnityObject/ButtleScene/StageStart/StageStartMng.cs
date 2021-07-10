using System.Collections;
using UnityEngine;

public class StageStartMng : MonoBehaviour
{
    [SerializeField]
    private GameObject WaveScene;

    [SerializeField]
    private Jingle Jingle;

    private void Awake()
    {
        StageStart();
    }

    private void StageStart()
    {
        Jingle.Play(Jingle.eJingleName.GAMESTART);
        StartCoroutine(StargeStartCoroutine());
    }

    private IEnumerator StargeStartCoroutine()
    {
        //三秒待機　WANT:本当はAnimationにしたい
        yield return new WaitForSeconds(4.5f);

        gameObject.SetActive(false);
        WaveScene.SetActive(true);
    }

}

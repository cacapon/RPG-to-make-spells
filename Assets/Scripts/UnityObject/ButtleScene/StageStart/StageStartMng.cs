using System.Collections;
using UnityEngine;

public class StageStartMng : MonoBehaviour
{
    [SerializeField]
    private GameObject WaveScene;


    private void Awake()
    {
        StageStart();
    }

    private void StageStart()
    {
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

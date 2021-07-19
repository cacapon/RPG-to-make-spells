using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AdventureScript : MonoBehaviour
{
    [Multiline(8)] [SerializeField] private List<string> Quotes;
    [SerializeField] private List<Sprite> PortraitImages;
    [Multiline(2)] [SerializeField] private List<string> Names;

    [SerializeField] private List<Sprite> BackGrounds;
    [SerializeField] private List<AudioClip> BGMList;
    [SerializeField] private List<AudioClip> SEList;

    [SerializeField] private GameObject AnimationTile;
    [SerializeField] private Animator Animator;
    [SerializeField] private AudioSource BGM;
    [SerializeField] private AudioSource SE;

    [SerializeField] private Image PortraitImage;
    [SerializeField] private Text Name;
    [SerializeField] private Text Quote;

    private List<(Sprite, string, string)> data;
    private int count = 0;

    private void Start()
    {
        SetData();
        Next();
    }

    private void SetData()
    {
        data = new List<(Sprite, string, string)>();

        data.Add((PortraitImages[0], Names[0], Quotes[0]));
        data.Add((PortraitImages[1], Names[1], Quotes[1]));
        data.Add((PortraitImages[0], Names[0], Quotes[2]));
    }

    public void Next()
    {
        if(count >= data.Count)
        {
            SendNextScene();
            return;
        }

        PortraitImage.sprite    = data[count].Item1;
        Name.text               = data[count].Item2;
        Quote.text              = data[count].Item3;
        count++;
    }

    public void SendNextScene()
    {
        StartCoroutine(Encounter());
    }

    private IEnumerator Encounter()
    {
        AnimationTile.SetActive(true);
        Animator.SetTrigger("Encounter");
        yield return new WaitForAnimation(Animator,0);
        SceneManager.sceneLoaded += DataSet;
        SceneManager.LoadScene("test_Wave");
    }

    private void DataSet(Scene next, LoadSceneMode mode)
    {
        throw new NotImplementedException();
    }

    private void StopBGM()
    {
        BGM.Stop();
    }

    private void SEPlay()
    {
        SE.PlayOneShot(SEList[0]);
    }


}

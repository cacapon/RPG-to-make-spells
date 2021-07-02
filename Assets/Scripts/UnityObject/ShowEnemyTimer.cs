using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowEnemyTimer : MonoBehaviour
{
    public List<Sprite> Numbers;
    private Image SetNumber;

    [SerializeField]
    private UniObjEnemy Target;
    void Awake()
    {
        SetNumber = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        int index = (int)Target.AttackInterval;
        if (index >= 10){
            index = 10;
        }
        SetNumber.sprite = Numbers[index];

    }
}

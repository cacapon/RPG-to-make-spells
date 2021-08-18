using UnityEngine;
using UnityEngine.Tilemaps;

public class ShowHoldErea : MonoBehaviour
{
    [SerializeField] float flashDistance = 1.0f;
    Tilemap myTilemap;
    float time;
    bool isHide;
    private void Awake() {
        myTilemap = gameObject.GetComponent<Tilemap>();
    }

    private void Update() {
        time += Time.deltaTime;

        if(time >= flashDistance)
        {
            time = 0f;
            isHide = !isHide;
        }

        if(isHide)
        {
            myTilemap.color = new Color(255f,255f,255f,0.5f);
        }
        else
        {
            myTilemap.color = new Color(255f,255f,255f,1.0f);
        }
    }
}
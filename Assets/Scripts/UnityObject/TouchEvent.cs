using UnityEngine;
using UnityEngine.EventSystems;

public class TouchEvent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float FlickCriteriaDistance = 20.0f;
    public float FlickCriteriaDuration =  0.3f;

    [SerializeField]
    private UniObjBook targetBook;

    private Touch touch;

    private void Awake()
    {
        touch = new Touch(FlickCriteriaDuration, FlickCriteriaDistance);
    }
    private void Update()
    {
        touch.durationTimer.Countup(Time.deltaTime);
    }



    public void OnPointerDown(PointerEventData eventData)
    {
        touch.durationTimer.Start();
        touch.position.x.begin = eventData.position.x;
        touch.position.y.begin = eventData.position.y;

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        touch.durationTimer.Stop();
        touch.position.x.end = eventData.position.x;
        touch.position.y.end = eventData.position.y;

        float distanceX = touch.position.x.distance;

        if (touch.IsTap())
        {
            Debug.Log("TAP");
            targetBook.UseMagic();
        }

        if (touch.IsFlick())
        {
            if (distanceX < 0)
            {
                Debug.Log("Left Flick");
                targetBook.Turn(true);
            }

            if (distanceX > 0)
            {
                Debug.Log("Right Flick");
                targetBook.Turn(false);
            }
        }
        touch.durationTimer.Reset();
    }
}

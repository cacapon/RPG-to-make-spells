using UnityEngine;
using UnityEngine.EventSystems;

public class TouchEvent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool UseTap = true;

    public bool UseFlick = true;
    public float FlickCriteriaDistance = 20.0f;
    public float FlickCriteriaDuration =  0.3f;
    public GameObject target;

    private Touch touch;

    private void Awake()
    {

        touch = new Touch(FlickCriteriaDuration, FlickCriteriaDistance);
    }
    private void Update()
    {
        touch.durationTimer.Countup(Time.deltaTime);
    }

    private void TapCallBack(ITap instance)
    {
        instance.Tap();
    }

    private void LeftFlickCallBack(IFlick instance)
    {
        instance.LeftFlick();
    }

    private void RightFlickCallBack(IFlick instance)
    {
        instance.RightFlick();
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

        if (touch.IsTap() && UseTap)
        {
            Debug.Log("TAP");
            TapCallBack(target.GetComponent<ITap>());
        }

        if (touch.IsFlick() && UseFlick)
        {
            if (distanceX < 0)
            {
                Debug.Log("Left Flick");
                LeftFlickCallBack(target.GetComponent<IFlick>());
            }

            if (distanceX > 0)
            {
                Debug.Log("Right Flick");
                RightFlickCallBack(target.GetComponent<IFlick>());
            }
        }
        touch.durationTimer.Reset();
    }
}

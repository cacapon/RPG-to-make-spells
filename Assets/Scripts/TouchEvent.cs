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
public class Touch
{
    public class PositionPropaty
    {
        public class Position
        {
            public float begin { set; get; } = 0f;
            public float end   { set; get; } = 0f;
            public float distance { get { return end - begin; } }
        }

        public Position x = new Position();
        public Position y = new Position();
    }
    public PositionPropaty position = new PositionPropaty();
    public Timer durationTimer      = new Timer();
    private float FlickCriteriaDuration;
    private float FlickCriteriaDistance;

    public Touch(float flickCriteriaDuration,
                 float flickCriteriaDistance)
    {
        FlickCriteriaDistance = flickCriteriaDistance;
        FlickCriteriaDuration = flickCriteriaDuration;
    }

    public bool IsTap()
    {
        float distanceX = Mathf.Abs(this.position.x.distance);
        float distanceY = Mathf.Abs(this.position.y.distance);
        float duration  = this.durationTimer.Timercount;

        if (distanceX < FlickCriteriaDistance &&
            distanceY < FlickCriteriaDistance &&
            duration  < FlickCriteriaDuration)
        {
            return true;
        }

        return false;
    }

    public bool IsFlick()
    {
        float distanceX = Mathf.Abs(this.position.x.distance);
        float distanceY = Mathf.Abs(this.position.y.distance);
        float duration  = this.durationTimer.Timercount;

        if (distanceY > distanceX)
        {
            return false;
        }
        else
        {
            if (distanceX >= FlickCriteriaDistance &&
                duration  <  FlickCriteriaDuration)
            {
                return true;
            }

            return false;
        }
    }

}
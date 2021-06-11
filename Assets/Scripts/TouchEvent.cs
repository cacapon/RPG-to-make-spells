using UnityEngine;
using UnityEngine.EventSystems;

public class TouchEvent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float FlickCriteriaDistance = 20f;
    public float FlickCriteriaDuring = 0.3f;
    public float FlickCriteriaSpped = 300f;

    [SerializeField]
    private Book targetBook;

    private Touch touch;

    private void Awake()
    {
        touch = new Touch(FlickCriteriaDuring, FlickCriteriaDistance);
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
        float duration = touch.durationTimer.Timercount;
        float speed = Mathf.Abs(distanceX) / duration;

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
            public float end { set; get; } = 0f;

            public float distance { get { return end - begin; } }
        }

        public Position x = new Position();
        public Position y = new Position();
    }
    public PositionPropaty position = new PositionPropaty();
    public Timer durationTimer = new Timer();
    private float FlickCriteriaDuring;
    private float FlickCriteriaDistance;

    public Touch(float flickCriteriaDuring,
                 float flickCriteriaDistance)
    {
        FlickCriteriaDistance = flickCriteriaDistance;
        FlickCriteriaDuring = flickCriteriaDuring;
    }

    public bool IsTap()
    {
        float distanceX = Mathf.Abs(this.position.x.distance);
        float distanceY = Mathf.Abs(this.position.y.distance);
        float duration = this.durationTimer.Timercount;

        if (distanceX < FlickCriteriaDistance &&
            distanceY < FlickCriteriaDistance &&
            duration < FlickCriteriaDuring)
        {
            return true;
        }

        return false;
    }

    public bool IsFlick()
    {
        //���󍶉E�t���b�N�̂ݎ������Ă��܂��B
        float distanceX = Mathf.Abs(this.position.x.distance);
        float distanceY = Mathf.Abs(this.position.y.distance);
        float duration = this.durationTimer.Timercount;

        if (distanceY > distanceX)
        {
            // �㉺�̋����̕�������
            // �����_�ł�False�Ƃ��ĕԂ�
            return false;
        }
        else
        {
            // ���E�̋����̕�������

            if (distanceX >= FlickCriteriaDistance &&
                duration < FlickCriteriaDuring)
            {
                return true;
            }

            return false;
        }
    }

}

public class Timer
{
    public float Timercount { get { return _timerCount; } }

    private float _timerCount = 0f;

    private bool isTimerRun = false;

    public void Countup(float deltaTime)
    {
        if (isTimerRun)
        {
            _timerCount += deltaTime;
        }
    }

    public void Start()
    {
        isTimerRun = true;
    }
    public void Stop()
    {
        isTimerRun = false;
    }

    public void Reset()
    {
        _timerCount = 0f;
    }
}

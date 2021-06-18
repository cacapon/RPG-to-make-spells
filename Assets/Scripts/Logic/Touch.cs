using UnityEngine;

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
        float duration = this.durationTimer.Timercount;

        if (distanceX < FlickCriteriaDistance &&
            distanceY < FlickCriteriaDistance &&
            duration < FlickCriteriaDuration)
        {
            return true;
        }

        return false;
    }

    public bool IsFlick()
    {
        float distanceX = Mathf.Abs(this.position.x.distance);
        float distanceY = Mathf.Abs(this.position.y.distance);
        float duration = this.durationTimer.Timercount;

        if (distanceY > distanceX)
        {
            return false;
        }
        else
        {
            if (distanceX >= FlickCriteriaDistance &&
                duration < FlickCriteriaDuration)
            {
                return true;
            }

            return false;
        }
    }
}


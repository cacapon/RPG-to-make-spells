public class Timer
{
    public float Timercount { get { return _timerCount; } }

    public bool IsTimerRun { get => _isTimerRun; }

    private float _timerCount = 0f;

    private bool _isTimerRun = false;

    public void Countup(float deltaTime)
    {
        if (_isTimerRun)
        {
            _timerCount += deltaTime;
        }
    }

    public void Start()
    {
        _isTimerRun = true;
    }
    public void Stop()
    {
        _isTimerRun = false;
    }

    public void Reset()
    {
        _timerCount = 0f;
    }
}

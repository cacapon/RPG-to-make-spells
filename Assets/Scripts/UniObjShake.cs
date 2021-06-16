using System.Collections;
using UnityEngine;

public class UniObjShake : MonoBehaviour
{
    public float SmallShakeDuration  =  0.2f;
    public float SmallShakeMagnitude = 20.0f;
    public float LargeShakeDuration  =  1.0f;
    public float LargeShakeMagnitude = 40.0f;

    private Vector3 originalPosition;

    private void Awake()
    {
        originalPosition = this.transform.position;
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 shakePosition = originalPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            shakePosition.y = Random.Range(0f, 1f) * magnitude;

            transform.position = shakePosition;
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = originalPosition;
    }

    public void SmallShake()
    {
        StartCoroutine(Shake(SmallShakeDuration, SmallShakeMagnitude));
    }

    public void LargeShake()
    {
        StartCoroutine(Shake(LargeShakeDuration, LargeShakeMagnitude));
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniObjShake : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.position;
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
        StartCoroutine(Shake(0.2f, 10.0f));
    }

    public void LargeShake()
    {
        StartCoroutine(Shake(0.6f, 20.0f));
    }

}


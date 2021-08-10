using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnabledTapTrigger : MonoBehaviour
{
    [SerializeField] private GameObject TapTriggerObj;
    [SerializeField] private GameObject TextObj;
    [SerializeField] private float time = 1.0f;
    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(Enabled());
    }

    private IEnumerator Enabled()
    {
        TapTriggerObj.SetActive(false);
        TextObj.SetActive(false);
        yield return new WaitForSeconds(time);
        TapTriggerObj.SetActive(true);
        TextObj.SetActive(true);
    }
}

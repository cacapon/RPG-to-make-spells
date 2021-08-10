using UnityEngine;

public class DisableObject : MonoBehaviour
{
    [SerializeField] private GameObject TargetObj;

    private void OnEnable() {
        TargetObj.SetActive(false);
    }
    private void OnDisable() {
        TargetObj.SetActive(true);
    }
}

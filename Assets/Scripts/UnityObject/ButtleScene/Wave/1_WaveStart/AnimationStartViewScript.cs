using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStartViewScript : MonoBehaviour
{
    [SerializeField]
    private TouchEvent Target;

    public void EnableUseTap(){
        Target.SwitchUseTap(true);
    }

    public void DisableUseTap(){
        Target.SwitchUseTap(false);
    }

}

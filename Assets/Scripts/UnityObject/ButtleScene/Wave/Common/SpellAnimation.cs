using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellAnimation : MonoBehaviour
{
    public Animator SpellAnimator { get => spellAnimator; }
    [SerializeField] private Animator spellAnimator;


    public void PlaySoloAttackAnimation(Magic magic)
    {
        StartCoroutine(AnimationCroutine(magic));
    }

    IEnumerator AnimationCroutine(Magic magic)
    {
        spellAnimator.SetTrigger(magic.EffectToString(magic.Effect));
        yield return new WaitForAnimation(spellAnimator, 0);
    }


}

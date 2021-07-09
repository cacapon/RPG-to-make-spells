using UnityEngine;

/// <summary>
/// コルーチンにてアニメーション開始から待機に戻るまでコルーチンを止めるクラスです。
/// ※ Entry -> Idle -> [任意のアニメーション] -> Exit という構成のアニメーターを想定しています。
/// </summary>
public class WaitForAnimation : CustomYieldInstruction
{
    Animator m_animator;
    int m_idlehash = 0;
    int m_layerNo = 0;
    bool isPlay = false;

    public WaitForAnimation(Animator animator, int layerNo)
    {
        Init(animator, layerNo, animator.GetCurrentAnimatorStateInfo(layerNo).fullPathHash);
    }

    void Init(Animator animator, int layerNo, int hash)
    {
        m_layerNo = layerNo;
        m_animator = animator;
        m_idlehash = hash;

    }

    public override bool keepWaiting
    {
        get
        {
            var now_state = m_animator.GetCurrentAnimatorStateInfo(m_layerNo).fullPathHash;

            if(m_idlehash != now_state && !isPlay) //最初の待機状態からアニメーションが変わった時
            {
                isPlay = true;
                return true;
            }
            else if(m_idlehash == now_state && isPlay){ //アニメーションが待機状態に戻った時
                isPlay = false; // newで毎回作る為要らないかも
                return false;
            }
            else{
                return true;
            }
        }
    }
}

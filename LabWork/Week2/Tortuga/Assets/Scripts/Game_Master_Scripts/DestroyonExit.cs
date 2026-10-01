using UnityEngine;

public class DestroyonExit : StateMachineBehaviour
{
     override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(animator.gameObject, stateInfo.length);
        //Used as part of animations, when it plays each of its frames, it then deletes itself
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnFinish : StateMachineBehaviour 
{
  public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        ThLetter a=animator.gameObject.GetComponent<ThLetter>();
        a.Fin();
        Destroy(animator.gameObject, stateInfo.length);
    }

}

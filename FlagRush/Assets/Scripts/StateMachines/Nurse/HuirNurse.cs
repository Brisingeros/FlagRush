using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HuirNurse : StateMachineBehaviour {
    //enemigo cerca. Se esconde en el matojo mas cercano

	private Player player;
    private float elapsedTime;
    private NavMeshAgent pAI;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.SetFloat("Blend", 1.0f);
        player = animator.gameObject.GetComponent<Player>();
        pAI = player.getAgent();
		pAI.speed = player.getSpeedMax();
		pAI.ResetPath ();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (player.getHidden ()) {

			player.GetRigidbody ().freezeRotation = true;
			animator.SetFloat ("Blend", 0.0f);
			elapsedTime += Time.deltaTime;

			if (elapsedTime >= 10) {
				player.GetRigidbody ().freezeRotation = false;
				animator.SetBool ("Peligro", false);
			}

		} else {
			pAI.destination = player.findHidingPlace().transform.position;
		}
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}

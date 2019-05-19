using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RevivirNurse : StateMachineBehaviour {

	private Player player;
    private NavMeshAgent pAI;
    private float elapsedTime = 0.0f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        player = animator.gameObject.GetComponent<Player>();
        pAI = player.getAgent();
        pAI.velocity = pAI.velocity * 0.05f;
        pAI.ResetPath();

    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        elapsedTime += Time.deltaTime;

		bool aliado = animator.GetBool ("Aliado");
		if (aliado) {
			if(elapsedTime >= 2.0f)
			{
				Nurse n = player.gameObject.GetComponent<Nurse>();
				if (n.focusAlly) {
					n.focusAlly.revive ();
					n.removeAlly (n.focusAlly);
				} /*else {					//TODO: Mira esto a ver si sirve Laura
					animator.SetBool ("Aliado", false);
				}*/
				elapsedTime = 0.0f;
			}
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

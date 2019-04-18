using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuertoSniper : StateMachineBehaviour {

	private Player player;
    private float elapsedTime = 0.0f;
	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
<<<<<<< HEAD
		player = animator.gameObject.GetComponent<Player>();
		animator.SetInteger("Lives", -1);
=======
        animator.SetInteger("Lives", -1);
        player = animator.gameObject.GetComponent<Player>();
>>>>>>> 4782c658955e39dfef4f1803d8e227cf61448188
		Vector3 posPlayer = player.transform.position;
        player.generateSound();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        elapsedTime += Time.deltaTime;

        if (elapsedTime >= 60)
        {
            //TODO:
            //Instantiate(); tumba
            //Pos tumba = posPlayer;
            Destroy(player.gameObject);
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

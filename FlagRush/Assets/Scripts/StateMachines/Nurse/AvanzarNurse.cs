using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AvanzarNurse : StateMachineBehaviour {

    private Player player;
    private NavMeshAgent pAI;

	private Vector3 initDestination;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = animator.gameObject.GetComponent<Player>();
        player.setLayerAnimator(layerIndex);
        pAI = player.getAgent();
		pAI.ResetPath ();
		pAI.speed = player.getSpeedMax();
        animator.SetFloat("Running", 1.0f);
		setPath();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
		if (!pAI.hasPath)
			setPath();
    }

	public void setPath(){

		WayPoint destination = player.getObjective ();
		Bounds destBound = destination.GetComponent<CapsuleCollider> ().bounds;
		if (destBound.Intersects(player.getColliderNPC()) || destBound.Contains(player.transform.position))
        {
            WayPoint lastDest = destination;
            destination = destination.getNext();

            if(lastDest == destination)
            {
                player.getAnimator().SetFloat("Running", 0.0f);
            }
        }

        pAI.destination = destination.transform.position;
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

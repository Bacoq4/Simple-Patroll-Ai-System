using RPG.Core;
using UnityEngine;
using UnityEngine.AI;


namespace RPG.Movement
{
    public class Mover : MonoBehaviour , IAction
    {
        NavMeshAgent navMeshAgent;
        Animator animator;
        ActionScheduler actionScheduler;

        private void Update()
        {
            UpdateAnimator();
        }

        void Start()
        {
            actionScheduler = GetComponent<ActionScheduler>();
            animator = GetComponent<Animator>();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public void startMoveAction(Vector3 Destination)
        {
            actionScheduler.StartAction(this);
            moveTo(Destination);
        }

        public void moveTo(Vector3 hitPos)
        {
            navMeshAgent.SetDestination(hitPos);
            navMeshAgent.isStopped = false;
        }
        
        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }

        private void UpdateAnimator()
        {
            Vector3 Velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(Velocity);
            float zSpeed = localVelocity.z;
            animator.SetFloat("forwardSpeed", zSpeed);
        }
    }
}


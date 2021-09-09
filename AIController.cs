using RPG.Combat;
using RPG.Movement;
using UnityEngine;
using RPG.Core;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float ChaseDistance = 5f;
        [SerializeField] float SuspicionTime = 3f;

        [SerializeField] PatrolPath patrolPath;
        private Vector3 CurrentPosToMove;
        private int currentIndex;

        GameObject player;
        Fighter fighter;
        Mover mover;
        ActionScheduler actionScheduler;

        Vector3 StartLocation;
        float timeSinceLastSawPlayer = Mathf.Infinity;

        private void Awake() {

            currentIndex = 0;
            CurrentPosToMove = patrolPath.GetPositionOfIndex(currentIndex);

            actionScheduler = GetComponent<ActionScheduler>();
            player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();
            mover = GetComponent<Mover>();

            StartLocation = transform.position;
        }

        private void Update()
        {
            if(IsInDistanceOf(player) && fighter.canAttack(player))
            {
                AttackBehaviour();
            }
            else if(timeSinceLastSawPlayer < SuspicionTime)
            {
                SuspiciousBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }

            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            if (Vector3.Distance(transform.position , CurrentPosToMove) < 0.1f)
            {
                currentIndex = patrolPath.GetNextIndex(currentIndex);
                CurrentPosToMove = patrolPath.GetPositionOfIndex(currentIndex);
            }

            mover.startMoveAction(CurrentPosToMove);
        }

        private void SuspiciousBehaviour()
        {
            actionScheduler.CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            timeSinceLastSawPlayer = 0;
            fighter.Attack(player);
        }

        private bool IsInDistanceOf(GameObject player)
        {
            return Vector3.Distance(transform.position, player.transform.position) < ChaseDistance;           
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(transform.position, ChaseDistance);
        }
    }
}


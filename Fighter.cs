using UnityEngine;
using RPG.Movement;
using RPG.Core;


namespace RPG.Combat
{
    public class Fighter : MonoBehaviour , IAction
    {
        [SerializeField] float weaponRange;
        [SerializeField] float weaponDamage;
        [SerializeField] float timeBetweenAttacks = 1f;

        private float timer = 0;

        Health target;

        Mover _Mover;
        ActionScheduler actionScheduler;
        Animator animator;

        private void Awake() {
            _Mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if(target == null) return;
            if(target.IsDead()) return;
            if (!IsInRange())
            {
                _Mover.moveTo(target.transform.position);
            }
            else
            {
                _Mover.Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            if (Time.time > timer)
            {
                timer = Time.time + timeBetweenAttacks;
                
                // this will trigger Hit() event
                TriggerAttackAnim();
            }
        }

        private void ResetTimer()
        {
            timer = Time.time;
        }

        private void TriggerAttackAnim()
        {
            animator.ResetTrigger("stopAttack");
            animator.SetTrigger("attack");
        }

        // animation event
        void Hit()
        {
            if (target != null)
            {
                target.TakeDamage(weaponDamage);
            }
        }


        private bool IsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public void Attack(GameObject combatTarget)
        {
            actionScheduler.StartAction(this);

            target = combatTarget.GetComponent<Health>();
            transform.LookAt(target.transform);
        }
        public void Cancel()
        {
            target = null;
            ResetTimer();
            StopAttackAnim();
        }

        private void StopAttackAnim()
        {
            animator.ResetTrigger("attack");
            animator.SetTrigger("stopAttack");
        }

        public bool canAttack(GameObject combatTarget)
        {
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }
    }
}
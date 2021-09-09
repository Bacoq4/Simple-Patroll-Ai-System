using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction;
        
        public void StartAction(IAction Action)
        {
            if(currentAction == Action) return;
            if (currentAction != null)
            {
                currentAction.Cancel();
            }
            
            currentAction = Action;
        }

        public void CancelCurrentAction()
        {
            currentAction.Cancel();
        }

    }
}
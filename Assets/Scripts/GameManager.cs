using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

namespace StudyProject
{
    public enum ActorIndexer { LION = 0, ELEPHANT = 1, ZEBRA = 2, PARROT = 3, VOVA = 4, PANDA = 5 }

    public class GameManager : MonoBehaviour
    {
        public List<Actor> actors;
        public List<UnityEvent> actions;

        public List<float> inactiveTimeOnActions;

        public List<int> dialoguesBeforeAction;       
        
        public int currentAction = 0;

        private DialogueHandler dialogueHandler;

        private void Start()
        {
            dialogueHandler = GetComponent<DialogueHandler>();
            dialogueHandler.Play(dialoguesBeforeAction[currentAction]);
            ResetActorsState();
        }

        private void ResetActorsState()
        {
            foreach (Actor actor in actors)
            {
                actor.ChangeMood(StatePassive.DEFAULT);
            }
        }

        public void DialogueHasDone()
        {
            NextAction();
        }

        public void NextAction()
        {
            actions[currentAction].Invoke();
            dialogueHandler.SetInactive(true);
            StartCoroutine(InactiveChange(inactiveTimeOnActions[currentAction]));
            currentAction++;

            if (dialoguesBeforeAction[currentAction] > 0)
            {
                dialogueHandler.Play(dialoguesBeforeAction[currentAction]);
            }
            else 
            {
                NextAction();
            }
        }

        IEnumerator InactiveChange(float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
            dialogueHandler.SetInactive(false);
        }
    }
}

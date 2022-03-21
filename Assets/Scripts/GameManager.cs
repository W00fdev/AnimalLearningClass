using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

namespace StudyProject
{
    public enum ActorIndexer { LION = 0, ELEPHANT = 1, ZEBRA = 2, PARROT = 3, VOVA = 4, PANDA = 5 }

    public class GameManager : MonoBehaviour
    {
        [Header("Система событий")]
        public List<Actor> actors;
        public List<UnityEvent> actions;

        public List<float> inactiveTimeOnActions;
        public List<int> dialoguesBeforeAction;       
        
        public int currentAction = 0;

        private DialogueHandler _dialogueHandler;

        private void Start()
        {
            SetActorsMood();

            _dialogueHandler = GetComponent<DialogueHandler>();
            _dialogueHandler.Play(dialoguesBeforeAction[currentAction]);
        }

        public void SetActorsMood(int mood = 0)
        {
            foreach (Actor actor in actors)
            {
                actor.ChangeMood(mood);
            }
        }

        public void DialogueHasDone()
        {
            NextAction();
        }

        public void NextAction()
        {
            actions[currentAction].Invoke();
            _dialogueHandler.SetInactive(true);
            if (inactiveTimeOnActions[currentAction] > 0)
                StartCoroutine(InactiveChange(inactiveTimeOnActions[currentAction]));
            currentAction++;

            if (inactiveTimeOnActions[currentAction - 1] == -1)
                return; // infinite waiting ... wait for actions :3

            if (dialoguesBeforeAction[currentAction] > 0)
            {
                _dialogueHandler.Play(dialoguesBeforeAction[currentAction]);
            }
            else
            {
                NextAction();
            }
        }

        IEnumerator InactiveChange(float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
            _dialogueHandler.SetInactive(false);
        }
    }
}

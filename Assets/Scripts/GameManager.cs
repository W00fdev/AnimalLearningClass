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

        [Header("Выбор таблички")]

        public List<string> tabletsNames;

        private int _currentIndexTablet = 0;

        private DialogueHandler _dialogueHandler;

        private void Start()
        {
            _dialogueHandler = GetComponent<DialogueHandler>();
            _dialogueHandler.Play(dialoguesBeforeAction[currentAction]);
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
            _dialogueHandler.SetInactive(true);
            StartCoroutine(InactiveChange(inactiveTimeOnActions[currentAction]));
            currentAction++;

            float time = dialoguesBeforeAction[currentAction];

            if (time > 0)
            {
                _dialogueHandler.Play(dialoguesBeforeAction[currentAction]);
            }
            else if (time == -1)
            {
                // infinity ... do nothing  :3
            }
            else
            {
                NextAction();
            }
        }

        public bool ChooseTablet(string name)
        {
            if (tabletsNames[_currentIndexTablet] == name) 
            {
                _currentIndexTablet++;
                return true;
            }
            
            return false;
        }

        IEnumerator InactiveChange(float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
            _dialogueHandler.SetInactive(false);
        }
    }
}

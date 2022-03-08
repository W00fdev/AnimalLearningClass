using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace StudyProject
{
    public class DialogueHandler : MonoBehaviour
    {
        public List<Dialogue> dialogues;

        public Text textSpeaker;
        public Text textMessage;

        public List<string> skippedSpeakers;

        private GameManager gameManager;

        private Coroutine tickCoroutine;

        [SerializeField]
        private int currentDialogue = 0;
        [SerializeField]
        private int nextCheckpointDialogue = 10;

        [SerializeField]
        private float tickDelay = 0.1f;

        private bool isTicking = false;
        [SerializeField]
        private bool isInactive = false;

        private void Awake()
        {
            gameManager = GetComponent<GameManager>();
        }

        public void SetInactive(bool _active) 
        {
            isInactive = _active;
        }

        public void Play(int count) 
        {
            nextCheckpointDialogue = currentDialogue + count;
            PlayNext();
        }

        public void Reset()
        {
            currentDialogue = 0;
        }

        // Is invoked from mouse click in DialoguePlayNext.cs 
        public void PlayNext()
        {
            if (isInactive)
                return;

            if (nextCheckpointDialogue <= currentDialogue) 
            {
                gameManager.DialogueHasDone();
                return;
            }

            // Skip ticking and return control
            if (isTicking) 
            {
                StopCoroutine(tickCoroutine);
                isTicking = false;
                textMessage.text = dialogues[currentDialogue].dialogue;
                currentDialogue++;
                return;
            }
                
            // If an active person speaks
            if (!skippedSpeakers.Contains(dialogues[currentDialogue].speaker))
            {
                //Debug.Log(dialogues[currentDialogue].speaker);
                Actor currentActor = gameManager.actors.Find( x => x.name == dialogues[currentDialogue].speaker );
                if (currentActor != null)
                {
                    currentActor.ChangeMood(dialogues[currentDialogue].passiveState);
                    currentActor.ChangeState(dialogues[currentDialogue].activeState);
                }
            }

            textSpeaker.text = dialogues[currentDialogue].speaker;
            textMessage.text = "";
            tickCoroutine = StartCoroutine(TickText());
        }

        IEnumerator TickText() 
        {
            isTicking = true;
            string temp = "";
            foreach(char c in dialogues[currentDialogue].dialogue) 
            {
                temp += c;
                textMessage.text = temp;
                
                yield return new WaitForSeconds(tickDelay);
            }

            isTicking = false;
            currentDialogue++;
            //PlayNext();
        }
    }
}

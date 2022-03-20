using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace StudyProject
{
    public class DialogueHandler : MonoBehaviour
    {
        [Header("Список диалогов")]
        public List<Dialogue> dialogues;

        [Header("Ссылки на UI текст")]
        public Text textSpeaker;
        public Text textMessage; 

        [Header("Спикеры, не участвующие в логике программы")]
        public List<string> skippedSpeakers;

        [Header("Отладка работы")]
        [SerializeField] private float _tickDelay = 0.1f;

        [SerializeField] private int _currentDialogue = 0;
        [SerializeField] private int _nextCheckpointDialogue = 10;

        [SerializeField] private bool _isInactive = false;
        
        private GameManager _gameManager;
        private Coroutine _tickCoroutine;

        private Actor _currentSpeaker;

        private bool isTicking = false;
       
        private void Awake() => _gameManager = GetComponent<GameManager>();

        public void SetInactive(bool _active) => _isInactive = _active;
        public void Reset() => _currentDialogue = 0;

        public void Play(int count) 
        {
            _nextCheckpointDialogue = _currentDialogue + count;
            PlayNext();
        }

        // Is invoked from mouse click in DialoguePlayNext.cs 
        public void PlayNext()
        {
            if (_isInactive)
                return;

            if (_nextCheckpointDialogue <= _currentDialogue) 
            {
                _gameManager.DialogueHasDone();

                if (_currentSpeaker != null)
                {
                    _currentSpeaker.StopSpeaking();
                    _currentSpeaker = null;
                }

                return;
            }

            // Skip ticking and return control
            if (isTicking) 
            {
                StopCoroutine(_tickCoroutine);
                isTicking = false;
                textMessage.text = dialogues[_currentDialogue].dialogue;

                if (_currentSpeaker != null)
                {
                    _currentSpeaker.StopSpeaking();
                    _currentSpeaker = null;
                }

                _currentDialogue++;
                return;
            }
                
            // If an active person speaks
            if (!skippedSpeakers.Contains(dialogues[_currentDialogue].speaker))
            {
                if (_currentSpeaker != null)
                {
                    _currentSpeaker.StopSpeaking();
                    _currentSpeaker = null;
                }

                _currentSpeaker = _gameManager.actors.Find( x => x.name == dialogues[_currentDialogue].speaker );
                if (_currentSpeaker != null)
                {
                    _currentSpeaker.ChangeMood(dialogues[_currentDialogue].passiveState);

                    if (_currentSpeaker != null)
                        _currentSpeaker.StartSpeaking();
                }
            }

            textSpeaker.text = dialogues[_currentDialogue].speaker;
            textMessage.text = "";
            _tickCoroutine = StartCoroutine(TickText());
        }

        IEnumerator TickText() 
        {
            isTicking = true;
            string temp = "";
            foreach(char c in dialogues[_currentDialogue].dialogue) 
            {
                temp += c;
                textMessage.text = temp;
                
                yield return new WaitForSeconds(_tickDelay);
            }

            isTicking = false;
            _currentDialogue++;
            //PlayNext();

            if (_currentSpeaker != null)
            {
                _currentSpeaker.StopSpeaking();
                _currentSpeaker = null;
            }
        }
    }
}

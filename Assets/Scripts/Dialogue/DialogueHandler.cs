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
        public List<Message> messages;

        [Header("Ссылки на UI текст")]
        public Text textSpeaker;
        public Text textMessage; 

        [Header("Спикеры, не участвующие в логике программы")]
        public List<string> skippedSpeakers;

        [Header("Отладка работы")]
        [SerializeField] private float _tickDelay = 0.1f;

        [SerializeField] private int _currentDialogue = 0;
        [SerializeField] private int _currentMessage = 0;
        [SerializeField] private int _nextCheckpointDialogue = 10;

        [SerializeField] private bool _isInactive = false;
        
        private GameManager _gameManager;
        private Coroutine _tickCoroutine;

        private Actor _currentSpeaker;
        private Message _message;

        private bool isTicking = false;
       
        private void Awake() => _gameManager = GetComponent<GameManager>();

        public void SetInactive(bool _active) => _isInactive = _active;
        public void Reset() => _currentDialogue = 0;

        public void Play() 
        {
            _message = messages[_currentMessage];
            _nextCheckpointDialogue = _message.Speeches.Count;
            PlayNext();
        }

        // Is invoked from mouse click in DialoguePlayNext.cs 
        public void PlayNext()
        {
            if (_isInactive)
                return;

            if (_currentDialogue >= _nextCheckpointDialogue) 
            {
                if (_currentSpeaker != null)
                {
                    _currentSpeaker.StopSpeaking();
                    _currentSpeaker = null;
                }

                _currentDialogue = 0;
                _currentMessage++;
                //_message = messages[_currentMessage];

                _gameManager.DialogueHasDone();

                return;
            }

            // Skip ticking and return control
            if (isTicking) 
            {
                StopCoroutine(_tickCoroutine);
                isTicking = false;
                // textMessage.text = dialogues[_currentDialogue].dialogue;
                textMessage.text = _message.Speeches[_currentDialogue];

                if (_currentSpeaker != null)
                {
                    _currentSpeaker.StopSpeaking();
                    _currentSpeaker = null;
                }

                _currentDialogue++;
                return;
            }

            // If an active person speaks
            string speakerName = _message.Speakers[_currentDialogue];
            if (!skippedSpeakers.Contains(speakerName))
            {
                if (_currentSpeaker != null)
                {
                    _currentSpeaker.StopSpeaking();
                    _currentSpeaker = null;
                }

                _currentSpeaker = _gameManager.actors.Find( x => x.name == speakerName);
                if (_currentSpeaker != null)
                {
                    _currentSpeaker.ChangeMood(_message.Moods[_currentDialogue]);

                    if (_currentSpeaker != null)
                        _currentSpeaker.StartSpeaking();
                }
            }

            textSpeaker.text = _message.Speakers[_currentDialogue];
            textMessage.text = "";
            _tickCoroutine = StartCoroutine(TickText());
        }

        IEnumerator TickText() 
        {
            isTicking = true;
            string temp = "";
            foreach(char c in _message.Speeches[_currentDialogue]) 
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

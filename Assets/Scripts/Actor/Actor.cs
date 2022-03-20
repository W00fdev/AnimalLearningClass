using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace StudyProject
{
    // public enum StateActive { PASSIVE, SPEAKING, FLAPPING, TABLE, FIGURE }
    public enum StatePassive { DEFAULT, HAPPY, SAD, INCOMPREHESION, PROUD, ANGRY, SURPRISED, SMILING }

    public class Actor : MonoBehaviour
    {
        [Header("Анимационные спрайты")]
        public Sprite speaking1;
        public Sprite speaking2;
        public float speakingTimeTick = 0.3f;
        public bool speaking = false;

        [Header("Хлопанье")]
        public bool cloppable = false;
        public float clopTime = 1f;

        public Sprite clop1 = null;
        public Sprite clop2 = null;
        public Sprite clop3 = null;

        [Header("Состояния персонажа")]

        [Tooltip("Коллекция возможных состояний спрайтами.")]
        public List<Sprite> spriteSheet;// = new List<Sprite>();

        new public string name; 

        [SerializeField] 
        private StatePassive _mood = StatePassive.DEFAULT;
        //private StateActive _state = StateActive.PASSIVE;

        private Image _image;

        // Speaking attributes
        private Coroutine _speakingCoroutine;
        private Sprite _originSprite;
        private bool _speakingFlag = false;
        
        // Clopping attributes
        private Coroutine _cloppingCoroutine;
        private byte _cloppingIndex = 0;

        // Animal_st         ==    DEFAULT            = 0
        // Animal_fun        ==    HAPPY              = 1
        // Animal_sad        ==    SAD                = 2
        // Animal_underst    ==    INCOMPREHESION     = 3
        // Animal_sp         ==    SPEAKING           = 4
        // Animal_gor        ==    PROUD              = 5
        // Animal_ang        ==    ANGRY              = 6
        // Animal_underst    ==    SURPRISED          = 7
        // Animal_st         ==    SMILING            = 8
        
        private void Awake() => _image = GetComponent<Image>();

        // public void ChangeState(StateActive newState) => _state = newState;
        // public void ChangeState(int newState) => _state = (StateActive)newState;

        public void ChangeMood(StatePassive newMood)
        {
            _mood = newMood;
            _image.sprite = spriteSheet[(int)_mood];
        }

        // Inspector-version function.
        public void ChangeMood(int newMood)
        {
            _mood = (StatePassive)newMood;
            _image.sprite = spriteSheet[newMood];
        }

        public void StartSpeaking()
        {
            if (_speakingCoroutine != null)
            {
                StopCoroutine(_speakingCoroutine);
                _speakingCoroutine = null;
            }
            
            _originSprite = _image.sprite;
            _speakingCoroutine = StartCoroutine(SpeakCoroutine());
            speaking = true;
        }

        public void StopSpeaking()
        {
            if (_speakingCoroutine != null)
            {
                StopCoroutine(_speakingCoroutine);
                _image.sprite = _originSprite;
                speaking = false;
                _speakingCoroutine = null;
            }
        }

        IEnumerator SpeakCoroutine()
        {
            yield return new WaitForSeconds(speakingTimeTick);
            if (_speakingFlag == false)
                _image.sprite = speaking1;
            else
                _image.sprite = speaking2;

            _speakingFlag = !_speakingFlag;
        }


        public void StartClopping()
        {
            if (cloppable)
            {
                _originSprite = _image.sprite;
                _cloppingCoroutine = StartCoroutine(ClopCoroutine());
                StartCoroutine(ClopStop());
            }
        }

        public void StopClopping()
        {
            if (_cloppingCoroutine != null)
            {
                StopCoroutine(_cloppingCoroutine);
                _image.sprite = _originSprite;
                _cloppingCoroutine = null;
            }
        }

        IEnumerator ClopCoroutine()
        {
            yield return new WaitForSeconds(speakingTimeTick);
            if (_cloppingIndex == 0)
                _image.sprite = clop1;
            else if (_cloppingIndex == 1)
                _image.sprite = clop2;
            else
                _image.sprite = clop3;

            _cloppingIndex++;
            _cloppingIndex %= 3;
        }

        IEnumerator ClopStop()
        {
            yield return new WaitForSeconds(clopTime);
            StopClopping();
        }

    }
}

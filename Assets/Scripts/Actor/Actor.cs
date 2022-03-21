using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace StudyProject
{
    // public enum StateActive { PASSIVE, SPEAKING, FLAPPING, TABLE, FIGURE }
    public enum StatePassive { DEFAULT = 0, HAPPY = 1, SAD, INCOMPREHESION, PROUD, ANGRY, SURPRISED, SMILING }

    public class Actor : MonoBehaviour
    {
        [Header("Анимационные спрайты")]
        public Sprite speaking1;
        public Sprite speaking2;
        public float speakingTimeTick = 0.3f;
        public bool speaking = false;

        [Header("Хлопанье")]
        public bool clopping = false;
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

        private Image _image;

        // Speaking attributes
        private Coroutine _speakingCoroutine;
        private bool _speakingFlag = false;
        
        // Clopping attributes
        private Coroutine _cloppingCoroutine;
        private byte _cloppingIndex = 0;

        // Animal_st         ==    DEFAULT            = 0
        // Animal_fun        ==    HAPPY              = 1
        // Animal_sad        ==    SAD                = 2
        // Animal_underst    ==    INCOMPREHESION     = 3
        // Animal_gor        ==    PROUD              = 4
        // Animal_ang        ==    ANGRY              = 5
        // Animal_underst    ==    SURPRISED          = 6
        // Animal_st         ==    SMILING            = 7
        
        private void Awake() => _image = GetComponent<Image>();

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
                speaking = false;
                StopCoroutine(_speakingCoroutine);
                _speakingCoroutine = null;
            }
            
            speaking = true;
            _speakingCoroutine = StartCoroutine(SpeakCoroutine());
        }

        public void StopSpeaking()
        {
            if (_speakingCoroutine != null)
            {
                speaking = false;
                StopCoroutine(_speakingCoroutine);
                _speakingCoroutine = null;

                ChangeMood(_mood);
            }
        }

        IEnumerator SpeakCoroutine()
        {
            speaking = true;

            while (speaking)
            {
                yield return new WaitForSeconds(speakingTimeTick);
                if (_speakingFlag == false)
                    _image.sprite = speaking1;
                else
                    _image.sprite = speaking2;

                _speakingFlag = !_speakingFlag;
            }
        }


        public void StartClopping()
        {
            if (clopping)
            {
                clopping = false;
                StopCoroutine(_cloppingCoroutine);
                _cloppingCoroutine = null;
            }

            _cloppingCoroutine = StartCoroutine(ClopCoroutine());
            StartCoroutine(ClopStop());
        }

        public void StopClopping()
        {
            if (_cloppingCoroutine != null)
            {
                clopping = false;
                StopCoroutine(_cloppingCoroutine);
                _cloppingCoroutine = null;

                ChangeMood(_mood);
            }
        }

        IEnumerator ClopCoroutine()
        {
            clopping = true;

            while (clopping)
            {
                yield return new WaitForSeconds(speakingTimeTick / 2.5f);
                if (_cloppingIndex == 0)
                    _image.sprite = clop1;
                else if (_cloppingIndex == 1)
                    _image.sprite = clop2;
                else
                    _image.sprite = clop3;

                _cloppingIndex++;
                _cloppingIndex %= 3;
            }
            _cloppingIndex = 0;
        }

        IEnumerator ClopStop()
        {
            yield return new WaitForSeconds(clopTime);
            StopClopping();
        }

    }
}

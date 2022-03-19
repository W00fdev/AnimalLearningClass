using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace StudyProject
{
    // public enum StateActive { PASSIVE, SPEAKING, FLAPPING, TABLE, FIGURE }
    public enum StatePassive { DEFAULT, HAPPY, SAD, INCOMPREHESION, SPEAKING, PROUD, ANGRY, SURPRISED, SMILING }

    public class Actor : MonoBehaviour
    {
        public List<Sprite> spriteSheet;// = new List<Sprite>();

        new public string name; 

        [SerializeField] 
        private StatePassive _mood = StatePassive.DEFAULT;
        //private StateActive _state = StateActive.PASSIVE;

        private Image _image;

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

        
    }
}

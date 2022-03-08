using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace StudyProject
{
    public class Actor : MonoBehaviour
    {
        new public string name; 
        public List<Sprite> spriteSheet;// = new List<Sprite>();

        [SerializeField]
        private StatePassive _mood = StatePassive.DEFAULT;
        private StateActive _state = StateActive.PASSIVE;

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
        
        private void Awake()
        {
            _image = GetComponent<Image>();
        } 

        public void ChangeMood(StatePassive newMood)
        {
            _mood = newMood;
            _image.sprite = spriteSheet[(int)_mood];
        }

        public void ChangeState(StateActive newState) 
        {
            _state = newState;
        }

        // Inspector functions.
        public void ChangeMood(int newMood)
        {
            _mood = (StatePassive)newMood;
            _image.sprite = spriteSheet[newMood];
        }

        public void ChangeState(int newState) 
        {
            _state = (StateActive)newState;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace StudyProject
{
    public class ScoreTable : MonoBehaviour
    {
        public int Score 
        {
            get => _score;
            set 
            {
                _score = value;
                _scoreText.text = _score.ToString();
            }
        }

        [SerializeField]
        private int _score = 0;

        public Text _scoreText;

        void Start() => _scoreText.text = "0";

    }
}

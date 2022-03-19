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

        [SerializeField] private Text _scoreText;
        [SerializeField] private int _score = 0;

        void Start() => _scoreText.text = "0";
    }
}

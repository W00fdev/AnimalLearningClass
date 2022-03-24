using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace StudyProject
{
    public class ScoreTable : MonoBehaviour, IPointerClickHandler
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

        [Header("Переход в главное меню")]
        [SerializeField] private string _menuSceneName;

        void Start() => _scoreText.text = "0";

        public void OnPointerClick(PointerEventData pointerEventData) => SceneManager.LoadScene(_menuSceneName);
    }
}

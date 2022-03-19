using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace StudyProject
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject _panelRU;
        [SerializeField] private GameObject _panelENG;

        [SerializeField] private string _nextSceneName;

        public void ShowPanelENG(bool eng = true)
        {
            if (eng)
            {
                _panelRU.SetActive(false);
                _panelENG.SetActive(true);
                return;
            }

            _panelENG.SetActive(false);
            _panelRU.SetActive(true);
        }

        public void ChangeScene()
        {
            SceneManager.LoadScene(_nextSceneName);
        }
    }
}

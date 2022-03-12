using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace StudyProject
{
    public class TimerScript : MonoBehaviour
    {
        public GameManager gameManager;
        public Text timerText;

        [SerializeField]
        private int _time = 10;

        public void EnableTimer()
        {
            StartCoroutine(Timer1Sec());
        }

        IEnumerator Timer1Sec()
        {
            yield return new WaitForSeconds(1f);
            _time--;
            timerText.text = _time.ToString();
            if (_time > 0)
                StartCoroutine(Timer1Sec());
            else
                EndTime();
        }

        private void EndTime()
        {
            gameManager.NextAction();
            Debug.Log("End!");
        }
    }
}

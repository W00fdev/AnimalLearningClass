using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace StudyProject
{
    public class TimerScript : MonoBehaviour
    {
        [Header("Управление таймером")]
        public GameManager gameManager;
        public Text timerText;

        [Header("Ссылки на объекты")]
        public Animator busAnimator;

        public GameObject tabletOwner;
        public GameObject actorOwner;
        public GameObject vovaOwner;
        public GameObject dialogueOwner;

        [SerializeField]
        private int _time = 20;

        public void EnableTimer()
        {
            StartCoroutine(TimerCoroutine());
        }

        IEnumerator TimerCoroutine()
        {
            while (_time > 0)
            {
                yield return new WaitForSeconds(1f);
                _time--;
                timerText.text = _time.ToString();

                if (_time == 5)
                    busAnimator.SetTrigger("End");
            }
            EndTime();
        }

        private void EndTime()
        {
            gameManager.NextAction();

            tabletOwner.SetActive(false);
            actorOwner.SetActive(false);
            vovaOwner.SetActive(false);
            //dialogueOwner.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}

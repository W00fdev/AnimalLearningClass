using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StudyProject
{
    public class TabletsHandler : MonoBehaviour
    {
        private Animator animator; 

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void Appearance()
        {
            animator.SetTrigger("Appearance");
        } 
    }
}

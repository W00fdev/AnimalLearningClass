using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

namespace StudyProject
{
    public class DialoguePlayNext : MonoBehaviour, IPointerClickHandler
    {
        public DialogueHandler dialogueHandler;

        public void OnPointerClick(PointerEventData pointerEventData)
        {
            dialogueHandler.PlayNext();
        }
    }
}

using UnityEngine.EventSystems;
using UnityEngine;

namespace StudyProject
{
    public class DialoguePlayNext : MonoBehaviour, IPointerClickHandler
    {
        public DialogueHandler dialogueHandler;

        public void OnPointerClick(PointerEventData pointerEventData) => dialogueHandler.PlayNext();

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                dialogueHandler.PlayNext();
        }
    }
}

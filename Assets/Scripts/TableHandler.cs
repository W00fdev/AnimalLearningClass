using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

namespace StudyProject
{
    public class TableHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public GameManager gameManager;
        public Material OutlineMaterial;
        public string tabletName;
        public bool Selectable { get => _selectable; set => _selectable = value; }
        public bool Clickable { get => _clickable; set => _clickable = value; }
        public bool WaitForNextAction { get => _waitForNextAction; set => _waitForNextAction = value;}

        private Animator _animator;
        private Image _image;


        [Header("Управление интеракциями таблички.")]
        [SerializeField]
        private bool _selectable;
        [SerializeField]
        private bool _clickable;
        [SerializeField]
        private bool _waitForNextAction;

        [SerializeField]
        private bool _selected = false;

        private void Awake()
        {
            Selectable = false;
            Clickable = false;
            WaitForNextAction = false;
        }

        private void Start()
        {
            _image = GetComponent<Image>();
            _animator = GetComponent<Animator>();
        }

        public void SwitchSelect()
        {
            _selected = !_selected;
            if (_selected == false)
                _image.material = null;
            else
                _image.material = OutlineMaterial;
        }

        public void HandleEvent(bool success)
        {
            if (!success)
            {
                _animator.SetTrigger("Error");
                return;
            }

            // success

            if (WaitForNextAction) 
            {
                gameManager.NextAction();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (Selectable)
            {
                SwitchSelect();
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (Selectable)
            {
                SwitchSelect();
            }
        }

        public void OnPointerClick(PointerEventData pointerEventData)
        {
            if (Clickable)
            {
                if (WaitForNextAction || gameManager.ChooseTablet(tabletName))
                    HandleEvent(true);
                else
                    HandleEvent(false);
            }
        }

    }
}

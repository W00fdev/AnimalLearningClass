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
        public bool Selectable {get; set;}

        private Animator _animator;
        private Image _image;

        [SerializeField]
        private bool _selected = false;

        private void Awake()
        {
            Selectable = false;
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
            if (Selectable)
            {
                SwitchSelect();
            }
        }

    }
}

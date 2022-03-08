using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

namespace StudyProject
{
    public class TableHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public Material OutlineMaterial;
        private Image _image;

        [SerializeField]
        private bool _selected = false;

        void Start()
        {
            _image = GetComponent<Image>();
        }

        void SwitchSelect()
        {
            _selected = !_selected;
            if (_selected == false)
                _image.material = null;
            else
                _image.material = OutlineMaterial;
        }

    public void OnPointerEnter(PointerEventData eventData)
	{
		SwitchSelect();
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		SwitchSelect();
	}

    }
}

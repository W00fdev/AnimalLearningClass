 using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

namespace StudyProject
{
    public class TabletHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler,
    IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [Header("Счетчик очков")]
        public ScoreTable scoreTable;


        [Header("Настройка")]
        public GameManager gameManager;
        public string tabletName;

        public bool Selectable { get => _selectable; set => _selectable = value; }
        public bool Clickable { get => _clickable; set => _clickable = value; }
        public bool WaitForNextAction { get => _waitForNextAction; set => _waitForNextAction = value;}
        public bool Grabbable { get => _grabbable; set => _grabbable = value; }

        public bool RightChoiceColor 
        { 
            get => _rightChoiceColor;  
            set
            {
                if (value == true)
                    _image.color = _green;
                else
                    _image.color = _white;

                _rightChoiceColor = value;
            }
        }

        public bool Selected 
        { 
            get => _selected;
            set
            {
                if (Selectable)
                {
                    if (value == true)
                        _image.color = _gray;
                    else
                        _image.color = _white;
                }

                _selected = value;
            }
        }


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
        private bool _grabbable;

        [SerializeField]
        private bool _selected = false;     
        
        [SerializeField]
        private bool _rightChoiceColor = false;

        [SerializeField]
        private bool onActor = false;

        private RectTransform _rtransform;
        private Vector2 _originRTransform;

        // Возвращает animator в исходное состояние.
        private bool _originAnimatorState;

        private bool _checkCollider2D = false;

        private readonly Color32 _green = new Color32(176, 255, 123, 255);
        private readonly Color32 _white = new Color32(255, 255, 255, 255);
        private readonly Color32 _gray = new Color32(200, 200, 200, 255);

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
            _rtransform = GetComponent<RectTransform>();

            _image.color = _white;
            _originRTransform = _rtransform.anchoredPosition;
        }

        public void SwitchSelect() => Selected = !Selected;
        public void SetAnimatorTrigger(string triggerName) => _animator.SetTrigger(triggerName);

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
                Selected = false;
                gameManager.NextAction();
                WaitForNextAction = false;
            }
        }

        public void OnPointerEnter(PointerEventData eventData) => Selected = true;
        public void OnPointerExit(PointerEventData eventData) => Selected = false;

        public void OnPointerClick(PointerEventData pointerEventData)
        {
            if (Clickable)
            {
                if (WaitForNextAction)
                    HandleEvent(true);
                else
                    HandleEvent(false);
            }
        }

        // Grabbable
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (Grabbable)
            {
                _originAnimatorState = _animator.enabled;
                _rtransform.sizeDelta = new Vector2(700, 700);
                _animator.enabled = false;
            }
        }

        public void OnDrag(PointerEventData data)
        {
            if (Grabbable)
                _rtransform.anchoredPosition = new Vector3(data.position.x, data.position.y, 0);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _checkCollider2D = true;
            StartCoroutine(DisableCheckCollider());
        }

        IEnumerator DisableCheckCollider()
        {
            yield return new WaitForSeconds(0.075f);

            if (!onActor)
            {
                _rtransform.anchoredPosition = _originRTransform;
                _rtransform.sizeDelta = new Vector2(1024, 1024);
                _animator.enabled = _originAnimatorState;
            }
            
            _checkCollider2D = false;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!onActor)
            {
                if(_checkCollider2D && other.TryGetComponent(out Actor actor))
                {
                    if (tabletName == actor.name)
                    {
                        onActor = true;
                        
                        ColorGreen();
                        scoreTable.Score++;
                        AllReset();
                        
                        var anchorTabletTransform = actor.GetComponent<Transform>().GetChild(0);
                        _rtransform.position = anchorTabletTransform.position;
                    }
                }
            }
        }

        private void AllReset()
        {
            Clickable = false;
            Selectable = false;
            Grabbable = false;
            _selected = false;
            _image.material = null;
        }

        public void ColorGreen(bool isGreen = true)
        {
            if (isGreen)
                _image.color = _green;
            else
                _image.color = _white;
        }

    }
}

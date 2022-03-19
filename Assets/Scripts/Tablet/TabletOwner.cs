using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StudyProject
{
    public class TabletOwner : MonoBehaviour
    {
        private List<TabletHandler> _tabletChildren = new List<TabletHandler>();

        public enum PropertyChoice { SELECTABLE = 0, CLICKABLE = 1, GRABBABLE = 2, COLORPICKED = 3, IDLE = 4 }

        private void Start()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                _tabletChildren.Add(transform.GetChild(i).GetComponent<TabletHandler>());
            }   
        }

        public void ChangeAllTabletsTRUE(string name)
        {
            switch(name)
            {
                case "Selectable": //PropertyChoice.SELECTABLE:
                    foreach (TabletHandler tablet in _tabletChildren)
                        tablet.Selectable = true;
                    break;
                case "Clickable": //PropertyChoice.CLICKABLE:
                    foreach (TabletHandler tablet in _tabletChildren)
                        tablet.Clickable = true;
                    break;
                case "Grabbable": //PropertyChoice.GRABBABLE:
                    foreach (TabletHandler tablet in _tabletChildren)
                        tablet.Grabbable = true;
                    break;
                case "RightChoiceColor": //PropertyChoice.COLORPICKED:
                    foreach (TabletHandler tablet in _tabletChildren)
                        tablet.RightChoiceColor = true;
                    break;
                case "Idle": //PropertyChoice.IDLE:
                    foreach (TabletHandler tablet in _tabletChildren)
                        tablet.SetAnimatorTrigger("Idle");
                    break;
                default:
                    throw new System.Exception("Strange property has given to change");
            }
        }

        public void ChangeAllTabletsFALSE(string name)
        {
            switch (name)
            {
                case "Selectable": //PropertyChoice.SELECTABLE:
                    foreach (TabletHandler tablet in _tabletChildren)
                        tablet.Selectable = false;
                    break;
                case "Clickable": //PropertyChoice.CLICKABLE:
                    foreach (TabletHandler tablet in _tabletChildren)
                        tablet.Clickable = false;
                    break;
                case "Grabbable": //PropertyChoice.GRABBABLE:
                    foreach (TabletHandler tablet in _tabletChildren)
                        tablet.Grabbable = false;
                    break;
                case "RightChoiceColor": //PropertyChoice.COLORPICKED:
                    foreach (TabletHandler tablet in _tabletChildren)
                        tablet.RightChoiceColor = false;
                    break;
                case "Idle": //PropertyChoice.IDLE:
                    foreach (TabletHandler tablet in _tabletChildren)
                        tablet.SetAnimatorTrigger("Idle");
                    break;
                default:
                    throw new System.Exception("Strange property has given to change");
            }
        }
    }
}

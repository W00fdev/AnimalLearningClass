using UnityEngine;

namespace StudyProject
{
    [CreateAssetMenu(menuName = "Dialogue Instance")]
    public class Dialogue : ScriptableObject
    {
        public string speaker = "";
        [TextArea(5,10)]
        public string dialogue = "";  

        //public StateActive activeState;
        public StatePassive passiveState;
    }
}

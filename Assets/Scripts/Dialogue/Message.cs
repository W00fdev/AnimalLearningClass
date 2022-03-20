using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StudyProject
{
    [CreateAssetMenu(menuName = "Message Instance")]
    public class Message : ScriptableObject
    {
        public List<string> Speakers;
        [TextArea(5, 10)]
        public List<string> Speeches;
        public List<StatePassive> Moods;
    }
}

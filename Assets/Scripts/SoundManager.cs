using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StudyProject
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioClip _lionClip;
        [SerializeField] private AudioClip _elephantClip;

        private AudioSource _audio;

        private void Start() => _audio = GetComponent<AudioSource>();

        public void PlayLion() => _audio.PlayOneShot(_lionClip);
        public void PlayElephant() => _audio.PlayOneShot(_elephantClip);
    }
}

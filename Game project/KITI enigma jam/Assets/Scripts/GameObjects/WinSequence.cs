using System;
using UnityEngine;

namespace GameObjects
{
    public class WinSequence : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip audioClip;
        [SerializeField] private GameObject[] particleSystemsToEnable;

        public void Play()
        {
            audioSource.Stop();
            audioSource.PlayOneShot(audioClip);
            foreach (var o in particleSystemsToEnable)
            {
                o.SetActive(true);
            }
        }
    }
}
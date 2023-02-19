using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ButtonSound : MonoBehaviour
    {
        [SerializeField] private AudioClip audioClip;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private Button button;

        private void Start()
        {
            button.onClick.AddListener(PlayClickSound);
        }

        private void PlayClickSound()
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
}
using System.Collections;
using ChatGPT.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace GameObjects
{
    public class WinSequence : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip audioClip;
        [SerializeField] private GameObject[] particleSystemsToEnable;
        [FormerlySerializedAs("trackLength")] [SerializeField] private int delayLength;
        private bool startFade;
        private float fadeTime;
        private float initialVolume;
        [SerializeField] private CanvasGroup fadeCanvas;
        [SerializeField] private int mainSceneIndex;
        [SerializeField] private TextFader winText;

        public void Play()
        {
            initialVolume = audioSource.volume;
            audioSource.Stop();
            audioSource.PlayOneShot(audioClip);
            foreach (var o in particleSystemsToEnable)
            {
                o.SetActive(true);
            }
            winText.StartFade();

            StartCoroutine(nameof(StopMusicAndFade));
        }

        IEnumerator StopMusicAndFade()
        {
            yield return new WaitForSeconds(delayLength);
            startFade = true;
        }

        private void Update()
        {
            if (startFade)
            {
                fadeTime += Mathf.Min(1f, Time.deltaTime);
                var time = fadeTime * 0.5f;
                fadeCanvas.alpha = Mathf.Lerp(0.0f, 1.0f, time);
                audioSource.volume = Mathf.Lerp(initialVolume, 0.0f, time);
                if (time > 1)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    SceneManager.LoadScene(mainSceneIndex);
                }
            }
        }
    }
}
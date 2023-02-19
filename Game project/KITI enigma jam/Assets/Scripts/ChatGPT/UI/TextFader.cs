using System.Collections;
using TMPro;
using UnityEngine;

namespace ChatGPT.UI
{
    public class TextFader : MonoBehaviour
    {
        public TextMeshProUGUI textComponent;
        public float fadeTime = 1f;
        public float waitTime = 1f;
        public float initialWaitTime = 3f;

        private Color originalColor;

        void Start()
        {
            originalColor = textComponent.color;
            textComponent.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        }

        public void StartFade()
        {
            StartCoroutine(Fade());
        }

        IEnumerator Fade()
        {
            yield return new WaitForSeconds(initialWaitTime);
            
            float t = 0f;

            // Fade in
            while (t < fadeTime)
            {
                t += Time.deltaTime;
                float alpha = Mathf.Lerp(0f, 1f, t / fadeTime);
                textComponent.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                yield return null;
            }

            yield return new WaitForSeconds(waitTime);

            t = 0f;

            // Fade out
            while (t < fadeTime)
            {
                t += Time.deltaTime;
                float alpha = Mathf.Lerp(1f, 0f, t / fadeTime);
                textComponent.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                yield return null;
            }

            // textComponent.color = originalColor;
        }
    }
}
